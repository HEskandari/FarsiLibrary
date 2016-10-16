using System;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using FarsiLibrary.Utils;

namespace FarsiLibrary.Win.DevExpress
{
    public class XtraFACalendarControl : CalendarControl, IPersianCalendarControl
    {
        private PersianCultureInfo culture;

        public XtraFACalendarControl()
        {
            culture = new PersianCultureInfo();
            MinValue = PersianDate.MinValue;
            FirstDayOfWeek = DayOfWeek.Saturday;
            DateFormat = culture.DateTimeFormat;
        }

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            if (ActualCalendarView == CalendarView.Vista)
                return new VistaPersianCalendarViewInfo(this);

            return new CalendarViewInfo(this);
        }

        //protected override CalendarSelectionManager CreateSelectionManager()
        //{
        //    return new PersianCalendarSelectionManager(this);
        //}

        //protected override DateTime GetTodayDateTime()
        //{
        //    DateTime todayDate = GetTodayPersianDate();
        //    if (ShowTimeEdit)
        //        return new DateTime(todayDate.Year, todayDate.Month, todayDate.Day, TimeEdit.Time.Hour, TimeEdit.Time.Minute, TimeEdit.Time.Second);

        //    return todayDate;
        //}

        public DateTime GetTodayPersianDate()
        {
            if (!PersianCalendar.IsWithInSupportedRange(TodayDate) ||
                TodayDate == PersianDate.MinValue)
            {
                return PersianDate.Today;
            }

            return TodayDate;
        }

        public bool ShouldSerializeDateTime()
        {
            return DateTime != GetTodayPersianDate();
        }

        public void ResetDateTime()
        {
            DateTime = GetTodayPersianDate();
        }

        public new DateTime MinValue
        {
            get { return base.MinValue; }
            set { base.MinValue = value; }
        }

        public bool ShouldSerializeMinValue()
        {
            return false;
        }

    }
}