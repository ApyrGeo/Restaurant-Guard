﻿namespace Restaurant_Management_App_2
{
    partial class EmployeeLogIn
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
            label3 = new Label();
            label2 = new Label();
            txt_password = new TextBox();
            label1 = new Label();
            txt_uname = new TextBox();
            button1 = new Button();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // label3
            // 
            label3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label3.Location = new Point(21, 8);
            label3.Name = "label3";
            label3.Size = new Size(334, 52);
            label3.TabIndex = 12;
            label3.Text = "Restaurant Guard";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(33, 240);
            label2.Name = "label2";
            label2.Size = new Size(92, 24);
            label2.TabIndex = 11;
            label2.Text = "Password";
            // 
            // txt_password
            // 
            txt_password.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_password.Location = new Point(38, 270);
            txt_password.Name = "txt_password";
            txt_password.PasswordChar = '*';
            txt_password.Size = new Size(310, 28);
            txt_password.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(33, 147);
            label1.Name = "label1";
            label1.Size = new Size(186, 24);
            label1.TabIndex = 9;
            label1.Text = "Employee username";
            // 
            // txt_uname
            // 
            txt_uname.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_uname.Location = new Point(38, 177);
            txt_uname.Name = "txt_uname";
            txt_uname.Size = new Size(310, 28);
            txt_uname.TabIndex = 8;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.LimeGreen;
            button1.FlatAppearance.MouseOverBackColor = Color.LimeGreen;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.Location = new Point(126, 429);
            button1.Name = "button1";
            button1.Size = new Size(123, 40);
            button1.TabIndex = 7;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 238);
            label4.Location = new Point(13, 81);
            label4.Name = "label4";
            label4.Size = new Size(354, 52);
            label4.TabIndex = 13;
            label4.Text = "In order to access this window, you need to be connected as an employee!";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label5.ImageAlign = ContentAlignment.MiddleRight;
            label5.Location = new Point(351, 0);
            label5.Name = "label5";
            label5.Size = new Size(29, 34);
            label5.TabIndex = 14;
            label5.Text = "X";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.Click += label5_Click;
            label5.MouseLeave += label5_MouseLeave;
            label5.MouseHover += label5_MouseHover;
            // 
            // EmployeeLogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(378, 518);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txt_password);
            Controls.Add(label1);
            Controls.Add(txt_uname);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmployeeLogIn";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EmployeeLogIn";
            Load += EmployeeLogIn_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_uname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}