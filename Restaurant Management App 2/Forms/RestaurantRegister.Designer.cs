namespace Restaurant_Management_App_2
{
    partial class RestaurantRegister
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
            txt_rpassword = new TextBox();
            label1 = new Label();
            txt_rname = new TextBox();
            button1 = new Button();
            txt_L = new TextBox();
            txt_W = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txt_mpassword = new TextBox();
            label8 = new Label();
            txt_mname = new TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label3.Location = new Point(32, 26);
            label3.Name = "label3";
            label3.Size = new Size(354, 52);
            label3.TabIndex = 12;
            label3.Text = "Restaurant Guard";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(55, 191);
            label2.Name = "label2";
            label2.Size = new Size(192, 24);
            label2.TabIndex = 11;
            label2.Text = "Restaurant password*";
            // 
            // txt_rpassword
            // 
            txt_rpassword.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_rpassword.Location = new Point(60, 221);
            txt_rpassword.Name = "txt_rpassword";
            txt_rpassword.PasswordChar = '*';
            txt_rpassword.Size = new Size(310, 28);
            txt_rpassword.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(55, 115);
            label1.Name = "label1";
            label1.Size = new Size(152, 24);
            label1.TabIndex = 9;
            label1.Text = "Restaurant name";
            // 
            // txt_rname
            // 
            txt_rname.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_rname.Location = new Point(60, 145);
            txt_rname.Name = "txt_rname";
            txt_rname.Size = new Size(310, 28);
            txt_rname.TabIndex = 8;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.LimeGreen;
            button1.FlatAppearance.MouseOverBackColor = Color.LimeGreen;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.Location = new Point(144, 531);
            button1.Name = "button1";
            button1.Size = new Size(123, 40);
            button1.TabIndex = 7;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // txt_L
            // 
            txt_L.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_L.Location = new Point(101, 277);
            txt_L.Name = "txt_L";
            txt_L.Size = new Size(98, 28);
            txt_L.TabIndex = 13;
            // 
            // txt_W
            // 
            txt_W.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_W.Location = new Point(272, 277);
            txt_W.Name = "txt_W";
            txt_W.Size = new Size(98, 28);
            txt_W.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(55, 279);
            label4.Name = "label4";
            label4.Size = new Size(25, 24);
            label4.TabIndex = 15;
            label4.Text = "L:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.Location = new Point(231, 279);
            label5.Name = "label5";
            label5.Size = new Size(33, 24);
            label5.TabIndex = 16;
            label5.Text = "W:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(10, 589);
            label6.Name = "label6";
            label6.Size = new Size(336, 13);
            label6.TabIndex = 17;
            label6.Text = "*Password that is used by all employees to gain access to the platform";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label7.Location = new Point(54, 438);
            label7.Name = "label7";
            label7.Size = new Size(171, 24);
            label7.TabIndex = 21;
            label7.Text = "Manager password";
            // 
            // txt_mpassword
            // 
            txt_mpassword.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_mpassword.Location = new Point(59, 468);
            txt_mpassword.Name = "txt_mpassword";
            txt_mpassword.PasswordChar = '*';
            txt_mpassword.Size = new Size(310, 28);
            txt_mpassword.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label8.Location = new Point(54, 362);
            label8.Name = "label8";
            label8.Size = new Size(175, 24);
            label8.TabIndex = 19;
            label8.Text = "Manager username";
            // 
            // txt_mname
            // 
            txt_mname.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_mname.Location = new Point(59, 392);
            txt_mname.Name = "txt_mname";
            txt_mname.Size = new Size(310, 28);
            txt_mname.TabIndex = 18;
            // 
            // RestaurantRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(422, 612);
            Controls.Add(label7);
            Controls.Add(txt_mpassword);
            Controls.Add(label8);
            Controls.Add(txt_mname);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txt_W);
            Controls.Add(txt_L);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txt_rpassword);
            Controls.Add(label1);
            Controls.Add(txt_rname);
            Controls.Add(button1);
            Name = "RestaurantRegister";
            Text = "RestaurantRegister";
            Load += RestaurantRegister_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_rpassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_rname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_L;
        private System.Windows.Forms.TextBox txt_W;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_mpassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_mname;
    }
}