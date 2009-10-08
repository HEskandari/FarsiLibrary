namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DateConversion
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
            this.btnToday = new System.Windows.Forms.Button();
            this.lblTodayPersian = new System.Windows.Forms.Label();
            this.lblTodayGregorian = new System.Windows.Forms.Label();
            this.lblTodayPersianDate = new System.Windows.Forms.Label();
            this.lblPersianDateCtor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDirectCast = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(10, 149);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(104, 23);
            this.btnToday.TabIndex = 0;
            this.btnToday.Text = "Show";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // lblTodayPersian
            // 
            this.lblTodayPersian.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblTodayPersian.Location = new System.Drawing.Point(395, 42);
            this.lblTodayPersian.Name = "lblTodayPersian";
            this.lblTodayPersian.Size = new System.Drawing.Size(194, 23);
            this.lblTodayPersian.TabIndex = 1;
            this.lblTodayPersian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTodayGregorian
            // 
            this.lblTodayGregorian.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblTodayGregorian.Location = new System.Drawing.Point(395, 19);
            this.lblTodayGregorian.Name = "lblTodayGregorian";
            this.lblTodayGregorian.Size = new System.Drawing.Size(194, 23);
            this.lblTodayGregorian.TabIndex = 2;
            this.lblTodayGregorian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTodayPersianDate
            // 
            this.lblTodayPersianDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblTodayPersianDate.Location = new System.Drawing.Point(395, 65);
            this.lblTodayPersianDate.Name = "lblTodayPersianDate";
            this.lblTodayPersianDate.Size = new System.Drawing.Size(194, 23);
            this.lblTodayPersianDate.TabIndex = 3;
            this.lblTodayPersianDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPersianDateCtor
            // 
            this.lblPersianDateCtor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblPersianDateCtor.Location = new System.Drawing.Point(395, 88);
            this.lblPersianDateCtor.Name = "lblPersianDateCtor";
            this.lblPersianDateCtor.Size = new System.Drawing.Size(194, 23);
            this.lblPersianDateCtor.TabIndex = 4;
            this.lblPersianDateCtor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Displays DateTime.Now.ToString value : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Converts DateTime.Now using PersianDateConverter class : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Displays PersianDate.Now.ToWritten : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.Location = new System.Drawing.Point(10, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(387, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Uses DateTime.Now to construct a new instance of PersianDate : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label5.Location = new System.Drawing.Point(10, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(387, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Direct casting is supported too! : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDirectCast
            // 
            this.lblDirectCast.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblDirectCast.Location = new System.Drawing.Point(395, 111);
            this.lblDirectCast.Name = "lblDirectCast";
            this.lblDirectCast.Size = new System.Drawing.Size(194, 24);
            this.lblDirectCast.TabIndex = 10;
            this.lblDirectCast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DateConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDirectCast);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPersianDateCtor);
            this.Controls.Add(this.lblTodayPersianDate);
            this.Controls.Add(this.lblTodayGregorian);
            this.Controls.Add(this.lblTodayPersian);
            this.Controls.Add(this.btnToday);
            this.Name = "DateConversion";
            this.Size = new System.Drawing.Size(602, 179);
            this.Title = "Date Conversions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Label lblTodayPersian;
        private System.Windows.Forms.Label lblTodayGregorian;
        private System.Windows.Forms.Label lblTodayPersianDate;
        private System.Windows.Forms.Label lblPersianDateCtor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDirectCast;
    }
}