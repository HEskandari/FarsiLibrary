using System;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class MultiSelection
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
            this.faMonthView = new FarsiLibrary.Win.Controls.FAMonthView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnChangeSelectionMode = new System.Windows.Forms.Button();
            this.btnSelectDays = new System.Windows.Forms.Button();
            this.btnSelectMonth = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnSetNull = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // faMonthView
            // 
            this.faMonthView.IsMultiSelect = true;
            this.faMonthView.Location = new System.Drawing.Point(10, 11);
            this.faMonthView.Name = "faMonthView";
            this.faMonthView.SelectedDateTime = new System.DateTime(2006, 5, 25, 10, 20, 34, 380);
            this.faMonthView.TabIndex = 6;
            this.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faMonthView.SelectedDateTimeChanged += new System.EventHandler(this.faMonthView_SelectedDateTimeChanged);
            this.faMonthView.SelectedDateRangeChanged += new System.EventHandler<FarsiLibrary.Win.Events.SelectedDateRangeChangedEventArgs>(this.faMonthView_SelectedDateRangeChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(182, 45);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(221, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear Selection";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnChangeSelectionMode
            // 
            this.btnChangeSelectionMode.Location = new System.Drawing.Point(182, 16);
            this.btnChangeSelectionMode.Name = "btnChangeSelectionMode";
            this.btnChangeSelectionMode.Size = new System.Drawing.Size(221, 23);
            this.btnChangeSelectionMode.TabIndex = 9;
            this.btnChangeSelectionMode.Text = "Change Selection Mode";
            this.btnChangeSelectionMode.UseVisualStyleBackColor = true;
            this.btnChangeSelectionMode.Click += new System.EventHandler(this.btnChangeSelectionMode_Click);
            // 
            // btnSelectDays
            // 
            this.btnSelectDays.Location = new System.Drawing.Point(182, 102);
            this.btnSelectDays.Name = "btnSelectDays";
            this.btnSelectDays.Size = new System.Drawing.Size(221, 23);
            this.btnSelectDays.TabIndex = 10;
            this.btnSelectDays.Text = "Select Some Days";
            this.btnSelectDays.UseVisualStyleBackColor = true;
            this.btnSelectDays.Click += new System.EventHandler(this.btnSelectDays_Click);
            // 
            // btnSelectMonth
            // 
            this.btnSelectMonth.Location = new System.Drawing.Point(182, 131);
            this.btnSelectMonth.Name = "btnSelectMonth";
            this.btnSelectMonth.Size = new System.Drawing.Size(221, 23);
            this.btnSelectMonth.TabIndex = 11;
            this.btnSelectMonth.Text = "Select The Whole Month";
            this.btnSelectMonth.UseVisualStyleBackColor = true;
            this.btnSelectMonth.Click += new System.EventHandler(this.btnSelectMonth_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(10, 199);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(393, 101);
            this.textBox1.TabIndex = 12;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(10, 170);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(364, 23);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetNull
            // 
            this.btnSetNull.Location = new System.Drawing.Point(182, 73);
            this.btnSetNull.Name = "btnSetNull";
            this.btnSetNull.Size = new System.Drawing.Size(221, 23);
            this.btnSetNull.TabIndex = 14;
            this.btnSetNull.Text = "Set Null";
            this.btnSetNull.UseVisualStyleBackColor = true;
            this.btnSetNull.Click += new System.EventHandler(this.btnSetNull_Click);
            // 
            // MultiSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSetNull);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSelectMonth);
            this.Controls.Add(this.btnSelectDays);
            this.Controls.Add(this.btnChangeSelectionMode);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.faMonthView);
            this.Name = "MultiSelection";
            this.Size = new System.Drawing.Size(413, 311);
            this.Title = "Multi Date Selection";
            this.Load += new System.EventHandler(this.frm20_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMonthView faMonthView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnChangeSelectionMode;
        private System.Windows.Forms.Button btnSelectDays;
        private System.Windows.Forms.Button btnSelectMonth;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnSetNull;
    }
}