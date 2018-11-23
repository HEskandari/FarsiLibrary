using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using FarsiLibrary.Localization;
using FarsiLibrary.Utils;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Design;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// A datepicker control which can select date in <see cref="System.Globalization.GregorianCalendar"/>, <see cref="PersianCalendar" /> and <see cref="System.Globalization.HijriCalendar"/> based on current thread's Culture and UICulture. 
    /// 
    /// To know how to display the control in other cultures and calendars, please see <see cref="FAMonthView"/> control's documentation.
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("SelectedDateTimeChanged")]
    [DefaultProperty("SelectedDateTime")]
    [Designer(typeof(FADatePickerDesigner))]
    [DefaultBindingProperty("SelectedDateTime")]
    public class FADatePicker : FAContainerComboBox
    {
        #region Fields

        private DateTime? selectedDateTime;
        private string dateseparator = ";";
        internal FAMonthViewContainer mv;

	    #endregion

        #region Events

        /// <summary>
        /// Fires when SelectedDateTime property of the control changes.
        /// </summary>
        public event EventHandler SelectedDateTimeChanged;

        /// <summary>
        /// Fires when SelectedDateTime property of the control is changing.
        /// </summary>
        public event SelectedDateTimeChangingEventHandler SelectedDateTimeChanging;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of FADatePicker class.
        /// </summary>
        public FADatePicker()
        {
            mv = new FAMonthViewContainer(this);
            RightToLeftChanged += OnInternalRightToLeftChanged;
            mv.MonthViewControl.SelectedDateTimeChanged += OnMVSelectedDateTimeChanged;
            mv.MonthViewControl.SelectedDateRangeChanged += OnMVSelectedDateRangeChanged;
            mv.MonthViewControl.ButtonClicked += OnMVButtonClicked;
            FALocalizeManager.Instance.LocalizerChanged += OnInternalLocalizerChanged;
            base.TextBox.TextChanged += (sender, e) => OnTextChanged(EventArgs.Empty);
            PopupShowing += OnInternalPopupShowing;
            Text = FALocalizeManager.Instance.GetLocalizerByCulture(mv.MonthViewControl.DefaultCulture).GetLocalizedString(StringID.Validation_NullText);
            FormatInfo = FormatInfoTypes.ShortDate;
        }

        #endregion

        #region Props
        
        /// <summary>
        /// Determines if the control has not made any selection yet.
        /// </summary>
        [DefaultValue(true)]
        [Description("Determines if the control has not made any selection yet.")]
        [Browsable(false)]
        public bool IsNull
        {
            get { return mv.MonthViewControl.IsNull; }
        }

        /// <summary>
        /// Determinces scrolling option of the MonthView control.
        /// </summary>
        [DefaultValue(typeof(ScrollOptionTypes), "Month")]
        [Description("Determinces scrolling option of the MonthView control.")]
        public ScrollOptionTypes ScrollOption
        {
            get { return mv.MonthViewControl.ScrollOption; }
            set { mv.MonthViewControl.ScrollOption = value; }
        }

        /// <summary>
        /// Determines if Empty button should be shown in MonthView control.
        /// </summary>
        [DefaultValue(true)]
        [Description("Determines if Empty button should be shown in MonthView control")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ShowEmptyButton
        {
            get { return mv.MonthViewControl.ShowEmptyButton; }
            set { mv.MonthViewControl.ShowEmptyButton = value; }
        }

        /// <summary>
        /// Determines if Today button should be shown in MonthView control.
        /// </summary>
        [DefaultValue(true)]
        [Description("Determines if Today button should be shown in MonthView control")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ShowTodayButton
        {
            get { return mv.MonthViewControl.ShowTodayButton; }
            set { mv.MonthViewControl.ShowTodayButton = value; }
        }
        
        /// <summary>
        /// Gets or Sets to show a border around the MonthView control.
        /// </summary>
        [DefaultValue(true)]
        [Description("Gets or Sets to show a border around the MonthView control.")]
        public bool ShowBorder
        {
            get { return mv.MonthViewControl.ShowBorder; }
            set { mv.MonthViewControl.ShowBorder = value; }
        }

        /// <summary>
        /// Gets or Sets to show the focus rectangle around the selected day in MonthView control.
        /// </summary>
        [DefaultValue(false)]
        [Description("Gets or Sets to show the focus rectangle around the selected day in MonthView control.")]
        public bool ShowFocusRect
        {
            get { return mv.MonthViewControl.ShowFocusRect; }
            set { mv.MonthViewControl.ShowFocusRect = value; }
        }

        /// <summary>
        /// Selected value of the control as a <see cref="DateTime"/> instance.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Selected value of the control as a DateTime instance.")]
        public DateTime? SelectedDateTime
        {
            get { return selectedDateTime; }
            set
            {
                if (selectedDateTime == value)
                    return;

                //Validating
                var validateArgs = new ValueValidatingEventArgs(Text) {HasError = HasErrors};
                OnValueValidating(validateArgs);
                if (validateArgs.HasError)
                    return;

                var oldValue = selectedDateTime;
                var newValue = value;
                var changeArgs = new SelectedDateTimeChangingEventArgs(newValue, oldValue);
                OnSelectedDateTimeChanging(changeArgs);

                if (changeArgs.Cancel)
                {
                    if (string.IsNullOrEmpty(changeArgs.Message))
                        Error.SetError(this, FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_Cancel));
                    else
                        Error.SetError(this, changeArgs.Message);

                    return;
                }

                if (!string.IsNullOrEmpty(changeArgs.Message))
                {
                    Error.SetError(this, changeArgs.Message);
                }
                else
                {
                    Error.SetError(this, string.Empty);
                }

                //No errors, proceed
                mv.MonthViewControl.SelectedDateTime = changeArgs.NewValue;
            }
        }

        /// <summary>
        /// Selected values collection, if the control is in MultiSelect mode.
        /// </summary>
        [Editor(typeof(DateTimeCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(DateTimeConverter))]
        [Description("Selected values collection, if the MonthView control is in MultiSelect mode.")]
        public DateTimeCollection SelectedDateRange
        {
            get { return mv.MonthViewControl.SelectedDateRange; }
        }

        /// <summary>
        /// Gets or Sets the control in MultiSelect mode.
        /// </summary>
		[DefaultValue(false)]
        [Description("Gets or Sets the control in MultiSelect mode.")]
		public bool IsMultiSelect
		{
			get { return mv.MonthViewControl.IsMultiSelect; }
			set
			{
			    mv.MonthViewControl.IsMultiSelect = value;
                UpdateTextValue();
			}
		}

        /// <summary>
        /// Gets or Sets the character that separates date values when control 
        /// is in MultiSelect mode.
        /// </summary>
        [DefaultValue(";")]
        [Description("Gets or Sets the character that separates date values when control is in MultiSelect mode.")]
        public string DateSeparator
        {
            get { return dateseparator; }
            set { dateseparator = value; }
        }

        #endregion

        #region EventHandling

        private void OnInternalLocalizerChanged(object sender, EventArgs e)
        {
            UpdateTextValue();
        }

        private void OnInternalRightToLeftChanged(object sender, EventArgs e)
        {
            SetPosTextBox();
        }

        private void OnInternalPopupShowing(object sender, EventArgs e)
        {
            mv.MonthViewControl.Theme = Theme;
            var args = new ValueValidatingEventArgs(Text);
            OnValueValidating(args);
        }

        protected override void OnBindingPopupControl(BindPopupControlEventArgs e)
        {
            e.BindedControl = mv;
            base.OnBindingPopupControl(e);
        }

        protected virtual void OnSelectedDateTimeChanging(SelectedDateTimeChangingEventArgs e)
        {
            e.Cancel = false;

            if (SelectedDateTimeChanging != null)
                SelectedDateTimeChanging(this, e);
        }

        protected virtual void OnSelectedDateTimeChanged(EventArgs e)
        {
            if (SelectedDateTimeChanged != null)
                SelectedDateTimeChanged(this, e);
        }

        private void OnMVSelectedDateTimeChanged(object sender, EventArgs e)
        {
            SetSelectedDateTime(mv.MonthViewControl.SelectedDateTime);
        }

        private void OnMVSelectedDateRangeChanged(object sender, SelectedDateRangeChangedEventArgs e)
        {
            UpdateTextValue();
        }

        private void OnMVButtonClicked(object sender, CalendarButtonClickedEventArgs e)
        {
            HideDropDown();
        }

        private void SetSelectedDateTime(DateTime? dt)
        {
            var oldValue = selectedDateTime;
            var newValue = dt;
            
            var changeArgs = new SelectedDateTimeChangingEventArgs(newValue, oldValue);
            OnSelectedDateTimeChanging(changeArgs);
            
            if (changeArgs.Cancel)
            {
                if(string.IsNullOrEmpty(changeArgs.Message))
                {
                    Error.SetError(this, FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_Cancel));
                }
                else
                {
                    Error.SetError(this, changeArgs.Message);
                }

                return;
            }
            
            if(!string.IsNullOrEmpty(changeArgs.Message))
            {
                Error.SetError(this, changeArgs.Message);
            }
            else
            {
                Error.SetError(this, string.Empty);
            }

            selectedDateTime = changeArgs.NewValue;
            OnSelectedDateTimeChanged(EventArgs.Empty);

            UpdateTextValue();
        }

        /// <summary>
        /// Updates text representation of the selected value
        /// </summary>
        public override void UpdateTextValue()
        {
            if (mv.MonthViewControl.IsNull)
            {
                Text = FALocalizeManager.Instance.GetLocalizerByCulture(mv.MonthViewControl.DefaultCulture).GetLocalizedString(StringID.Validation_NullText);
            }
            else
            {
                if(!IsMultiSelect)
                {
                    Text = ConvertDateValue(SelectedDateTime);
                }
                else
                {
                    string textValue = string.Empty;
                    bool isFirst = true;
                    foreach (var date in SelectedDateRange)
                    {
                        if(!isFirst)
                        {
                            textValue += DateSeparator;
                        }

                        textValue += ConvertDateValue(date);
                        isFirst = false;
                    }

                    Text = textValue;
                }
            }
        }

        private string ConvertDateValue(DateTime? date)
        {
            string result;

            if(!date.HasValue)
            {
                result = FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NullText);
            }
            else if (mv.MonthViewControl.DefaultCulture.Equals(mv.MonthViewControl.PersianCulture))
            {
                result = ((PersianDate) date).ToString(GetFormatByFormatInfo(FormatInfo));
            }
            else
            {
                result = date.Value.ToString(GetFormatByFormatInfo(FormatInfo), mv.MonthViewControl.DefaultCulture);
            }

            return result;
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            var args = new ValueValidatingEventArgs(Text);
            OnValueValidating(args);
            e.Cancel = args.HasError;

            base.OnValidating(e);
        }

        protected override void OnValueValidating(ValueValidatingEventArgs e)
        {
            base.OnValueValidating(e);

            try
            {
                var txt = e.Value;
                if (string.IsNullOrEmpty(txt) || txt == FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NullText))
                {
                    e.HasError = false;
                }
                else
                {
                    if(!IsMultiSelect)
                    {
                        var pd = Parse(txt);
                        e.HasError = false;
                        mv.MonthViewControl.SelectedDateTime = pd;
                    }
                    else
                    {
                        var dates = txt.Split(DateSeparator.ToCharArray(0, 1));
                        var dateList = new List<DateTime>();

                        foreach (string dateEntry in dates)
                        {
                            var pd = Parse(dateEntry);
                            dateList.Add(pd);
                        }

                        e.HasError = false;
                        mv.MonthViewControl.SelectedDateRange.Clear();
                        mv.MonthViewControl.SelectedDateRange.AddRange(dateList.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                e.HasError = true;
                mv.MonthViewControl.SelectedDateTime = null;
            }
        }

        private DateTime Parse(string value)
        {
            if (mv.MonthViewControl.DefaultCulture.Equals(mv.MonthViewControl.PersianCulture))
            {
                return PersianDate.Parse(value);
            }
            else
            {
                return DateTime.Parse(value);
            }
        }
        
        #endregion

        #region ShouldSerialize and Reset

        /// <summary>
        /// Decides to serialize the SelectedDateTime property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSelectedDateTime()
        {
            return SelectedDateTime.HasValue;
        }

        /// <summary>
        /// Rests SelectedDateTime to default value.
        /// </summary>
        public void ResetSelectedDateTime()
        {
            SelectedDateTime = null;
        }

        #endregion
    }
}
