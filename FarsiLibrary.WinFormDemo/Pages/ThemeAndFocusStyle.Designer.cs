namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class ThemeAndFocusStyle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.faMonthView = new FarsiLibrary.Win.Controls.FAMonthView();
            this.btnToggleFocusRect = new System.Windows.Forms.Button();
            this.btnToggleBorder = new System.Windows.Forms.Button();
            this.btnVisualStyles = new System.Windows.Forms.Button();
            this.btnChangeTheme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // faMonthView
            // 
            this.faMonthView.Location = new System.Drawing.Point(3, 3);
            this.faMonthView.Name = "faMonthView";
            this.faMonthView.SelectedDateTime = new System.DateTime(2006, 5, 25, 10, 20, 34, 380);
            this.faMonthView.TabIndex = 10;
            this.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            // 
            // btnToggleFocusRect
            // 
            this.btnToggleFocusRect.Location = new System.Drawing.Point(175, 144);
            this.btnToggleFocusRect.Name = "btnToggleFocusRect";
            this.btnToggleFocusRect.Size = new System.Drawing.Size(134, 25);
            this.btnToggleFocusRect.TabIndex = 9;
            this.btnToggleFocusRect.Text = "Toggle Focus Rect";
            this.btnToggleFocusRect.UseVisualStyleBackColor = true;
            this.btnToggleFocusRect.Click += new System.EventHandler(this.btnToggleFocusRect_Click);
            // 
            // btnToggleBorder
            // 
            this.btnToggleBorder.Location = new System.Drawing.Point(175, 112);
            this.btnToggleBorder.Name = "btnToggleBorder";
            this.btnToggleBorder.Size = new System.Drawing.Size(134, 25);
            this.btnToggleBorder.TabIndex = 8;
            this.btnToggleBorder.Text = "Toggle Border";
            this.btnToggleBorder.UseVisualStyleBackColor = true;
            this.btnToggleBorder.Click += new System.EventHandler(this.btnToggleBorder_Click);
            // 
            // btnVisualStyles
            // 
            this.btnVisualStyles.Location = new System.Drawing.Point(175, 81);
            this.btnVisualStyles.Name = "btnVisualStyles";
            this.btnVisualStyles.Size = new System.Drawing.Size(134, 25);
            this.btnVisualStyles.TabIndex = 7;
            this.btnVisualStyles.Text = "Toggle VisualStyles";
            this.btnVisualStyles.UseVisualStyleBackColor = true;
            this.btnVisualStyles.Click += new System.EventHandler(this.btnVisualStyles_Click);
            // 
            // btnChangeTheme
            // 
            this.btnChangeTheme.Location = new System.Drawing.Point(175, 3);
            this.btnChangeTheme.Name = "btnChangeTheme";
            this.btnChangeTheme.Size = new System.Drawing.Size(134, 25);
            this.btnChangeTheme.TabIndex = 6;
            this.btnChangeTheme.Text = "Next Theme";
            this.btnChangeTheme.UseVisualStyleBackColor = true;
            this.btnChangeTheme.Click += new System.EventHandler(this.btnChangeTheme_Click);
            // 
            // ThemeAndFocusStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faMonthView);
            this.Controls.Add(this.btnToggleFocusRect);
            this.Controls.Add(this.btnToggleBorder);
            this.Controls.Add(this.btnVisualStyles);
            this.Controls.Add(this.btnChangeTheme);
            this.Name = "ThemeAndFocusStyle";
            this.Size = new System.Drawing.Size(333, 193);
            this.Title = "Theme and Focus Styles";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMonthView faMonthView;
        private System.Windows.Forms.Button btnToggleFocusRect;
        private System.Windows.Forms.Button btnToggleBorder;
        private System.Windows.Forms.Button btnVisualStyles;
        private System.Windows.Forms.Button btnChangeTheme;
    }
}