namespace LinkInContactManagement
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtExcludeDate = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnSyncWithLinkedIn = new System.Windows.Forms.Button();
            this.ShouldSkipAutoReachOut = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastAutoContacted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsStillLinkedInContact = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFollowUpNotes = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToDoList = new System.Windows.Forms.LinkLabel();
            this.btnAddToDo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(794, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Body";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(805, 527);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(223, 23);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(802, 475);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Exclude those who have been contacts since:";
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(797, 30);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(223, 442);
            this.txtBody.TabIndex = 6;
            this.txtBody.Text = resources.GetString("txtBody.Text");
            this.txtBody.TextChanged += new System.EventHandler(this.txtBody_TextChanged);
            // 
            // txtExcludeDate
            // 
            this.txtExcludeDate.Location = new System.Drawing.Point(805, 491);
            this.txtExcludeDate.Name = "txtExcludeDate";
            this.txtExcludeDate.Size = new System.Drawing.Size(136, 20);
            this.txtExcludeDate.TabIndex = 7;
            this.txtExcludeDate.Text = "1/1/2017";
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShouldSkipAutoReachOut,
            this.LastName,
            this.FirstName,
            this.LastAutoContacted,
            this.IsStillLinkedInContact,
            this.Notes});
            this.dataGrid.Location = new System.Drawing.Point(12, 67);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(769, 768);
            this.dataGrid.TabIndex = 8;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValueChanged);
            this.dataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseClick);
            this.dataGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGrid_DataBindingComplete);
            this.dataGrid.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_RowHeaderMouseClick);
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Location = new System.Drawing.Point(793, 553);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(235, 50);
            this.lblMessage.TabIndex = 9;
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // btnSyncWithLinkedIn
            // 
            this.btnSyncWithLinkedIn.Location = new System.Drawing.Point(793, 812);
            this.btnSyncWithLinkedIn.Name = "btnSyncWithLinkedIn";
            this.btnSyncWithLinkedIn.Size = new System.Drawing.Size(232, 23);
            this.btnSyncWithLinkedIn.TabIndex = 10;
            this.btnSyncWithLinkedIn.Text = "Resync with Linked In";
            this.btnSyncWithLinkedIn.UseVisualStyleBackColor = true;
            this.btnSyncWithLinkedIn.Click += new System.EventHandler(this.btnSyncWithLinkedIn_Click);
            // 
            // ShouldSkipAutoReachOut
            // 
            this.ShouldSkipAutoReachOut.DataPropertyName = "ShouldSkipAutoReachOut";
            this.ShouldSkipAutoReachOut.HeaderText = "Skip Email";
            this.ShouldSkipAutoReachOut.Name = "ShouldSkipAutoReachOut";
            this.ShouldSkipAutoReachOut.Width = 50;
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            this.LastName.HeaderText = "Last Name";
            this.LastName.Name = "LastName";
            this.LastName.Width = 150;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            this.FirstName.Width = 150;
            // 
            // LastAutoContacted
            // 
            this.LastAutoContacted.DataPropertyName = "LastAutoContacted";
            this.LastAutoContacted.HeaderText = "Last Auto Contacted";
            this.LastAutoContacted.Name = "LastAutoContacted";
            // 
            // IsStillLinkedInContact
            // 
            this.IsStillLinkedInContact.DataPropertyName = "IsStillLinkedInContact";
            this.IsStillLinkedInContact.HeaderText = "Still A Contact";
            this.IsStillLinkedInContact.Name = "IsStillLinkedInContact";
            this.IsStillLinkedInContact.Width = 50;
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "Notes";
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.Width = 220;
            // 
            // btnFollowUpNotes
            // 
            this.btnFollowUpNotes.Enabled = false;
            this.btnFollowUpNotes.Location = new System.Drawing.Point(324, 28);
            this.btnFollowUpNotes.Name = "btnFollowUpNotes";
            this.btnFollowUpNotes.Size = new System.Drawing.Size(165, 23);
            this.btnFollowUpNotes.TabIndex = 11;
            this.btnFollowUpNotes.Text = "Follow up record";
            this.btnFollowUpNotes.UseVisualStyleBackColor = true;
            this.btnFollowUpNotes.Click += new System.EventHandler(this.btnFollowUpNotes_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(12, 30);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(306, 20);
            this.txtFilter.TabIndex = 12;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Filter";
            // 
            // lblToDoList
            // 
            this.lblToDoList.AutoSize = true;
            this.lblToDoList.Location = new System.Drawing.Point(588, 32);
            this.lblToDoList.Name = "lblToDoList";
            this.lblToDoList.Size = new System.Drawing.Size(37, 13);
            this.lblToDoList.TabIndex = 14;
            this.lblToDoList.TabStop = true;
            this.lblToDoList.Text = "To Do";
            this.lblToDoList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblToDoList_LinkClicked);
            // 
            // btnAddToDo
            // 
            this.btnAddToDo.Location = new System.Drawing.Point(686, 27);
            this.btnAddToDo.Name = "btnAddToDo";
            this.btnAddToDo.Size = new System.Drawing.Size(75, 23);
            this.btnAddToDo.TabIndex = 15;
            this.btnAddToDo.Text = "Add To Do";
            this.btnAddToDo.UseVisualStyleBackColor = true;
            this.btnAddToDo.Click += new System.EventHandler(this.btnAddToDo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 847);
            this.Controls.Add(this.btnAddToDo);
            this.Controls.Add(this.lblToDoList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnFollowUpNotes);
            this.Controls.Add(this.btnSyncWithLinkedIn);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.txtExcludeDate);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Linked In Reach Out";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.TextBox txtExcludeDate;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnSyncWithLinkedIn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ShouldSkipAutoReachOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastAutoContacted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsStillLinkedInContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.Button btnFollowUpNotes;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblToDoList;
        private System.Windows.Forms.Button btnAddToDo;
    }
}

