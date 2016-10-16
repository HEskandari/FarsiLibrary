using FarsiLibrary.Win.DevExpress;

namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DevExpressCalendar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.calendarControl1 = new FarsiLibrary.Win.DevExpress.XtraFACalendarControl();
            this.calendarControl2 = new DevExpress.XtraEditors.Controls.CalendarControl();
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl2.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(707, 60);
            this.label2.TabIndex = 2;
            this.label2.Text = "DevExpres Calendar Controls";
            // 
            // calendarControl1
            // 
            this.calendarControl1.AllowAnimatedContentChange = true;
            this.calendarControl1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calendarControl1.Location = new System.Drawing.Point(85, 98);
            this.calendarControl1.Name = "calendarControl1";
            this.calendarControl1.Size = new System.Drawing.Size(274, 227);
            this.calendarControl1.TabIndex = 3;
            // 
            // calendarControl2
            // 
            this.calendarControl2.AllowAnimatedContentChange = true;
            this.calendarControl2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calendarControl2.Location = new System.Drawing.Point(406, 98);
            this.calendarControl2.Name = "calendarControl2";
            this.calendarControl2.Size = new System.Drawing.Size(235, 227);
            this.calendarControl2.TabIndex = 4;
            // 
            // DevExpressCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.calendarControl2);
            this.Controls.Add(this.calendarControl1);
            this.Controls.Add(this.label2);
            this.IsNew = true;
            this.Name = "DevExpressCalendar";
            this.Size = new System.Drawing.Size(713, 556);
            this.Title = "DevExpress Calendar Controls";
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl2.CalendarTimeProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private XtraFACalendarControl calendarControl1;
        private DevExpress.XtraEditors.Controls.CalendarControl calendarControl2;
    }
}
