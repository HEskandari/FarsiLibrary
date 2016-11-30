using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.DevExpress
{
    public class XtraFADatePicker : PopupContainerEdit, IToolTipControlClient, IXtraResizableControl
    {
        #region Fields

        internal int DEFAULT_SIZE = 166;
        private readonly PopupContainerControl popupContainerControl;
        private readonly XtraFAMonthView picker;
        private ThemeTypes visualStyles = ThemeTypes.Office2000;

        #endregion

        #region Ctor

        static XtraFADatePicker()
        {
            RepositoryItemFADatePicker.Register();
        }

        public XtraFADatePicker()
        {
            ToolTipController.DefaultController.CalcSize += OnCalculateToolTip;
            ToolTipController.DefaultController.CustomDraw += OnCustomDrawToolTip;

            picker = new XtraFAMonthView();
            picker.ShowBorder = false;
            picker.SelectedDateTime = null;
            picker.Dock = DockStyle.Fill;
            picker.DoubleClick += OnDoubleClicked;
            picker.ButtonClicked += OnButtonClicked;
            picker.SelectedDateTimeChanged += OnSelectionChanged;

            popupContainerControl = new PopupContainerControl();
            popupContainerControl.Controls.Add(picker);

            Properties.PopupControl = popupContainerControl;
            Properties.QueryPopUp += OnEditorQueryPopup;

            popupContainerControl.Size = new Size(200, 200);
            popupContainerControl.PopupContainerProperties.PopupSizeable = false;
            popupContainerControl.PopupContainerProperties.CloseOnLostFocus = true;
            popupContainerControl.PopupContainerProperties.CloseOnOuterMouseClick = true;
            popupContainerControl.PopupContainerProperties.PopupSizeable = false;

            Text = string.Empty;
            Controls.Add(popupContainerControl);
            UpdateTheme();
        }

        private void OnDoubleClicked(object sender, EventArgs e)
        {
            if(IsPopupOpen)
                ClosePopup(PopupCloseMode.Immediate);
        }

        #endregion

        #region Props

        public override string EditorTypeName
        {
            get { return RepositoryItemFADatePicker.EditorName; }
        }

        [Description("Gets or sets a value specifying whether the editor's value can be changed by end users.")]
        public new bool ReadOnly
        {
            get { return Properties.ReadOnly; }
            set
            {
                Properties.ReadOnly = !value;
                Properties.Buttons[Properties.ActionButtonIndex].Enabled = !value;
                popupContainerControl.PopupContainerProperties.ShowDropDown = value ? ShowDropDown.Never : ShowDropDown.SingleClick;
                popupContainerControl.PopupContainerProperties.ReadOnly = value;
            }
        }

        [Browsable(false)]
        public XtraFAMonthView MonthView
        {
            get { return picker; }
        }

        private void OnEditorQueryPopup(object sender, CancelEventArgs e)
        {
            UpdateTheme();
        }

        private bool TryParseDateTime(string datetime, out DateTime? dt)
        {
            dt = null;

            try
            {
                if(string.IsNullOrEmpty(datetime))
                    return false;

                if (CultureHelper.IsFarsiCulture())
                {
                    dt = PersianDate.Parse(datetime);
                }
                else
                {
                    dt = DateTime.Parse(datetime);
                }

                if (dt.HasValue)
                    dt = dt.Value.Date;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override object EditValue
        {
            get { return base.EditValue; }
            set
            {
                if (value is string)
                {
                    DateTime? dt;
                    if (TryParseDateTime(value.ToString(), out dt))
                    {
                        base.EditValue = dt.Value;
                    }
                }
                else
                {
                    base.EditValue = value;
                }
            }
        }

        public override bool DoValidate()
        {
            return true;
        }

        [Browsable(false)]
        protected override bool CanShowPopup
        {
            get { return !Properties.ReadOnly; }
        }

        [Description("Gets or sets the string displayed in the edit box when the editor's BaseEdit.EditValue is null.")]
        [Localizable(true)]
        public string NullText
        {
            get { return Properties.NullText; }
            set { Properties.NullText = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region IToolTipControlClient

        public ToolTipControlInfo GetObjectInfo(Point point)
        {
            string value = Properties.GetDisplayText(EditValue);
            if (!string.IsNullOrEmpty(value) && ShowToolTips)
                return new ToolTipControlInfo(this, value);

            return null;
        }

        private void OnCustomDrawToolTip(object sender, ToolTipControllerCustomDrawEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.ShowInfo.ToolTip) && ShowToolTips)
            {
                using (StringFormat fmt = new StringFormat())
                {
                    fmt.LineAlignment = StringAlignment.Center;
                    fmt.Alignment = StringAlignment.Center;

                    if (RightToLeft == RightToLeft.Yes)
                    {
                        fmt.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    }
                    else
                    {
                        fmt.FormatFlags &= (~StringFormatFlags.DirectionRightToLeft);
                    }

                    using (SolidBrush br = new SolidBrush(ForeColor))
                    {
                        e.Cache.Graphics.DrawString(e.ShowInfo.ToolTip, e.ShowInfo.Appearance.Font, br, e.Bounds, fmt);
                    }
                }
            }

            e.Handled = true;
        }

        private void OnCalculateToolTip(object sender, ToolTipControllerCalcSizeEventArgs e)
        {
            if (e.SelectedControl is PopupContainerEdit)
            {
                PopupContainerEdit popup = (PopupContainerEdit)e.SelectedControl;
                if (popup == this)
                {
                    if (EditValue != null)
                    {
                        e.Size = new Size(75, 20);
                    }
                    else
                    {
                        e.ShowInfo.Show = false;
                    }
                }
            }
        }

        #endregion

        #region IXtraResizableControl Members

        public event EventHandler Changed;

        [Browsable(false)]
        public Size MinSize
        {
            get { return new Size(50, 21); }
        }

        [Browsable(false)]
        public Size MaxSize
        {
            get { return new Size(0, 21); }
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

        #region Methods

        protected override void CreateRepositoryItem()
        {
            base.CreateRepositoryItem();
            fProperties = new RepositoryItemFADatePicker();
            Properties.PopupControl = popupContainerControl;
            Properties.SetOwner(this);
        }

        protected override Size CalcPopupFormSize()
        {
            return new Size(168, 168);
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            popupContainerControl.RightToLeft = RightToLeft;
        }

        protected virtual bool ShouldSerializeNullText()
        {
            return NullText != string.Empty;
        }

        public bool ShouldSerializeLookAndFeel()
        {
            return LookAndFeel != null && LookAndFeel.ShouldSerialize();
        }

        public bool ShouldSerializeReadOnly()
        {
            return ReadOnly;
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (picker.IsNull)
            {
                EditValue = null;
            }
            else
            {
                EditValue = picker.SelectedDateTime;
            }
        }

        private void OnButtonClicked(object sender, CalendarButtonClickedEventArgs e)
        {
            if (Properties.CloseOnLostFocus)
                ClosePopup();
        }

        #endregion

        #region Repository

        /// <summary>
        /// Repository
        /// </summary>
        [Browsable(true)]
        [Category("Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemFADatePicker Properties
        {
            get
            {
                if (IsDesignMode)
                {
                    RepositoryItemFADatePicker.Register();
                }

                return base.Properties as RepositoryItemFADatePicker;
            }
        }

        #endregion

        #region LookAndFeel

        protected override void OnLookAndFeelChanged(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        protected internal virtual void UpdateTheme()
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
                    throw new NotImplementedException("This style is not implemented");
            }
        }

        #endregion

        #region Themes

        /// <summary>
        /// Gets or Sets currently selected visual style for the control.
        /// </summary>
        //[DefaultValue(typeof(VisualStyleTypes), "Office2000")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeTypes Theme
        {
            get { return visualStyles; }
            set
            {
                visualStyles = value;
                UpdateVisualStyles();
            }
        }

        private void UpdateVisualStyles()
        {
            switch (Theme)
            {
                case ThemeTypes.Office2007:
                    picker.Theme = ThemeTypes.Office2007;
                    LookAndFeel.SetSkinStyle("Blue");
                    break;

                case ThemeTypes.WindowsXP:
                    picker.Theme = ThemeTypes.WindowsXP;
                    LookAndFeel.SetWindowsXPStyle();
                    break;

                case ThemeTypes.Office2003:
                    picker.Theme = ThemeTypes.Office2003;
                    LookAndFeel.SetOffice2003Style();
                    break;

                case ThemeTypes.Office2000:
                    picker.Theme = ThemeTypes.Office2000;
                    LookAndFeel.SetFlatStyle();
                    break;

                default:
                    throw new NotImplementedException("Selected Theme is not implemented.");
            }
        }

        #endregion
    }
}
