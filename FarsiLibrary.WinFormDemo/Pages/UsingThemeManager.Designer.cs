namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class UsingThemeManager
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
            this.faMonthView1 = new FarsiLibrary.Win.Controls.FAMonthView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // faDatePicker1
            // 
            this.faDatePicker1.Location = new System.Drawing.Point(37, 85);
            this.faDatePicker1.Name = "faDatePicker1";
            this.faDatePicker1.SelectedDateTime = null;
            this.faDatePicker1.Size = new System.Drawing.Size(166, 19);
            this.faDatePicker1.TabIndex = 11;
            // 
            // faMonthView1
            // 
            this.faMonthView1.Location = new System.Drawing.Point(37, 110);
            this.faMonthView1.Name = "faMonthView1";
            this.faMonthView1.SelectedDateTime = new System.DateTime(2005, 5, 31, 23, 14, 57, 364);
            this.faMonthView1.TabIndex = 12;
            this.faMonthView1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 21);
            this.button1.TabIndex = 13;
            this.button1.Text = "To Office 2000...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.faButton1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(253, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 21);
            this.button2.TabIndex = 14;
            this.button2.Text = "To Windows XP...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.faButton2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(253, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 21);
            this.button3.TabIndex = 15;
            this.button3.Text = "To Office 2003...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.faButton3_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 71);
            this.label1.TabIndex = 16;
            this.label1.Text = "Notice that on changing the theme on this form, all other controls will paint the" +
                "meselved using the specified theme, even if they\'re on another form.";
            // 
            // UsingThemeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.faMonthView1);
            this.Controls.Add(this.faDatePicker1);
            this.Name = "UsingThemeManager";
            this.Size = new System.Drawing.Size(430, 290);
            this.Title = "Global Theme Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker1;
        private FarsiLibrary.Win.Controls.FAMonthView faMonthView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;



    }
}