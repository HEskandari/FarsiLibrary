namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class ScrollingOptions
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
            this.rbDays = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbYear = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.faMonthView1 = new FarsiLibrary.Win.Controls.FAMonthView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose scrolling option and use your Mouse Wheen when the mouse is over the contr" +
                "ol.";
            // 
            // rbDays
            // 
            this.rbDays.AutoSize = true;
            this.rbDays.Checked = true;
            this.rbDays.Location = new System.Drawing.Point(18, 19);
            this.rbDays.Name = "rbDays";
            this.rbDays.Size = new System.Drawing.Size(78, 17);
            this.rbDays.TabIndex = 1;
            this.rbDays.TabStop = true;
            this.rbDays.Text = "Scroll Days";
            this.rbDays.UseVisualStyleBackColor = true;
            this.rbDays.CheckedChanged += new System.EventHandler(this.rbDays_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbYear);
            this.groupBox1.Controls.Add(this.rbMonth);
            this.groupBox1.Controls.Add(this.rbDays);
            this.groupBox1.Location = new System.Drawing.Point(182, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 92);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scrolling Options ";
            // 
            // rbYear
            // 
            this.rbYear.AutoSize = true;
            this.rbYear.Location = new System.Drawing.Point(18, 64);
            this.rbYear.Name = "rbYear";
            this.rbYear.Size = new System.Drawing.Size(81, 17);
            this.rbYear.TabIndex = 3;
            this.rbYear.Text = "Scroll Years";
            this.rbYear.UseVisualStyleBackColor = true;
            this.rbYear.CheckedChanged += new System.EventHandler(this.rbYear_CheckedChanged);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(18, 42);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(89, 17);
            this.rbMonth.TabIndex = 2;
            this.rbMonth.Text = "Scroll Months";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // faMonthView1
            // 
            this.faMonthView1.Location = new System.Drawing.Point(10, 53);
            this.faMonthView1.Name = "faMonthView1";
            this.faMonthView1.ScrollOption = FarsiLibrary.Win.Enums.ScrollOptionTypes.Day;
            this.faMonthView1.SelectedDateTime = new System.DateTime(2006, 6, 1, 13, 30, 31, 850);
            this.faMonthView1.TabIndex = 3;
            // 
            // ScrollingOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faMonthView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "ScrollingOptions";
            this.Size = new System.Drawing.Size(335, 228);
            this.Title = "MonthView Scrolling Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDays;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarsiLibrary.Win.Controls.FAMonthView faMonthView1;
        private System.Windows.Forms.RadioButton rbYear;
        private System.Windows.Forms.RadioButton rbMonth;
    }
}