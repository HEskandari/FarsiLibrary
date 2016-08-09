using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using FarsiLibrary.Localization;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using PersianCalendar=FarsiLibrary.Utils.PersianCalendar;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.BaseClasses
{
    public class BaseDateControl : BaseStyledControl
    {
        #region Fields

		private readonly PersianCalendar pc;
        private readonly GregorianCalendar gc;
        private readonly HijriCalendar hc;
        private Calendar customCalendar;
        private DateTime? selectedDate;
        private DateTime viewDate;

        #endregion

        #region Events

        /// <summary>
        /// Fired when displaying date value changes.
        /// </summary>
        public event EventHandler ViewDateTimeChanged;

        /// <summary>
        /// Fires when SelectedDateTime value changes.
        /// </summary>
        public event EventHandler SelectedDateTimeChanged;

        /// <summary>
        /// Fires when Day value changes.
        /// </summary>
		public event EventHandler<DateChangedEventArgs> SelectedDayChanged;

        /// <summary>
        /// Fires when MonthValue changes.
        /// </summary>
		public event EventHandler<DateChangedEventArgs> SelectedMonthChanged;

        /// <summary>
        /// Fires when Year value changes.
        /// </summary>
		public event EventHandler<DateChangedEventArgs> SelectedYearChanged;

        /// <summary>
        /// Fires when view's Day value changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> ViewDayChanged;

        /// <summary>
        /// Fires when view's Month value changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> ViewMonthChanged;

        /// <summary>
        /// Fires when view's Year value changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> ViewYearChanged;

        #endregion

        #region Ctor

        public BaseDateControl()
        {
            pc = new PersianCalendar();
            gc = new GregorianCalendar();
            hc = new HijriCalendar();

            SelectedDateTime = DateTime.Now.Date;
            ViewDateTime = DateTime.Now.Date;
        }

        #endregion

        #region Methods

        protected internal Calendar GetDefaultCalendar()
        {
            if (customCalendar != null)
                return customCalendar;

            if (DefaultCulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                return pc;
	        
            if (DefaultCulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                return hc;
	        
            return gc;
        }

        protected internal int GetFirstDayOfWeek(DateTime date)
        {
            if (CultureHelper.IsFarsiCulture())
            {
                return PersianDateTimeFormatInfo.GetDayIndex(date.DayOfWeek);
            }
         
            if (CultureHelper.IsArabicCulture())
            {
                return (int)date.DayOfWeek;
            }

            return (int)date.DayOfWeek;
        }

        /// <summary>
        /// Gets the abbreviated name of the specified day.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        protected internal string GetAbbrDayName(DayOfWeek day)
        {
            string dayName;

            if (FALocalizeManager.Instance.CustomCulture != null)
            {
                if (FALocalizeManager.Instance.IsCustomFarsiCulture)
                {
                    dayName = PersianDateTimeFormatInfo.GetWeekDayAbbr(day);
                }
                else if (FALocalizeManager.Instance.IsCustomArabicCulture)
                {
                    dayName = FALocalizeManager.Instance.CustomCulture.DateTimeFormat.GetAbbreviatedDayName(day);
                    dayName = dayName.Remove(0, 2).Substring(0, 1);
                }
                else
                {
                    dayName = FALocalizeManager.Instance.CustomCulture.DateTimeFormat.GetAbbreviatedDayName(day);
                }
            }
            else if (FALocalizeManager.Instance.IsThreadCultureFarsi)
            {
                dayName = PersianDateTimeFormatInfo.GetWeekDayAbbr(day);
            }
            else if (FALocalizeManager.Instance.IsThreadCultureArabic)
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day);
                dayName = dayName.Remove(0, 2).Substring(0, 1);
            }
            else
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day);
            }

            //Workaround for cultures lacking appropriate 
            //short name for weekday names
            if (FALocalizeManager.Instance.CustomCulture != null && dayName.Length > 3)
            {
                //Remove the definition artilce from the work
                //which is Al in arabic. then return the next 
                //charachter which is the first charachter of the day's name.
                dayName = dayName.Substring(0, 3);
            }

            return dayName;
        }

        /// <summary>
        /// Gets the name of the specified day.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        protected internal string GetDayName(DayOfWeek day)
        {
            string dayName;

            if (FALocalizeManager.Instance.CustomCulture != null)
            {
                if (FALocalizeManager.Instance.IsCustomFarsiCulture)
                {
                    dayName = PersianDateTimeFormatInfo.GetWeekDay(day);
                }
                else if (FALocalizeManager.Instance.IsCustomArabicCulture)
                {
                    dayName = FALocalizeManager.Instance.CustomCulture.DateTimeFormat.GetDayName(day);
                }
                else
                {
                    dayName = FALocalizeManager.Instance.CustomCulture.DateTimeFormat.GetDayName(day);
                }
            }
            else if (FALocalizeManager.Instance.IsThreadCultureFarsi)
            {
                dayName = PersianDateTimeFormatInfo.GetWeekDay(day);
            }
            else if (FALocalizeManager.Instance.IsThreadCultureArabic)
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetDayName(day);
            }
            else
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day);
            }

            return dayName;
        }

        /// <summary>
        /// Returns the name of the specified month.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        protected internal string GetMonthName(int month)
        {
            if (DefaultCulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                return PersianDateTimeFormatInfo.MonthGenitiveNames[month - 1];

            return DefaultCulture.DateTimeFormat.MonthGenitiveNames[month - 1].ToUpper();
        }

        protected void UpdateSelectedDayMonthYearValues(DateChangedEventArgs args)
        {
            OnSelectedDayChanged(args);
            OnSelectedMonthChanged(args);
            OnSelectedYearChanged(args);
        }

        protected void UpdateViewDayMonthYearValues(DateChangedEventArgs args)
        {
            OnViewDayChanged(args);
            OnViewMonthChanged(args);
            OnViewYearChanged(args);
        }

        protected void UpdateViewDayMonthYearValues()
        {
            var args = new DateChangedEventArgs(ViewDateTime, ViewDateTime);
            UpdateViewDayMonthYearValues(args);
        }

        protected void UpdateSelectedDayMonthYearValues()
        {
            var args = new DateChangedEventArgs(SelectedDateTime, SelectedDateTime);
            UpdateSelectedDayMonthYearValues(args);
        }

        #endregion

        #region Props

        /// <summary>
        /// Default calendar of the control, based on <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> properties.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Calendar DefaultCalendar
        {
            get
            {
                return GetDefaultCalendar();
            }
            set
            {
                customCalendar = value;
            }
        }

        /// <summary>
        /// Arabic culture supported by the control : ("AR-SA")
        /// </summary>
        [Browsable(false)]
        public CultureInfo ArabicCulture
        {
            get { return FALocalizeManager.Instance.ArabicCulture; }
        }

        /// <summary>
        /// Invariant culture supported by the control.
        /// </summary>
        [Browsable(false)]
        public CultureInfo InvariantCulture
        {
            get { return FALocalizeManager.Instance.InvariantCulture; }
        }

        /// <summary>
        /// Persian culture supported by the control. ("FA-IR")
        /// </summary>
        [Browsable(false)]
        public CultureInfo PersianCulture
        {
            get { return FALocalizeManager.Instance.FarsiCulture; }
        }

        /// <summary>
        /// PersianCalendar instance with which controls calculates values of <see cref="PersianCulture"/>.
        /// </summary>
        [Browsable(false)]
        public PersianCalendar PersianCalendar
        {
            get { return pc; }
        }

        /// <summary>
        /// GregorianCalendar instance with which controls calculates values of <see cref="InvariantCulture"/>.
        /// </summary>
        [Browsable(false)]
        public Calendar InvariantCalendar
        {
            get { return gc; }
        }

        /// <summary>
        /// HijriCalendar instance with which controls calculates values of <see cref="ArabicCulture"/>.
        /// </summary>
        [Browsable(false)]
        public Calendar HijriCalendar
        {
            get { return hc; }
        }

        [Browsable(false)]
        protected bool IsRightToLeftCulture
        {
            get { return DefaultCulture.TextInfo.IsRightToLeft; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CultureInfo DefaultCulture
        {
            get
            {
                if (FALocalizeManager.Instance.CustomCulture != null)
                    return FALocalizeManager.Instance.CustomCulture;

                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                    return FALocalizeManager.Instance.FarsiCulture;
	            
                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                    return FALocalizeManager.Instance.ArabicCulture;
	            
                return FALocalizeManager.Instance.InvariantCulture;
            }
            set
            {
                FALocalizeManager.Instance.CustomCulture = value;
            }
        }

        /// <summary>
        /// Currently selected DateTime in calendar control.
        /// </summary>
        [Description("Currently selected DateTime instance from calendar.")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Localizable(true)]
        public DateTime? SelectedDateTime
        {
            get { return selectedDate; }
            set
            {
                var oldValue = selectedDate;
                var newValue = value;

                UpdateSelectedDayMonthYearValues(new DateChangedEventArgs(newValue, oldValue));
                selectedDate = newValue;
                OnSelectedDateTimeChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        public DateTime ViewDateTime
        {
            get { return viewDate; }
            set
            {
                var oldValue = selectedDate;
                var newValue = value;

                UpdateViewDayMonthYearValues(new DateChangedEventArgs(newValue, oldValue));
                viewDate = value;
                OnSelectedViewDateChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets the Day value.
        /// </summary>
        [Browsable(false)]
        public int SelectedDay
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets the Month value.
        /// </summary>
        [Browsable(false)]
        public int SelectedMonth
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets the Year value.
        /// </summary>
        [Browsable(false)]
        public int SelectedYear
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets the view day value.
        /// </summary>
        [Browsable(false)]
        public int ViewDay
        {
            get; private set;
        }
        /// <summary>
        /// Gets the view month value.
        /// </summary>
        [Browsable(false)]
        public int ViewMonth
        {
            get; private set;
        }

        /// <summary>
        /// Gets the view year value.
        /// </summary>
        [Browsable(false)]
        public int ViewYear
        {
            get; private set;
        }

        #endregion

        #region Date Functions

        protected virtual void OnSelectedMonthChanged(DateChangedEventArgs e)
        {
            if(SelectedMonthChanged != null)
                SelectedMonthChanged(this, e);

            SelectedMonth = e.NewValue.HasValue ? DefaultCalendar.GetMonth(e.NewValue.Value) : -1;
        }

        protected virtual void OnSelectedYearChanged(DateChangedEventArgs e)
        {
            if(SelectedYearChanged != null)
                SelectedYearChanged(this, e);

            SelectedYear = e.NewValue.HasValue ? DefaultCalendar.GetYear(e.NewValue.Value) : -1;
        }

        protected virtual void OnSelectedDayChanged(DateChangedEventArgs e)
        {
            if(SelectedDayChanged != null)
                SelectedDayChanged(this, e);

            SelectedDay = e.NewValue.HasValue ? DefaultCalendar.GetDayOfMonth(e.NewValue.Value) : -1;
        }

        protected virtual void OnViewMonthChanged(DateChangedEventArgs e)
        {
            if (ViewMonthChanged != null)
                ViewMonthChanged(this, e);

            ViewMonth = DefaultCalendar.GetMonth(e.NewValue.Value);
        }

        protected virtual void OnViewYearChanged(DateChangedEventArgs e)
        {
            if (ViewYearChanged != null)
                ViewYearChanged(this, e);

            ViewYear = DefaultCalendar.GetYear(e.NewValue.Value);
        }

        protected virtual void OnViewDayChanged(DateChangedEventArgs e)
        {
            if (ViewDayChanged != null)
                ViewDayChanged(this, e);

            ViewDay = DefaultCalendar.GetDayOfMonth(e.NewValue.Value);
        }

        protected virtual void OnSelectedDateTimeChanged(EventArgs e)
        {
            if(SelectedDateTimeChanged != null)
                SelectedDateTimeChanged(this, e);
        }

        protected virtual void OnSelectedViewDateChanged(EventArgs e)
        {
            if (ViewDateTimeChanged != null)
                ViewDateTimeChanged(this, e);
        }

        protected virtual bool IsValidDate(DateTime value)
        {
            return value < DefaultCalendar.MaxSupportedDateTime && 
                   value > DefaultCalendar.MinSupportedDateTime;
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Determines if ViewDateTime property should be serialized.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeViewDateTime()
        {
            return false;
        }

        /// <summary>
        /// Determines to serialize DefaultCalendar property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeDefaultCalendar()
        {
            return false;
        }

        /// <summary>
        /// Determines to serialize DefaultCulture property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeDefaultCulture()
        {
            return false;
        }

        /// <summary>
        /// Determines to serialize SelectedDateTime property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSelectedDateTime()
        {
            return SelectedDateTime.HasValue && IsValidDate(SelectedDateTime.Value);
        }

        #endregion
    }
}