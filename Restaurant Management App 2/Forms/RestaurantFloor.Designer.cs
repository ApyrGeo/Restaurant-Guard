namespace Restaurant_Management_App
{
    partial class RestaurantFloor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ocupyTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProductsToOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 615);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ocupyTableToolStripMenuItem,
            this.addProductsToOrderToolStripMenuItem,
            this.viewOrderToolStripMenuItem,
            this.emptyTableToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip_empty";
            this.contextMenuStrip.Size = new System.Drawing.Size(227, 128);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_empty_Opening);
            // 
            // ocupyTableToolStripMenuItem
            // 
            this.ocupyTableToolStripMenuItem.Name = "ocupyTableToolStripMenuItem";
            this.ocupyTableToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.ocupyTableToolStripMenuItem.Text = "Ocupy table";
            this.ocupyTableToolStripMenuItem.Click += new System.EventHandler(this.ocupyTableToolStripMenuItem_Click);
            // 
            // addProductsToOrderToolStripMenuItem
            // 
            this.addProductsToOrderToolStripMenuItem.Name = "addProductsToOrderToolStripMenuItem";
            this.addProductsToOrderToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
            this.addProductsToOrderToolStripMenuItem.Text = "Add products to order";
            this.addProductsToOrderToolStripMenuItem.Click += new System.EventHandler(this.addProductsToOrderToolStripMenuItem_Click);
            // 
            // viewOrderToolStripMenuItem
            // 
            this.viewOrderToolStripMenuItem.Name = "viewOrderToolStripMenuItem";
            this.viewOrderToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
            this.viewOrderToolStripMenuItem.Text = "View order";
            this.viewOrderToolStripMenuItem.Click += new System.EventHandler(this.viewOrderToolStripMenuItem_Click);
            // 
            // emptyTableToolStripMenuItem
            // 
            this.emptyTableToolStripMenuItem.Name = "emptyTableToolStripMenuItem";
            this.emptyTableToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
            this.emptyTableToolStripMenuItem.Text = "Empty table";
            this.emptyTableToolStripMenuItem.Click += new System.EventHandler(this.emptyTableToolStripMenuItem_Click);
            // 
            // RestaurantFloor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 615);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RestaurantFloor";
            this.Text = "RestaurantFloor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ocupyTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProductsToOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyTableToolStripMenuItem;
    }
}