using System;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class Events
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
            this.faDatePicker = new FarsiLibrary.Win.Controls.FADatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.listEvents = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.btnDatePicker = new System.Windows.Forms.Button();
            this.btnMonthView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // faMonthView
            // 
            this.faMonthView.Location = new System.Drawing.Point(10, 12);
            this.faMonthView.Name = "faMonthView";
            this.faMonthView.SelectedDateTime = new System.DateTime(2006, 5, 24, 15, 55, 27, 49);
            this.faMonthView.TabIndex = 0;
            this.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faMonthView.ViewYearChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_ViewYearChanged);
            this.faMonthView.SelectedDayChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_SelectedDayChanged);
            this.faMonthView.ViewDateTimeChanged += new System.EventHandler(this.faMonthView_ViewDateTimeChanged);
            this.faMonthView.SelectedMonthChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_SelectedMonthChanged);
            this.faMonthView.SelectedYearChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_SelectedYearChanged);
            this.faMonthView.ViewDayChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_ViewDayChanged);
            this.faMonthView.SelectedDateTimeChanged += new System.EventHandler(this.faMonthView_SelectedDateTimeChanged);
            this.faMonthView.SelectedDateRangeChanged += new System.EventHandler<FarsiLibrary.Win.Events.SelectedDateRangeChangedEventArgs>(this.faMonthView_SelectedDateRangeChanged);
            this.faMonthView.ThemeChanged += new System.EventHandler(this.faMonthView_ThemeChanged);
            this.faMonthView.ViewMonthChanged += new System.EventHandler<FarsiLibrary.Win.Events.DateChangedEventArgs>(this.faMonthView_ViewMonthChanged);
            // 
            // faDatePicker
            // 
            this.faDatePicker.Location = new System.Drawing.Point(10, 184);
            this.faDatePicker.Name = "faDatePicker";
            this.faDatePicker.SelectedDateTime = null;
            this.faDatePicker.Size = new System.Drawing.Size(166, 20);
            this.faDatePicker.TabIndex = 1;
            this.faDatePicker.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP;
            this.faDatePicker.ValueValidating += new FarsiLibrary.Win.Events.ValueValidatingEventHandler(this.faDatePicker_ValueValidating);
            this.faDatePicker.SelectedDateTimeChanging += new FarsiLibrary.Win.Events.SelectedDateTimeChangingEventHandler(this.faDatePicker_SelectedDateTimeChanging);
            this.faDatePicker.RightToLeftChanged += new System.EventHandler(this.faDatePicker_RightToLeftChanged);
            this.faDatePicker.SelectedDateTimeChanged += new System.EventHandler(this.faDatePicker_SelectedDateTimeChanged);
            this.faDatePicker.ValueChanged += new System.EventHandler(this.faDatePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(184, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Events : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listEvents
            // 
            this.listEvents.FormattingEnabled = true;
            this.listEvents.Location = new System.Drawing.Point(187, 38);
            this.listEvents.Name = "listEvents";
            this.listEvents.Size = new System.Drawing.Size(348, 290);
            this.listEvents.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(460, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(544, 12);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this.faMonthView;
            this.propertyGrid.Size = new System.Drawing.Size(252, 316);
            this.propertyGrid.TabIndex = 6;
            // 
            // btnDatePicker
            // 
            this.btnDatePicker.Location = new System.Drawing.Point(10, 304);
            this.btnDatePicker.Name = "btnDatePicker";
            this.btnDatePicker.Size = new System.Drawing.Size(166, 23);
            this.btnDatePicker.TabIndex = 7;
            this.btnDatePicker.Text = "Monitor FADatePicker";
            this.btnDatePicker.UseVisualStyleBackColor = true;
            this.btnDatePicker.Click += new System.EventHandler(this.btnDatePicker_Click);
            // 
            // btnMonthView
            // 
            this.btnMonthView.Location = new System.Drawing.Point(10, 275);
            this.btnMonthView.Name = "btnMonthView";
            this.btnMonthView.Size = new System.Drawing.Size(166, 23);
            this.btnMonthView.TabIndex = 9;
            this.btnMonthView.Text = "Monitor FAMonthView";
            this.btnMonthView.UseVisualStyleBackColor = true;
            this.btnMonthView.Click += new System.EventHandler(this.btnMonthView_Click);
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMonthView);
            this.Controls.Add(this.btnDatePicker);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.listEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.faDatePicker);
            this.Controls.Add(this.faMonthView);
            this.Name = "Events";
            this.Size = new System.Drawing.Size(805, 337);
            this.Title = "Using Control Events";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMonthView faMonthView;
        private FarsiLibrary.Win.Controls.FADatePicker faDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listEvents;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button btnDatePicker;
        private System.Windows.Forms.Button btnMonthView;
    }
}