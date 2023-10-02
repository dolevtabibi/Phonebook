namespace Phonebook_APP
{
    partial class FrontPage
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.personsTable = new System.Windows.Forms.DataGridView();
            this.personGridDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.phonebook_DatabaseDataSet = new Phonebook_APP.Phonebook_DatabaseDataSet();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.personGridDataTableAdapter = new Phonebook_APP.Phonebook_DatabaseDataSetTableAdapters.PersonGridDataTableAdapter();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Picture = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personGridDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phonebook_DatabaseDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.personsTable);
            this.groupBox2.Location = new System.Drawing.Point(73, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(708, 190);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // personsTable
            // 
            this.personsTable.AllowUserToAddRows = false;
            this.personsTable.AllowUserToDeleteRows = false;
            this.personsTable.AllowUserToResizeColumns = false;
            this.personsTable.AllowUserToResizeRows = false;
            this.personsTable.AutoGenerateColumns = false;
            this.personsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.personsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FirstName,
            this.LastName,
            this.Id,
            this.DateOfBirth,
            this.Address,
            this.City,
            this.Picture});
            this.personsTable.DataSource = this.personGridDataBindingSource;
            this.personsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personsTable.Location = new System.Drawing.Point(3, 16);
            this.personsTable.MultiSelect = false;
            this.personsTable.Name = "personsTable";
            this.personsTable.ReadOnly = true;
            this.personsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.personsTable.Size = new System.Drawing.Size(702, 171);
            this.personsTable.TabIndex = 0;
            // 
            // personGridDataBindingSource
            // 
            this.personGridDataBindingSource.DataMember = "PersonGridData";
            this.personGridDataBindingSource.DataSource = this.phonebook_DatabaseDataSet;
            // 
            // phonebook_DatabaseDataSet
            // 
            this.phonebook_DatabaseDataSet.DataSetName = "Phonebook_DatabaseDataSet";
            this.phonebook_DatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SearchField
            // 
            this.SearchField.Location = new System.Drawing.Point(362, 19);
            this.SearchField.Name = "SearchField";
            this.SearchField.Size = new System.Drawing.Size(100, 20);
            this.SearchField.TabIndex = 4;
            this.SearchField.TextChanged += new System.EventHandler(this.SearchField_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "חפש";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(197, 18);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(70, 70);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "מחק";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(111, 18);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(70, 70);
            this.updateButton.TabIndex = 1;
            this.updateButton.Text = "עדכן";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(20, 18);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(70, 70);
            this.newButton.TabIndex = 0;
            this.newButton.Text = "חדש";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SearchField);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.updateButton);
            this.groupBox1.Controls.Add(this.newButton);
            this.groupBox1.Location = new System.Drawing.Point(267, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 106);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // personGridDataTableAdapter
            // 
            this.personGridDataTableAdapter.ClearBeforeFill = true;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            this.LastName.HeaderText = "LastName";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // DateOfBirth
            // 
            this.DateOfBirth.DataPropertyName = "DateOfBirth";
            this.DateOfBirth.HeaderText = "DateOfBirth";
            this.DateOfBirth.Name = "DateOfBirth";
            this.DateOfBirth.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // City
            // 
            this.City.DataPropertyName = "City";
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // Picture
            // 
            this.Picture.DataPropertyName = "Picture";
            this.Picture.HeaderText = "Picture";
            this.Picture.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Picture.Name = "Picture";
            this.Picture.ReadOnly = true;
            // 
            // FrontPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrontPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Front_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.personsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personGridDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phonebook_DatabaseDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView personsTable;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private Phonebook_DatabaseDataSet phonebook_DatabaseDataSet;
        private System.Windows.Forms.BindingSource personGridDataBindingSource;
        private Phonebook_DatabaseDataSetTableAdapters.PersonGridDataTableAdapter personGridDataTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewImageColumn Picture;
    }
}

