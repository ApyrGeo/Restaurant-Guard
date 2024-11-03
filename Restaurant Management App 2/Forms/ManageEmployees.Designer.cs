namespace Restaurant_Management_App_2
{
    partial class ManageEmployees
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            txt_name = new TextBox();
            txt_salary = new TextBox();
            label3 = new Label();
            txt_uname = new TextBox();
            label4 = new Label();
            txt_password = new TextBox();
            label5 = new Label();
            button1 = new Button();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(797, 781);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 19.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(803, 42);
            label1.Name = "label1";
            label1.Size = new Size(285, 38);
            label1.TabIndex = 1;
            label1.Text = "Add an employee";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(835, 212);
            label2.Name = "label2";
            label2.Size = new Size(64, 25);
            label2.TabIndex = 2;
            label2.Text = "Name";
            // 
            // txt_name
            // 
            txt_name.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txt_name.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_name.Location = new Point(919, 209);
            txt_name.Margin = new Padding(3, 4, 3, 4);
            txt_name.Name = "txt_name";
            txt_name.Size = new Size(183, 30);
            txt_name.TabIndex = 3;
            // 
            // txt_salary
            // 
            txt_salary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txt_salary.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_salary.Location = new Point(919, 429);
            txt_salary.Margin = new Padding(3, 4, 3, 4);
            txt_salary.Name = "txt_salary";
            txt_salary.Size = new Size(183, 30);
            txt_salary.TabIndex = 6;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(831, 432);
            label3.Name = "label3";
            label3.Size = new Size(68, 25);
            label3.TabIndex = 4;
            label3.Text = "Salary";
            // 
            // txt_uname
            // 
            txt_uname.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txt_uname.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_uname.Location = new Point(919, 282);
            txt_uname.Margin = new Padding(3, 4, 3, 4);
            txt_uname.Name = "txt_uname";
            txt_uname.Size = new Size(183, 30);
            txt_uname.TabIndex = 4;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.ForeColor = Color.Cornsilk;
            label4.Location = new Point(797, 286);
            label4.Name = "label4";
            label4.Size = new Size(102, 25);
            label4.TabIndex = 6;
            label4.Text = "Username";
            // 
            // txt_password
            // 
            txt_password.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txt_password.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_password.Location = new Point(919, 355);
            txt_password.Margin = new Padding(3, 4, 3, 4);
            txt_password.Name = "txt_password";
            txt_password.PasswordChar = '*';
            txt_password.Size = new Size(183, 30);
            txt_password.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.ForeColor = Color.Cornsilk;
            label5.Location = new Point(801, 359);
            label5.Name = "label5";
            label5.Size = new Size(98, 25);
            label5.TabIndex = 8;
            label5.Text = "Password";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.ForeColor = Color.Cornsilk;
            button1.Location = new Point(864, 525);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(161, 59);
            button1.TabIndex = 7;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Column1
            // 
            Column1.HeaderText = "Id";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Visible = false;
            // 
            // Column2
            // 
            Column2.HeaderText = "Name";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Username";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "Salary";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 192, 192);
            Column5.DefaultCellStyle = dataGridViewCellStyle1;
            Column5.FlatStyle = FlatStyle.Flat;
            Column5.HeaderText = "Remove";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Text = "Remove";
            Column5.UseColumnTextForButtonValue = true;
            // 
            // ManageEmployees
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateBlue;
            ClientSize = new Size(1160, 781);
            Controls.Add(button1);
            Controls.Add(txt_password);
            Controls.Add(label5);
            Controls.Add(txt_uname);
            Controls.Add(label4);
            Controls.Add(txt_salary);
            Controls.Add(label3);
            Controls.Add(txt_name);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ManageEmployees";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageEmployees";
            WindowState = FormWindowState.Maximized;
            Load += ManageEmployees_Load;
            ResizeEnd += ManageEmployees_ResizeEnd;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_salary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_uname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_name;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewButtonColumn Column5;
    }
}