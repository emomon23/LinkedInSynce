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
    public partial class ToDoForm : Form
    {
        private bool _cancelClicked = true;
        private ToDo _toDo;

        public ToDoForm()
        {
            InitializeComponent();
        }

        private void ToDo_Load(object sender, EventArgs e)
        {

        }

        
        public bool ShowDialog(ref ToDo todo)
        {
            _toDo = todo;
            txtDesc.Text = todo.Description;
            txtDueDate.Text = todo.DueDate.ToDateString();
            lblDateCreated.Text = todo.DateCreated.ToString("g");
            txtTitle.Text = todo.Title;
            ShowDialog();
            return _cancelClicked == false;
        }


        public ToDo ShowDialog_AddToDo()
        {
            _toDo = new ToDo();
            txtDueDate.Text = DateTime.Now.AddDays(1).ToString("g");
            lblDateCreated.Text = DateTime.Now.ToString("g");
            ShowDialog();

            if (_cancelClicked)
            {
                return null;
            }

            return _toDo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelClicked = true;
            Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _cancelClicked = false;
            _toDo.Description = txtDesc.Text;
            _toDo.Title = txtTitle.Text;
            _toDo.DueDate.SetValueFromString(txtDueDate.Text);
            _toDo.DateCreated = DateTime.Parse(lblDateCreated.Text);
            _toDo.DateCompleted.SetValueFromString(txtDateCompleted.Text);
            Hide();
        }
    }

}
