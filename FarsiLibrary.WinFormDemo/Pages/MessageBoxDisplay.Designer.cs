namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class MessageBoxDisplay
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
            this.btnBasicMSG = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnCustom = new System.Windows.Forms.Button();
            this.btnGetBasic = new System.Windows.Forms.Button();
            this.btnGetAdv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBasicMSG
            // 
            this.btnBasicMSG.Location = new System.Drawing.Point(10, 11);
            this.btnBasicMSG.Name = "btnBasicMSG";
            this.btnBasicMSG.Size = new System.Drawing.Size(272, 21);
            this.btnBasicMSG.TabIndex = 0;
            this.btnBasicMSG.Text = "Show Basic MessageBox";
            this.btnBasicMSG.UseVisualStyleBackColor = true;
            this.btnBasicMSG.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(10, 205);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(272, 52);
            this.lblMessage.TabIndex = 1;
            // 
            // btnCustom
            // 
            this.btnCustom.Location = new System.Drawing.Point(10, 38);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(272, 21);
            this.btnCustom.TabIndex = 2;
            this.btnCustom.Text = "Use Advanced Buttons";
            this.btnCustom.UseVisualStyleBackColor = true;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // btnGetBasic
            // 
            this.btnGetBasic.Location = new System.Drawing.Point(10, 65);
            this.btnGetBasic.Name = "btnGetBasic";
            this.btnGetBasic.Size = new System.Drawing.Size(272, 21);
            this.btnGetBasic.TabIndex = 3;
            this.btnGetBasic.Text = "Get Selected Value of Basic MessageBox";
            this.btnGetBasic.UseVisualStyleBackColor = true;
            this.btnGetBasic.Click += new System.EventHandler(this.btnGetBasic_Click);
            // 
            // btnGetAdv
            // 
            this.btnGetAdv.Location = new System.Drawing.Point(10, 92);
            this.btnGetAdv.Name = "btnGetAdv";
            this.btnGetAdv.Size = new System.Drawing.Size(272, 21);
            this.btnGetAdv.TabIndex = 4;
            this.btnGetAdv.Text = "Get Selected Value of Advanced MessageBox";
            this.btnGetAdv.UseVisualStyleBackColor = true;
            this.btnGetAdv.Click += new System.EventHandler(this.btnGetAdv_Click);
            // 
            // MessageBoxDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGetAdv);
            this.Controls.Add(this.btnGetBasic);
            this.Controls.Add(this.btnCustom);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnBasicMSG);
            this.Name = "MessageBoxDisplay";
            this.Size = new System.Drawing.Size(292, 266);
            this.Title = "MessageBox Control";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBasicMSG;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnGetBasic;
        private System.Windows.Forms.Button btnGetAdv;
    }
}