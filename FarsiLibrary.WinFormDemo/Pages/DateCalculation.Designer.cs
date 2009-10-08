namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DateCalculation
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnAddYears = new System.Windows.Forms.Button();
            this.btnAddDays = new System.Windows.Forms.Button();
            this.btnDeductMonth = new System.Windows.Forms.Button();
            this.lblToWritten = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(5, 153);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(263, 23);
            this.btnToday.TabIndex = 0;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(3, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(266, 23);
            this.lblMessage.TabIndex = 1;
            // 
            // btnAddYears
            // 
            this.btnAddYears.Location = new System.Drawing.Point(4, 124);
            this.btnAddYears.Name = "btnAddYears";
            this.btnAddYears.Size = new System.Drawing.Size(263, 23);
            this.btnAddYears.TabIndex = 2;
            this.btnAddYears.Text = "Add 10 Years to Today";
            this.btnAddYears.UseVisualStyleBackColor = true;
            this.btnAddYears.Click += new System.EventHandler(this.btnAddYears_Click);
            // 
            // btnAddDays
            // 
            this.btnAddDays.Location = new System.Drawing.Point(4, 96);
            this.btnAddDays.Name = "btnAddDays";
            this.btnAddDays.Size = new System.Drawing.Size(263, 23);
            this.btnAddDays.TabIndex = 3;
            this.btnAddDays.Text = "Deduct 66 Days from Today";
            this.btnAddDays.UseVisualStyleBackColor = true;
            this.btnAddDays.Click += new System.EventHandler(this.btnDeductDays);
            // 
            // btnDeductMonth
            // 
            this.btnDeductMonth.Location = new System.Drawing.Point(5, 67);
            this.btnDeductMonth.Name = "btnDeductMonth";
            this.btnDeductMonth.Size = new System.Drawing.Size(263, 23);
            this.btnDeductMonth.TabIndex = 4;
            this.btnDeductMonth.Text = "Add 2 Months to Today";
            this.btnDeductMonth.UseVisualStyleBackColor = true;
            this.btnDeductMonth.Click += new System.EventHandler(this.btnAddMonth_Click);
            // 
            // lblToWritten
            // 
            this.lblToWritten.Location = new System.Drawing.Point(3, 32);
            this.lblToWritten.Name = "lblToWritten";
            this.lblToWritten.Size = new System.Drawing.Size(266, 23);
            this.lblToWritten.TabIndex = 5;
            // 
            // DateCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblToWritten);
            this.Controls.Add(this.btnDeductMonth);
            this.Controls.Add(this.btnAddDays);
            this.Controls.Add(this.btnAddYears);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnToday);
            this.Name = "DateCalculation";
            this.Size = new System.Drawing.Size(273, 180);
            this.Title = "Date Calculations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnAddYears;
        private System.Windows.Forms.Button btnAddDays;
        private System.Windows.Forms.Button btnDeductMonth;
        private System.Windows.Forms.Label lblToWritten;
    }
}