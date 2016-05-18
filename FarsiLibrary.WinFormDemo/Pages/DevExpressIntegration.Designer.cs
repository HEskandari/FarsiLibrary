using FarsiLibrary.Win.DevExpress;

namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DevExpressIntegration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevExpressIntegration));
            this.xtraFADatePicker1 = new XtraFADatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEditValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADatePicker1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraFADatePicker1
            // 
            this.xtraFADatePicker1.Location = new System.Drawing.Point(132, 107);
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
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(390, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date Picker : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(26, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "EditValue : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditValue
            // 
            this.lblEditValue.Location = new System.Drawing.Point(132, 82);
            this.lblEditValue.Name = "lblEditValue";
            this.lblEditValue.Size = new System.Drawing.Size(202, 23);
            this.lblEditValue.TabIndex = 4;
            this.lblEditValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DevExpressIntegration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblEditValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xtraFADatePicker1);
            this.IsNew = true;
            this.Name = "DevExpressIntegration";
            this.Size = new System.Drawing.Size(390, 147);
            this.Title = "DevExpress Integration";
            ((System.ComponentModel.ISupportInitialize)(this.xtraFADatePicker1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XtraFADatePicker xtraFADatePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEditValue;
    }
}
