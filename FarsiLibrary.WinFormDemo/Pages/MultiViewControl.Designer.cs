namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class MultiViewControl
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
            this.faMultiView1 = new FarsiLibrary.Win.Controls.FAMultiView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectedDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // faMultiView1
            // 
            this.faMultiView1.Location = new System.Drawing.Point(96, 70);
            this.faMultiView1.Name = "faMultiView1";
            this.faMultiView1.SelectedDateTime = new System.DateTime(2009, 3, 5, 0, 0, 0, 0);
            this.faMultiView1.TabIndex = 0;
            this.faMultiView1.Text = "faMultiView1";
            this.faMultiView1.SelectedDateTimeChanged += new System.EventHandler(this.faMultiView1_SelectedDateTimeChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "MonthView";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 21);
            this.button2.TabIndex = 2;
            this.button2.Text = "DayView";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(219, 242);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(78, 21);
            this.button4.TabIndex = 4;
            this.button4.Text = "Theme";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Try clicking an already selected Day to change to day view. Clicking the day view" +
                " will lead you back to monthview.";
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.Location = new System.Drawing.Point(97, 44);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(165, 23);
            this.lblSelectedDate.TabIndex = 6;
            this.lblSelectedDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MultiViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSelectedDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.faMultiView1);
            this.Name = "MultiViewControl";
            this.Size = new System.Drawing.Size(355, 272);
            this.Title = "MultiView Control";
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.Controls.FAMultiView faMultiView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSelectedDate;



    }
}