using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;
using FarsiLibrary.Localization;
using Timer=System.Timers.Timer;

namespace FarsiLibrary.Win.Design
{
    /// <summary>
    /// About form
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class About : Form
    {
        private readonly Timer m_fadeinTimer;
        private Button btnClose;
        private Label label1;
        private Label label2;
        private ListBox lst;
        private bool fade;
        private Label lblCopyright;
        private Label label3;
        private Label lblCurrentVersion;
        private bool m_fadeInFlag;

        public About(bool doFade) : this()
        {
            if(doFade)
            {
                fade = doFade;
                m_fadeinTimer = new Timer();
                m_fadeinTimer.Elapsed += m_fadeinTimer_Elapsed;
            }
        }
        
        /// <summary>
        /// Default constructor for <bottom>About</bottom> form.
        /// </summary>
        public About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads default values on form startup.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode && fade)
            {
                m_fadeInFlag = true;
                Opacity = 0;
                m_fadeinTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Run when user closes the form.
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (e.Cancel)
                return;

            if(!fade)
                return;
            
            if (Opacity > 0)
            {
                m_fadeInFlag = false;
                m_fadeinTimer.Enabled = true;
                e.Cancel = true;
            }
        }

        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lst = new System.Windows.Forms.ListBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(8, 210);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "خروج";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(368, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "کاری از : هادی اسکندری";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 8);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(368, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "مجموعه کنترل های فارسی NET.";
            // 
            // lst
            // 
            this.lst.ItemHeight = 14;
            this.lst.Location = new System.Drawing.Point(8, 116);
            this.lst.Name = "lst";
            this.lst.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lst.Size = new System.Drawing.Size(402, 88);
            this.lst.TabIndex = 3;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(5, 55);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCopyright.Size = new System.Drawing.Size(273, 18);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Web Site : http://www.seesharpsoftware.com.au";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 74);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(273, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email : H.Eskandari@Gmail.com";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.Location = new System.Drawing.Point(5, 95);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentVersion.Size = new System.Drawing.Size(273, 18);
            this.lblCurrentVersion.TabIndex = 6;
            this.lblCurrentVersion.Text = "Current Version: ";
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(422, 246);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "About";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " درباره ";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);

        }

        private void m_fadeinTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(!fade)
            {
                m_fadeinTimer.Enabled = false;
                return;
            }
            
            // How should we fade?
            if (m_fadeInFlag == false)
            {
                Opacity -= (m_fadeinTimer.Interval/500.0);

                // Should we continue to fade?
                if (Opacity > 0)
                    m_fadeinTimer.Enabled = true;
                else
                {
                    m_fadeinTimer.Enabled = false;
                    Close();
                } // End else we should close the form.
            } // End if we should fade in.
            else
            {
                Opacity += (m_fadeinTimer.Interval/500.0);
                m_fadeinTimer.Enabled = (Opacity < 1.0);
                m_fadeInFlag = (Opacity < 1.0);
            } // End else we should fade out.
        }

        private void About_Load(object sender, EventArgs e)
        {
            lst.Items.Clear();

            foreach (var product in AssemblyNames.Products)
            {
                lst.Items.Add(product.Name);
            }

            lblCurrentVersion.Text = string.Format("Current Version: {0}", AssemblyNames.Version);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}