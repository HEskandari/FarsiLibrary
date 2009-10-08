namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DayCustomDrawing
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
            this.label1 = new System.Windows.Forms.Label();
            this.faMonthView = new FarsiLibrary.Win.Controls.FAMonthView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "We\'re custom drawing 1384/04/08 date plus each first day of the new year (Nowrooz" +
                ").";
            // 
            // faMonthView
            // 
            this.faMonthView.Location = new System.Drawing.Point(13, 46);
            this.faMonthView.Name = "faMonthView";
            this.faMonthView.SelectedDateTime = new System.DateTime(2006, 6, 6, 13, 49, 30, 658);
            this.faMonthView.TabIndex = 1;
            this.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faMonthView.DrawCurrentDay += new FarsiLibrary.Win.Events.CustomDrawDayEventHandler(this.faMonthView_DrawCurrentDay);
            // 
            // DayCustomDrawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faMonthView);
            this.Controls.Add(this.label1);
            this.Name = "DayCustomDrawing";
            this.Size = new System.Drawing.Size(292, 266);
            this.Title = "Day CustomDraw";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FarsiLibrary.Win.Controls.FAMonthView faMonthView;
    }
}