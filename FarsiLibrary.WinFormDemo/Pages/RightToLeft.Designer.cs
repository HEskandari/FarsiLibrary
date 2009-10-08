namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class RightToLeft
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
            this.faDatePicker1 = new FarsiLibrary.Win.Controls.FADatePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.faDatePicker2 = new FarsiLibrary.Win.Controls.FADatePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Right To Left : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // faDatePicker1
            // 
            this.faDatePicker1.Location = new System.Drawing.Point(88, 12);
            this.faDatePicker1.Name = "faDatePicker1";
            this.faDatePicker1.SelectedDateTime = null;
            this.faDatePicker1.Size = new System.Drawing.Size(183, 20);
            this.faDatePicker1.TabIndex = 1;
            this.faDatePicker1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.Office2003;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Left To Right : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // faDatePicker2
            // 
            this.faDatePicker2.Location = new System.Drawing.Point(88, 61);
            this.faDatePicker2.Name = "faDatePicker2";
            this.faDatePicker2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.faDatePicker2.SelectedDateTime = null;
            this.faDatePicker2.Size = new System.Drawing.Size(183, 20);
            this.faDatePicker2.TabIndex = 5;
            this.faDatePicker2.Theme = FarsiLibrary.Win.Enums.ThemeTypes.Office2003;
            // 
            // RightToLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faDatePicker2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.faDatePicker1);
            this.Controls.Add(this.label1);
            this.Name = "RightToLeft";
            this.Size = new System.Drawing.Size(336, 96);
            this.Title = "Right To Left";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker1;
        private System.Windows.Forms.Label label3;
        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker2;
    }
}