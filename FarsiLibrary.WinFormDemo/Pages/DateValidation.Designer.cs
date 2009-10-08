namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DateValidation
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
            this.faDatePicker1 = new FarsiLibrary.Win.Controls.FADatePicker();
            this.faDatePicker2 = new FarsiLibrary.Win.Controls.FADatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // faDatePicker1
            // 
            this.faDatePicker1.Location = new System.Drawing.Point(259, 8);
            this.faDatePicker1.Name = "faDatePicker1";
            this.faDatePicker1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.faDatePicker1.SelectedDateTime = null;
            this.faDatePicker1.Size = new System.Drawing.Size(195, 19);
            this.faDatePicker1.TabIndex = 0;
            this.faDatePicker1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faDatePicker1.SelectedDateTimeChanging += new FarsiLibrary.Win.Events.SelectedDateTimeChangingEventHandler(this.faDatePicker1_SelectedDateTimeChanging);
            // 
            // faDatePicker2
            // 
            this.faDatePicker2.Location = new System.Drawing.Point(259, 32);
            this.faDatePicker2.Name = "faDatePicker2";
            this.faDatePicker2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.faDatePicker2.SelectedDateTime = null;
            this.faDatePicker2.Size = new System.Drawing.Size(195, 19);
            this.faDatePicker2.TabIndex = 1;
            this.faDatePicker2.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faDatePicker2.SelectedDateTimeChanging += new FarsiLibrary.Win.Events.SelectedDateTimeChangingEventHandler(this.faDatePicker2_SelectedDateTimeChanging);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(37, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select a date prior to year 2000 : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select 20th Of a Month (Otherwise apply default)  : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select 1385/04/08 : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DateValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.faDatePicker2);
            this.Controls.Add(this.faDatePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DateValidation";
            this.Size = new System.Drawing.Size(483, 91);
            this.Title = "Date Validation";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker1;
        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;





    }
}