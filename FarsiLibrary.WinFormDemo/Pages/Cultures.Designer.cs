namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class Cultures
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
            this.btnFarsi = new System.Windows.Forms.Button();
            this.btnArabic = new System.Windows.Forms.Button();
            this.btnInvariant = new System.Windows.Forms.Button();
            this.faMonthView = new FarsiLibrary.Win.Controls.FAMonthView();
            this.btnDefault = new System.Windows.Forms.Button();
            this.faDatePicker1 = new FarsiLibrary.Win.Controls.FADatePicker();
            this.SuspendLayout();
            // 
            // btnFarsi
            // 
            this.btnFarsi.Location = new System.Drawing.Point(188, 12);
            this.btnFarsi.Name = "btnFarsi";
            this.btnFarsi.Size = new System.Drawing.Size(149, 23);
            this.btnFarsi.TabIndex = 1;
            this.btnFarsi.Text = "Farsi Culture";
            this.btnFarsi.UseVisualStyleBackColor = true;
            this.btnFarsi.Click += new System.EventHandler(this.btnFarsi_Click);
            // 
            // btnArabic
            // 
            this.btnArabic.Location = new System.Drawing.Point(188, 41);
            this.btnArabic.Name = "btnArabic";
            this.btnArabic.Size = new System.Drawing.Size(149, 23);
            this.btnArabic.TabIndex = 2;
            this.btnArabic.Text = "Arabic Culture";
            this.btnArabic.UseVisualStyleBackColor = true;
            this.btnArabic.Click += new System.EventHandler(this.btnArabic_Click);
            // 
            // btnInvariant
            // 
            this.btnInvariant.Location = new System.Drawing.Point(188, 70);
            this.btnInvariant.Name = "btnInvariant";
            this.btnInvariant.Size = new System.Drawing.Size(149, 23);
            this.btnInvariant.TabIndex = 3;
            this.btnInvariant.Text = "Invariant Culture";
            this.btnInvariant.UseVisualStyleBackColor = true;
            this.btnInvariant.Click += new System.EventHandler(this.btnInvariant_Click);
            // 
            // faMonthView
            // 
            this.faMonthView.Location = new System.Drawing.Point(10, 29);
            this.faMonthView.Name = "faMonthView";
            this.faMonthView.SelectedDateTime = new System.DateTime(2006, 5, 24, 16, 34, 43, 67);
            this.faMonthView.TabIndex = 0;
            this.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(188, 172);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(149, 23);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "System Default Culture";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // faDatePicker1
            // 
            this.faDatePicker1.Location = new System.Drawing.Point(10, 5);
            this.faDatePicker1.Name = "faDatePicker1";
            this.faDatePicker1.SelectedDateTime = null;
            this.faDatePicker1.Size = new System.Drawing.Size(166, 19);
            this.faDatePicker1.TabIndex = 5;
            this.faDatePicker1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            // 
            // Cultures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faDatePicker1);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnInvariant);
            this.Controls.Add(this.btnArabic);
            this.Controls.Add(this.btnFarsi);
            this.Controls.Add(this.faMonthView);
            this.Name = "Cultures";
            this.Size = new System.Drawing.Size(364, 204);
            this.Title = "Persian, Arabic and Invariant Cultures";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMonthView faMonthView;
        private System.Windows.Forms.Button btnFarsi;
        private System.Windows.Forms.Button btnArabic;
        private System.Windows.Forms.Button btnInvariant;
        private System.Windows.Forms.Button btnDefault;
        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker1;
    }
}