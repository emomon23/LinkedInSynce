using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LinkInContactManagement.Utils;
using LinkInContactManagement.LinkedIn;
using LinkInContactManagement.Model;
using LinkInContactManagement.Repository;

namespace LinkInContactManagement
{
    public partial class Form1 : Form
    {
        ContactRepository repo = new ContactRepository();
        ToDoRepo todoRepo = new ToDoRepo();
        LinkedInContactsDriver linkedInDriver = null;
        List<Contact> contacts = null;
        private List<Contact> subContacts = null;
        private bool dataIsLoaded = false;
        private Thread _workerThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          //  Database.SetInitializer<ContactContext>(new DropCreateDatabaseIfModelChanges<ContactContext>());
        
            contacts = repo.GetCurrentLinkedInContacts();
            UpdateToDoListLink();
            LoadDataGrid();
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataIsLoaded)
            {
                var contact = dataGrid.Rows[e.RowIndex].DataBoundItem as Contact;
                repo.UpdateContact(contact);
            }

        }

        private void UpdateToDoListLink()
        {
            var toDoList = todoRepo.GetToDoList();

            lblToDoList.ForeColor = toDoList.GetHightestUrgencyColor();
            lblToDoList.Text = "To Dos: " + toDoList.Count;

        }

        private void LoadDataGrid(List<Contact> listToBind = null)
        {

            dataIsLoaded = false;
            dataGrid.DataSource = null;
            dataGrid.DataSource = listToBind==null?  contacts : listToBind;
            dataIsLoaded = true;
            btnFollowUpNotes.Enabled = false;

            dataGrid.HideAllColumnsExcpet("ShouldSkipAutoReachOut", "FirstName", "LastName", "Title", "LastAutoContacted", "IsStillLinkedInContact", "Notes");

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            _workerThread = new Thread(new ThreadStart(SendMessages));
            _workerThread.Start();
        }

        private void SendMessages() { 
            if (!ValidateSendMessage())
            {
                return;
            }

            if (!CheckForGreetingTypoPassed())
            {
                return;
            }

            if (!VerifyUserWantsToProceed())
            {
                return;
            }

            var contactsToSendMessage = GetContactsToSendMessageTo();
            var count = contactsToSendMessage.Count;

            UpdateLabelTextCrossThread($"Sending messages to {count}");

            int counter = 1;

            if (contactsToSendMessage.Count > 0)
            {
                InitializeLinkedInDriver();
            }

            int consecutiveErrors = 0;
            foreach (var contact in contactsToSendMessage)
            {
                counter += 1;

                var body =
                    txtBody.Text.Replace("[FirstName]", contact.FirstName)
                        .Replace("[firstname]", contact.FirstName)
                        .Replace("[FIRSTNAME]", contact.FirstName)
                        .Replace("[LastName]", contact.LastName)
                        .Replace("[lastname]", contact.LastName)
                        .Replace("[LASTNAME]", contact.LastName);

                try
                {
                    linkedInDriver.SendMessage(contact, body);
                    repo.UpdateLastAutoContact(contact);

                    UpdateLabelTextCrossThread($"Send {counter} of {count} messages");
                    Application.DoEvents();

                    linkedInDriver.PauseRandom(50, 110);
                    consecutiveErrors = 0;
                }
                catch (Exception exp)
                {
                    consecutiveErrors += 1;
                    contact.Notes = "FAIL - " + exp.Message;
                    repo.UpdateLastAutoContact(contact);

                    if (consecutiveErrors > 10)
                    {
                        UpdateLabelTextCrossThread("Quit after 10 consective errors");
                        break;
                    }
                }
            }

        }

        private void UpdateLabelTextCrossThread(string text)
        {
            Invoke((MethodInvoker) delegate
            {
                lblMessage.Text = text;
            });
        }

        private bool ValidateSendMessage()
        {
            var message = "";

            if (txtExcludeDate.Text.IsNotNull() && !txtExcludeDate.Text.IsDate())
            {
               message = "Exclude date must be a valid date\n";
            }

           
            if (txtBody.Text.IsNull())
            {
                message += "You need a body";
            }

            if (message != "")
            {
                MessageBox.Show(message);
            }

            return message == "";
        }

        private bool VerifyUserWantsToProceed()
        {
            return MessageBox.Show("Send Message to linked in contacts", "Are you sure?", MessageBoxButtons.YesNo) ==
                   DialogResult.Yes;
        }

        private bool AreYouSure(string message)
        {
            return MessageBox.Show(message, "Are you sure?", MessageBoxButtons.YesNo) ==
                  DialogResult.Yes;
        }
    

        private bool CheckForGreetingTypoPassed()
        {
            var check = txtBody.Text.ToLower();

            check = check.Replace("[firstname]", "").Replace("[lastname]", "");

            if (check.Contains("firstname") || check.Contains("lastname"))
            {
                var result = MessageBox.Show("Greeting looks suspicious, are you sure you want to send this?", "Warning!!!",
                    MessageBoxButtons.YesNo);

                return result == DialogResult.Yes;
            }

            return true;
        }

        private List<Contact> GetContactsToSendMessageTo()
        {
            if (txtExcludeDate.Text.IsNotNull())
            {
                DateTime backDate = DateTime.Parse(txtExcludeDate.Text);
                return contacts.Where(c => c.ShouldSkipAutoReachOut == false && (c.LastAutoContacted < backDate || c.LastAutoContacted == null)).ToList();
            }

            return contacts.Where(c => c.ShouldSkipAutoReachOut == false).ToList();
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

        private void InitializeLinkedInDriver()
        {
            if (linkedInDriver == null)
            {
                linkedInDriver = new LinkedInContactsDriver();
                linkedInDriver.Initialize();
            }
        }

        private void btnSyncWithLinkedIn_Click(object sender, EventArgs e)
        {
            if (AreYouSure("RE-SYNC with linked in?"))
            {
                dataIsLoaded = false;
                InitializeLinkedInDriver();

                if (!repo.AllContactsAreCurrent())
                {
                    repo.Update_IsStillLinkedInContactToFalse(DateTime.Now.AddDays(-2));

                    foreach (var linkedInContact in linkedInDriver.Contacts)
                    {
                        repo.AddOrUpdate_IsStillLinkedInContactToTrue(linkedInContact);
                    }
                }

                contacts = repo.GetCurrentLinkedInContacts();
                LoadDataGrid();
            }
        }


        private void dataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void txtBody_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            var filter = txtFilter.Text.ToLower();
            if (filter.IsNull())
            {
                subContacts = contacts;
                LoadDataGrid();
                return;
            }

            if (subContacts == null)
            {
                subContacts = contacts;
            }
            
            subContacts = subContacts.Where(c => c.FirstName.ToLower().Contains(filter) || c.LastName.ToLower().Contains(filter) || c.Title.ToLower().Contains(filter.ToLower())).ToList();
            LoadDataGrid(subContacts);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFollowUpNotes_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count == 1)
            {
                var contact = dataGrid.SelectedRows[0].DataBoundItem as Contact;
                var frmFollowUpRecord = new FollowUpDetail();
                if (frmFollowUpRecord.ManageFollowUpRecord(ref contact))
                {
                    repo.UpdateContact(contact);
                }

                txtFilter.Text = "";
                subContacts = contacts;
                LoadDataGrid();
            }
        }

        private void dataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGrid.SelectedRows.Count == 1)
            {
                btnFollowUpNotes.Enabled = true;
                return;
            }

            btnFollowUpNotes.Enabled = false;

        }

        private void btnAddToDo_Click(object sender, EventArgs e)
        {
            var frmToDo = new ToDoForm();

            var newToDo = frmToDo.ShowDialog_AddToDo();
            if (newToDo != null)
            {
                todoRepo.AddNewToDo(newToDo);
                UpdateToDoListLink();
            }
        }

        private void lblToDoList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frmList = new ToDoList();
            frmList.ShowDialog(this);
            UpdateToDoListLink();
        }
    }
}
