using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkInContactManagement.Model;
using LinkInContactManagement.Utils;

namespace LinkInContactManagement
{
    public partial class FollowUpDetail : Form
    {
        private bool _cancelClicked = true;
        private Contact _contact;

        public FollowUpDetail()
        {
            InitializeComponent();
        }

        private void FollowUpDetail_Load(object sender, EventArgs e)
        {

        }

        public bool ManageFollowUpRecord(ref Contact contact)
        {
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtEmail.Text = contact.Email;
            txtPhone.Text = contact.Phone;
            txtLastReachOutDate.Text = contact.LastReachedOut.ToDateString(DateTime.Now.ToString("g"));
            txtNextReachOutDate.Text = contact.ScheduleNextReachOut.ToDateString();
            txtReachOutMethod.Text = contact.LastReachOutMethod;
            txttitle.Text = contact.Title;
            txtNotes.Text = contact.Notes;
            chkIsStillLinkedIn.Checked = contact.IsStillLinkedInContact;
            chkSkipAutoContact.Checked = contact.ShouldSkipAutoReachOut;
            lblLastAutoContact.Text = contact.LastAutoContacted.ToDateString("NA");
            lblLastUpdatedDate.Text = contact.LastUpdateDate.ToString("g");

            _contact = contact;
            ShowDialog();
            return _cancelClicked == false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelClicked = true;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _contact.FirstName = txtFirstName.Text;
            _contact.LastName = txtLastName.Text;
            _contact.Email = txtEmail.Text;
            _contact.Phone = txtPhone.Text;
            _contact.LastReachedOut.SetValueFromString(txtLastReachOutDate.Text);
            _contact.ScheduleNextReachOut.SetValueFromString(txtNextReachOutDate.Text);
            _contact.LastReachOutMethod = txtReachOutMethod.Text;
            _contact.Title = txttitle.Text;
            _contact.Notes = txtNotes.Text;
            _contact.IsStillLinkedInContact = chkIsStillLinkedIn.Checked;
            _contact.ShouldSkipAutoReachOut = chkSkipAutoContact.Checked;
            _contact.LastUpdateDate = DateTime.Now;
            _cancelClicked = false;
            this.Close();
        }

        private void txtNextReachOutDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
