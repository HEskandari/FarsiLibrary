namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DateTimeFormat
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
            this.lblDateTimeToString = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMinDateTime = new System.Windows.Forms.Label();
            this.lblMaxDateTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDayNames = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMonthNames = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "DateTime ToString : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDateTimeToString
            // 
            this.lblDateTimeToString.Location = new System.Drawing.Point(183, 12);
            this.lblDateTimeToString.Name = "lblDateTimeToString";
            this.lblDateTimeToString.Size = new System.Drawing.Size(342, 19);
            this.lblDateTimeToString.TabIndex = 1;
            this.lblDateTimeToString.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Min Supported DateTime : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Supported DateTime : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMinDateTime
            // 
            this.lblMinDateTime.Location = new System.Drawing.Point(183, 32);
            this.lblMinDateTime.Name = "lblMinDateTime";
            this.lblMinDateTime.Size = new System.Drawing.Size(342, 19);
            this.lblMinDateTime.TabIndex = 4;
            this.lblMinDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaxDateTime
            // 
            this.lblMaxDateTime.Location = new System.Drawing.Point(183, 51);
            this.lblMaxDateTime.Name = "lblMaxDateTime";
            this.lblMaxDateTime.Size = new System.Drawing.Size(342, 19);
            this.lblMaxDateTime.TabIndex = 5;
            this.lblMaxDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Day Names : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDayNames
            // 
            this.lblDayNames.Location = new System.Drawing.Point(183, 71);
            this.lblDayNames.Name = "lblDayNames";
            this.lblDayNames.Size = new System.Drawing.Size(342, 19);
            this.lblDayNames.TabIndex = 7;
            this.lblDayNames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Month Names : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMonthNames
            // 
            this.lblMonthNames.Location = new System.Drawing.Point(183, 90);
            this.lblMonthNames.Name = "lblMonthNames";
            this.lblMonthNames.Size = new System.Drawing.Size(342, 19);
            this.lblMonthNames.TabIndex = 9;
            this.lblMonthNames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateTimeFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMonthNames);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDayNames);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMaxDateTime);
            this.Controls.Add(this.lblMinDateTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDateTimeToString);
            this.Controls.Add(this.label1);
            this.Name = "DateTimeFormat";
            this.Size = new System.Drawing.Size(535, 119);
            this.Title = "DateTimeFormat Info";
            this.Load += new System.EventHandler(this.frm21_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDateTimeToString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMinDateTime;
        private System.Windows.Forms.Label lblMaxDateTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDayNames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMonthNames;

    }
}