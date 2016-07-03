namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DayViewControl
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
            this.faMonthView1 = new FarsiLibrary.Win.Controls.FAMonthView();
            this.faDayView1 = new FarsiLibrary.Win.Controls.FADayView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // faMonthView1
            // 
            this.faMonthView1.Location = new System.Drawing.Point(10, 43);
            this.faMonthView1.Name = "faMonthView1";
            this.faMonthView1.SelectedDateTime = new System.DateTime(2009, 4, 1, 0, 0, 0, 0);
            this.faMonthView1.TabIndex = 2;
            this.faMonthView1.SelectedDateTimeChanged += new System.EventHandler(this.faMonthView1_SelectedDateTimeChanged);
            // 
            // faDayView1
            // 
            this.faDayView1.Location = new System.Drawing.Point(182, 43);
            this.faDayView1.Name = "faDayView1";
            this.faDayView1.SelectedDateTime = new System.DateTime(2009, 3, 5, 0, 0, 0, 0);
            this.faDayView1.TabIndex = 3;
            this.faDayView1.Text = "faDayView1";
            this.faDayView1.Draw += new FarsiLibrary.Win.Events.CustomDrawEventHandler(this.faDayView1_Draw);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enable / Disable";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(132, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "Show / Hide Today Button";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(252, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 35);
            this.button3.TabIndex = 6;
            this.button3.Text = "Show / Hide Empty Button";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "First of Farvardin (New Year) is custom drawn";
            // 
            // DayViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.faDayView1);
            this.Controls.Add(this.faMonthView1);
            this.Name = "DayViewControl";
            this.Size = new System.Drawing.Size(369, 258);
            this.Title = "DayView Control";
            this.Load += new System.EventHandler(this.frm22_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMonthView faMonthView1;
        private FarsiLibrary.Win.Controls.FADayView faDayView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;


    }
}