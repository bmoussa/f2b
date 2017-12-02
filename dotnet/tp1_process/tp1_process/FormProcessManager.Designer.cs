namespace tp1_process
{
    partial class process_manager
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.LaunchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ballProcessToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.primeProcessToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastBallProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastPrimeProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ballProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primeProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LaunchToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.showProcessToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(341, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // LaunchToolStripMenuItem
            // 
            this.LaunchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ballProcessToolStripMenuItem1,
            this.primeProcessToolStripMenuItem1});
            this.LaunchToolStripMenuItem.Name = "LaunchToolStripMenuItem";
            this.LaunchToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.LaunchToolStripMenuItem.Text = "Launch";
            this.LaunchToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // ballProcessToolStripMenuItem1
            // 
            this.ballProcessToolStripMenuItem1.Name = "ballProcessToolStripMenuItem1";
            this.ballProcessToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.ballProcessToolStripMenuItem1.Text = "Ball process";
            this.ballProcessToolStripMenuItem1.Click += new System.EventHandler(this.ballProcessToolStripMenuItem1_Click);
            // 
            // primeProcessToolStripMenuItem1
            // 
            this.primeProcessToolStripMenuItem1.Name = "primeProcessToolStripMenuItem1";
            this.primeProcessToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.primeProcessToolStripMenuItem1.Text = "Prime process";
            this.primeProcessToolStripMenuItem1.Click += new System.EventHandler(this.primeProcessToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastProcessToolStripMenuItem,
            this.lastBallProcessToolStripMenuItem,
            this.lastPrimeProcessToolStripMenuItem,
            this.allProcessesToolStripMenuItem});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // lastProcessToolStripMenuItem
            // 
            this.lastProcessToolStripMenuItem.Name = "lastProcessToolStripMenuItem";
            this.lastProcessToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.lastProcessToolStripMenuItem.Text = "Last process";
            // 
            // lastBallProcessToolStripMenuItem
            // 
            this.lastBallProcessToolStripMenuItem.Name = "lastBallProcessToolStripMenuItem";
            this.lastBallProcessToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.lastBallProcessToolStripMenuItem.Text = "Last ball process";
            // 
            // lastPrimeProcessToolStripMenuItem
            // 
            this.lastPrimeProcessToolStripMenuItem.Name = "lastPrimeProcessToolStripMenuItem";
            this.lastPrimeProcessToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.lastPrimeProcessToolStripMenuItem.Text = "Last prime process";
            // 
            // allProcessesToolStripMenuItem
            // 
            this.allProcessesToolStripMenuItem.Name = "allProcessesToolStripMenuItem";
            this.allProcessesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.allProcessesToolStripMenuItem.Text = "All processes";
            // 
            // showProcessToolStripMenuItem
            // 
            this.showProcessToolStripMenuItem.Name = "showProcessToolStripMenuItem";
            this.showProcessToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.showProcessToolStripMenuItem.Text = "Show Process";
            this.showProcessToolStripMenuItem.Click += new System.EventHandler(this.showProcessToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(0, 47);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(341, 329);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // ballProcessToolStripMenuItem
            // 
            this.ballProcessToolStripMenuItem.Name = "ballProcessToolStripMenuItem";
            this.ballProcessToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ballProcessToolStripMenuItem.Text = "Ball process";
            // 
            // primeProcessToolStripMenuItem
            // 
            this.primeProcessToolStripMenuItem.Name = "primeProcessToolStripMenuItem";
            this.primeProcessToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.primeProcessToolStripMenuItem.Text = "Prime process";
            // 
            // process_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 377);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "process_manager";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastBallProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastPrimeProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allProcessesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem LaunchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ballProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primeProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ballProcessToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem primeProcessToolStripMenuItem1;
    }
}

