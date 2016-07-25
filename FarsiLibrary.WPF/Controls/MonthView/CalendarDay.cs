using System;
using System.ComponentModel;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.WPF.Controls
{
    public class CalendarDay : INotifyPropertyChanged
    {
        private DateTime date;
        private bool isOtherMonth;
        private bool isSelectable;
        private object data;

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarDay(DateTime dt)
        {
            date = dt;
        }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            internal set
            {
                if (date != value)
                {
                    DateTime oldDate = date;
                    date = value;
                    OnPropertyChanged("Date");

                    if (IsDateToday(oldDate) != IsDateToday(date))
                    {
                        OnPropertyChanged("IsToday");
                    }

                    if (IsDateWeekend(oldDate) != IsDateWeekend(date))
                    {
                        OnPropertyChanged("IsWeekend");
                    }
                }
            }
        }

        public bool IsOtherMonth
        {
            get { return isOtherMonth; }
            internal set
            {
                if (isOtherMonth != value)
                {
                    isOtherMonth = value;
                    OnPropertyChanged("IsOtherMonth");
                }
            }
        }

        public bool IsSelectable
        {
            get { return isSelectable; }
            internal set
            {
                if (isSelectable != value)
                {
                    isSelectable = value;
                    OnPropertyChanged("IsSelectable");
                }
            }
        }

        public bool IsToday
        {
            get { return IsDateToday(date); }
        }

        public bool IsWeekend
        {
            get { return IsDateWeekend(date); }
        }

        public object Data
        {
            get { return data; }
            internal set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }

        public override string ToString()
        {
            return date.ToShortDateString();
        }

        internal void InternalUpdate(CalendarDay cdate)
        {
            Date = cdate.Date;
            IsOtherMonth = cdate.IsOtherMonth;
            IsSelectable = cdate.IsSelectable;
        }

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private bool IsDateWeekend(DateTime selectedDate)
        {
            if(CultureHelper.IsFarsiCulture() || CultureHelper.IsArabicCulture())
            {
                return selectedDate.DayOfWeek == DayOfWeek.Friday || selectedDate.DayOfWeek == DayOfWeek.Thursday;
            }

            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsDateToday(DateTime selectedDate)
        {
            return CompareYearMonthDay(selectedDate, DateTime.Now) == 0;
        }

        internal static int CompareYearMonthDay(DateTime dt1, DateTime dt2)
        {
            DateTime first = new DateTime(dt1.Year, dt1.Month, dt1.Day);
            DateTime second = new DateTime(dt2.Year, dt2.Month, dt2.Day);
            TimeSpan ts = first - second;
            return ts.Days;
        }

    }
}