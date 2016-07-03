namespace FarsiLibrary.WinFormDemo
{
    partial class MainWinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWinForm));
            this.splitContainerView = new System.Windows.Forms.SplitContainer();
            this.listBoxDemos = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonShow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerView)).BeginInit();
            this.splitContainerView.Panel1.SuspendLayout();
            this.splitContainerView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerView
            // 
            this.splitContainerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerView.IsSplitterFixed = true;
            this.splitContainerView.Location = new System.Drawing.Point(5, 43);
            this.splitContainerView.Name = "splitContainerView";
            // 
            // splitContainerView.Panel1
            // 
            this.splitContainerView.Panel1.Controls.Add(this.listBoxDemos);
            // 
            // splitContainerView.Panel2
            // 
            this.splitContainerView.Panel2.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainerView.Size = new System.Drawing.Size(842, 508);
            this.splitContainerView.SplitterDistance = 227;
            this.splitContainerView.TabIndex = 25;
            // 
            // listBoxDemos
            // 
            this.listBoxDemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDemos.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxDemos.FormattingEnabled = true;
            this.listBoxDemos.IntegralHeight = false;
            this.listBoxDemos.Location = new System.Drawing.Point(0, 0);
            this.listBoxDemos.Name = "listBoxDemos";
            this.listBoxDemos.Size = new System.Drawing.Size(227, 508);
            this.listBoxDemos.TabIndex = 0;
            this.listBoxDemos.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxDemos_DrawItem);
            this.listBoxDemos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDemos_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonShow,
            this.toolStripButtonExit,
            this.toolStripSeparator1,
            this.toolStripButtonAbout});
            this.toolStrip1.Location = new System.Drawing.Point(5, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(842, 38);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonShow
            // 
            this.toolStripButtonShow.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShow.Image")));
            this.toolStripButtonShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShow.Name = "toolStripButtonShow";
            this.toolStripButtonShow.Size = new System.Drawing.Size(32, 35);
            this.toolStripButtonShow.Text = "Run";
            this.toolStripButtonShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonShow.Click += new System.EventHandler(this.toolStripShow_Click);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(29, 35);
            this.toolStripButtonExit.Text = "Exit";
            this.toolStripButtonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(53, 35);
            this.toolStripButtonAbout.Text = "About...";
            this.toolStripButtonAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // MainWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 556);
            this.Controls.Add(this.splitContainerView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainWinForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWinForm";
            this.splitContainerView.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerView)).EndInit();
            this.splitContainerView.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerView;
        private System.Windows.Forms.ListBox listBoxDemos;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShow;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
    }
}