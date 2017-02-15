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
using LinkInContactManagement.Repository;
using LinkInContactManagement.Utils;

namespace LinkInContactManagement
{
    public partial class ToDoList : Form
    {
        private ToDoRepo repo = new ToDoRepo();
        private List<ToDo> todos = null;

        public ToDoList()
        {
            InitializeComponent();
        }

        private void ToDoList_Load(object sender, EventArgs e)
        {
            todos = repo.GetToDoList();
            LoadToDoList();
        }


        private void LoadToDoList()
        {
            dataGridView1.DataSource = todos;

            if (dataGridView1.Columns["Select"] == null)
            {
                dataGridView1.Columns.Insert(0,
                    new DataGridViewCheckBoxColumn() {Name = "Select", HeaderText = "Select"});
            }

            dataGridView1.HideAllColumnsExcpet("Select", "Title", "DueDate", "DateCompleted");
        }

        private bool AreYouSure(string message)
        {
            return MessageBox.Show(message, "Are you sure?", MessageBoxButtons.YesNo) ==
                  DialogResult.Yes;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            var frm = new ToDoForm();

            var newToDo = frm.ShowDialog_AddToDo();
            if (newToDo != null)
            {
                repo.AddNewToDo(newToDo);
                todos.Add(newToDo);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                btnEdit.Enabled = true;
                return;
            }

            btnEdit.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (GetSelectedToDoListFromDataGrid().Count > 0)
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }
            }
        }

        private List<ToDo> GetSelectedToDoListFromDataGrid()
        {
            var result = new List<ToDo>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
            }

            return result;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (AreYouSure("Delete To Do's?"))
            {
                repo.DeleteToDoList(GetSelectedToDoListFromDataGrid());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var todo = dataGridView1.SelectedRows[0].DataBoundItem as ToDo;

            var frm = new ToDoForm();

            if (frm.ShowDialog(ref todo))
            {
                repo.UpdateToDo(todo);
            }

            btnEdit.Enabled = false;
        }
    }
}
