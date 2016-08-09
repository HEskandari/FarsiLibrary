using DevExpress.XtraEditors;
using FarsiLibrary.Win.DevExpress;

namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DevExpressEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevExpressEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDatePickerValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDateEditValue = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTouchUIValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.xtraFADateEdit1 = new FarsiLibrary.Win.DevExpress.XtraFADateEdit();
            this.xtraFADatePicker1 = new FarsiLibrary.Win.DevExpress.XtraFADatePicker();
            this.dateEdit1 = new FarsiLibrary.Win.DevExpress.XtraFADateEdit();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADatePicker1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(739, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date Picker : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "EditValue : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDatePickerValue
            // 
            this.lblDatePickerValue.Location = new System.Drawing.Point(152, 27);
            this.lblDatePickerValue.Name = "lblDatePickerValue";
            this.lblDatePickerValue.Size = new System.Drawing.Size(202, 23);
            this.lblDatePickerValue.TabIndex = 4;
            this.lblDatePickerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "DevExpress Date Picker : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(80, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "EditValue : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDateEditValue
            // 
            this.lblDateEditValue.Location = new System.Drawing.Point(152, 91);
            this.lblDateEditValue.Name = "lblDateEditValue";
            this.lblDateEditValue.Size = new System.Drawing.Size(202, 23);
            this.lblDateEditValue.TabIndex = 8;
            this.lblDateEditValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dateEdit2);
            this.splitContainer1.Panel1.Controls.Add(this.lblTouchUIValue);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.xtraFADateEdit1);
            this.splitContainer1.Panel1.Controls.Add(this.xtraFADatePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.lblDateEditValue);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.lblDatePickerValue);
            this.splitContainer1.Panel1.Controls.Add(this.dateEdit1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(733, 467);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 10;
            // 
            // lblTouchUIValue
            // 
            this.lblTouchUIValue.Location = new System.Drawing.Point(152, 159);
            this.lblTouchUIValue.Name = "lblTouchUIValue";
            this.lblTouchUIValue.Size = new System.Drawing.Size(202, 23);
            this.lblTouchUIValue.TabIndex = 12;
            this.lblTouchUIValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(80, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 23);
            this.label8.TabIndex = 11;
            this.label8.Text = "EditValue : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "DevExpress TouchUI : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xtraFADateEdit1
            // 
            this.xtraFADateEdit1.EditValue = null;
            this.xtraFADateEdit1.Location = new System.Drawing.Point(152, 138);
            this.xtraFADateEdit1.Name = "xtraFADateEdit1";
            this.xtraFADateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xtraFADateEdit1.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.xtraFADateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xtraFADateEdit1.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.xtraFADateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.xtraFADateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.xtraFADateEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.xtraFADateEdit1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.xtraFADateEdit1.Size = new System.Drawing.Size(202, 20);
            this.xtraFADateEdit1.TabIndex = 9;
            this.xtraFADateEdit1.EditValueChanged += new System.EventHandler(this.xtraFADateEdit1_EditValueChanged);
            // 
            // xtraFADatePicker1
            // 
            this.xtraFADatePicker1.Location = new System.Drawing.Point(152, 6);
            this.xtraFADatePicker1.Name = "xtraFADatePicker1";
            this.xtraFADatePicker1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xtraFADatePicker1.Properties.LookAndFeel.SkinName = "Blue";
            this.xtraFADatePicker1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraFADatePicker1.Properties.PopupSizeable = false;
            this.xtraFADatePicker1.Properties.ShowPopupCloseButton = false;
            this.xtraFADatePicker1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.xtraFADatePicker1.Size = new System.Drawing.Size(202, 20);
            this.xtraFADatePicker1.TabIndex = 0;
            this.xtraFADatePicker1.EditValueChanged += new System.EventHandler(this.xtraFADatePicker1_EditValueChanged);
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(152, 70);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Size = new System.Drawing.Size(202, 20);
            this.dateEdit1.TabIndex = 5;
            this.dateEdit1.EditValueChanged += new System.EventHandler(this.dateEdit1_EditValueChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.dateEdit1;
            this.propertyGrid1.Size = new System.Drawing.Size(273, 467);
            this.propertyGrid1.TabIndex = 9;
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(152, 214);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Size = new System.Drawing.Size(202, 20);
            this.dateEdit2.TabIndex = 13;
            // 
            // DevExpressEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.IsNew = true;
            this.Name = "DevExpressEditor";
            this.Size = new System.Drawing.Size(739, 513);
            this.Title = "DevExpress Custom Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADatePicker1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XtraFADatePicker xtraFADatePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDatePickerValue;
        private XtraFADateEdit dateEdit1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDateEditValue;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblTouchUIValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private XtraFADateEdit xtraFADateEdit1;
        private DateEdit dateEdit2;
    }
}
