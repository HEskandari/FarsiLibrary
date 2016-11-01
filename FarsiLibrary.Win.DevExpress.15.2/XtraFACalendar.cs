using System;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Win.DevExpress
{
    public class XtraFACalendarControl : CalendarControl, IPersianCalendarControl
    {
        public XtraFACalendarControl()
        {
            MinValue = PersianDate.MinValue;
            FirstDayOfWeek = DayOfWeek.Saturday;
        }

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            if (!CultureManager.Instance.ControlsCulture.IsFarsiCulture())
                return base.CreateViewInfo();

            if (ActualCalendarView == CalendarView.Vista)
                return new VistaPersianCalendarViewInfo(this);

            return new CalendarViewInfo(this);
        }

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