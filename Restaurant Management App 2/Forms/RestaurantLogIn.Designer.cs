namespace Restaurant_Management_App_2
{
    partial class RestaurantLogIn
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
            button1 = new Button();
            txt_id = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txt_password = new TextBox();
            lbl_register = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.LimeGreen;
            button1.FlatAppearance.MouseOverBackColor = Color.LimeGreen;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.Location = new Point(145, 548);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(141, 54);
            button1.TabIndex = 0;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // txt_id
            // 
            txt_id.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_id.Location = new Point(44, 211);
            txt_id.Margin = new Padding(3, 4, 3, 4);
            txt_id.Name = "txt_id";
            txt_id.Size = new Size(354, 34);
            txt_id.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(39, 171);
            label1.Name = "label1";
            label1.Size = new Size(154, 29);
            label1.TabIndex = 2;
            label1.Text = "Restaurant id";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(39, 295);
            label2.Name = "label2";
            label2.Size = new Size(120, 29);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // txt_password
            // 
            txt_password.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_password.Location = new Point(44, 335);
            txt_password.Margin = new Padding(3, 4, 3, 4);
            txt_password.Name = "txt_password";
            txt_password.PasswordChar = '*';
            txt_password.Size = new Size(354, 34);
            txt_password.TabIndex = 3;
            // 
            // lbl_register
            // 
            lbl_register.AutoSize = true;
            lbl_register.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lbl_register.ForeColor = SystemColors.HotTrack;
            lbl_register.Location = new Point(12, 658);
            lbl_register.Name = "lbl_register";
            lbl_register.Size = new Size(340, 18);
            lbl_register.TabIndex = 5;
            lbl_register.Text = "Don`t have a registered restaurant? Enter one here";
            lbl_register.Click += lbl_register_Click;
            lbl_register.MouseLeave += lbl_register_MouseLeave;
            lbl_register.MouseHover += lbl_register_MouseHover;
            // 
            // label3
            // 
            label3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label3.Location = new Point(15, 40);
            label3.Name = "label3";
            label3.Size = new Size(405, 70);
            label3.TabIndex = 6;
            label3.Text = "Restaurant Guard";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RestaurantLogIn
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(432, 691);
            Controls.Add(label3);
            Controls.Add(lbl_register);
            Controls.Add(label2);
            Controls.Add(txt_password);
            Controls.Add(label1);
            Controls.Add(txt_id);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "RestaurantLogIn";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Log In";
            Load += RestaurantLogIn_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label lbl_register;
        private System.Windows.Forms.Label label3;
    }
}