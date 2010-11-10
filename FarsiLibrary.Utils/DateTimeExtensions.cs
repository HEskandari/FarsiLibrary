using System;
using FarsiLibrary.Utils.Formatter;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Utils
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the DateTime to a PersianDate equivalant.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static PersianDate ToPersianDate(this DateTime dateTime)
        {
            if (dateTime < CultureHelper.PersianCulture.Calendar.MinSupportedDateTime ||
               dateTime > CultureHelper.PersianCulture.Calendar.MaxSupportedDateTime)
            {
                return null;
            }

            return new PersianDate(dateTime);
        }

        public static PersianDate Combine(this PersianDate datePart, PersianDate timePart)
        {
            return new PersianDate(datePart.Year, datePart.Month, datePart.Day, timePart.Hour, timePart.Minute, timePart.Second, timePart.Millisecond);
        }

        public static PersianDate EndOfMonth(this PersianDate dateTime)
        {
            var dt = dateTime.ToDateTime();
            var start = StartOfMonth(dateTime).ToDateTime();
            var pc = CultureHelper.PersianCalendar;
            var nextMonth = pc.AddMonths(start, 1);

            return pc.AddDays(nextMonth, -1);
        }

        public static PersianDate StartOfMonth(this PersianDate dateTime)
        {
            return new PersianDate(dateTime.Year, dateTime.Month, 1);
        }

        public static PersianDate EndOfWeek(this PersianDate dateTime)
        {
            var dt = dateTime.ToDateTime();
            var diff = GetEndOfWeekDiff(dt);
            var pc = CultureHelper.PersianCalendar;

            return pc.AddDays(dt, diff);
        }

        public static PersianDate StartOfWeek(this PersianDate dateTime)
        {
            var dt = dateTime.ToDateTime();
            var diff = GetStartOfWeekDiff(dt);
            var pc = CultureHelper.PersianCalendar;
            
            return pc.AddDays(dt, -diff);
        }

        /// <summary>
        /// Converts the PersianDate to a DateTime equivalant.
        /// </summary>
        /// <param name="persianDate"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this PersianDate persianDate)
        {
            return PersianDateConverter.ToGregorianDateTime(persianDate);
        }

        private static int GetStartOfWeekDiff(DateTime dateTime)
        {
            int diff = 0;

            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    diff = 0;
                    break;
                case DayOfWeek.Sunday:
                    diff = 1;
                    break;
                case DayOfWeek.Monday:
                    diff = 2;
                    break;
                case DayOfWeek.Tuesday:
                    diff = 3;
                    break;
                case DayOfWeek.Wednesday:
                    diff = 4;
                    break;
                case DayOfWeek.Thursday:
                    diff = 5;
                    break;
                case DayOfWeek.Friday:
                    diff = 6;
                    break;
            }

            return diff;
        }

        public static string ToPrettyTime(this DateTime date)
        {
            var pretty = new PrettyTime();
            return pretty.Format(date);
        }

        private static int GetEndOfWeekDiff(DateTime dateTime)
        {
            int diff = 0;

            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    diff = 6;
                    break;
                case DayOfWeek.Sunday:
                    diff = 5;
                    break;
                case DayOfWeek.Monday:
                    diff = 4;
                    break;
                case DayOfWeek.Tuesday:
                    diff = 3;
                    break;
                case DayOfWeek.Wednesday:
                    diff = 2;
                    break;
                case DayOfWeek.Thursday:
                    diff = 1;
                    break;
                case DayOfWeek.Friday:
                    diff = 0;
                    break;
            }

            return diff;
        }
    }
}
