using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils.Controls;
using FarsiLibrary.Win.Controls;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.DevExpress
{
    public class XtraFAMonthView : FAMonthView, IXtraResizableControl, ISupportLookAndFeel
    {
        #region Fields

        private UserLookAndFeel lookAndFeel;

        #endregion

        #region IXtraResizableControl

        public event EventHandler Changed;

        [Browsable(false)]
        public Size MinSize
        {
            get { return new Size(166, 166); }
        }

        [Browsable(false)]
        public Size MaxSize
        {
            get { return new Size(166, 166); }
        }

        [Browsable(false)]
        public bool IsCaptionVisible
        {
            get { return false; }
        }

        protected virtual void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        #endregion

        #region Ctor

        public XtraFAMonthView()
        {
            lookAndFeel = null;
            CreateLookAndFeel();
            UpdateTheme();
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (lookAndFeel != null)
                {
                    lookAndFeel.StyleChanged -= OnLookAndFeelChanged;
                }

                if (LookAndFeel != null)
                {
                    LookAndFeel.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region LookAndFeel

        public bool ShouldSerializeLookAndFeel()
        {
            return LookAndFeel != null && LookAndFeel.ShouldSerialize();
        }

        protected virtual void OnLookAndFeelChanged(object sender, EventArgs e)
        {
            switch(lookAndFeel.ActiveStyle)
            {
                case ActiveLookAndFeelStyle.WindowsXP:
                    Theme = ThemeTypes.WindowsXP;
                    break;

                case ActiveLookAndFeelStyle.Office2003:
                    Theme = ThemeTypes.Office2003;
                    break;

                case ActiveLookAndFeelStyle.Skin:
                    Theme = ThemeTypes.Office2007;
                    break;

                case ActiveLookAndFeelStyle.Flat:
                    Theme = ThemeTypes.Office2000;
                    break;

                default:
                    throw new Exception("This style is not implemented");
            }
        }

        protected void CreateLookAndFeel()
        {
            lookAndFeel = new ControlUserLookAndFeel(this);
            lookAndFeel.StyleChanged += OnLookAndFeelChanged;
        }

        #endregion

        #region ISupportLookAndFeel

        [Description("Provides access to the object containing the control's look and feel settings.")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual UserLookAndFeel LookAndFeel
        {
            get { return lookAndFeel; }
        }

        [Browsable(false)]
        public bool IgnoreChildren
        {
            get { return false; }
        }

        protected void UpdateTheme()
        {
            switch (LookAndFeel.ActiveStyle)
            {
                case ActiveLookAndFeelStyle.WindowsXP:
                    Theme = ThemeTypes.WindowsXP;
                    break;

                case ActiveLookAndFeelStyle.Office2003:
                    Theme = ThemeTypes.Office2003;
                    break;

                case ActiveLookAndFeelStyle.Skin:
                    Theme = ThemeTypes.Office2007;
                    break;

                case ActiveLookAndFeelStyle.Flat:
                    Theme = ThemeTypes.Office2000;
                    break;

                default:
                    throw new Exception("This style is not implemented");
            }
        }

        #endregion

        #region Hidden Props

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ThemeTypes Theme
        {
            get { return base.Theme; }
            set { base.Theme = value; }
        }

        #endregion
    }
}