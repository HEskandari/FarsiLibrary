using System;
using System.Globalization;

namespace FarsiLibrary.WPFDemo.Localization
{
    public class CultureHelper
    {
        public static CultureInfo DefaultCulture
        {
            get { return CultureInfo.InvariantCulture; }
        }

        public static Calendar DefaultCalendar
        {
            get { return DefaultCulture.Calendar; }
        }

        public static CultureInfo CurrentUICulture
        {
            get { return System.Threading.Thread.CurrentThread.CurrentUICulture; }
        }

        public static Calendar CurrentCalendar
        {
            get { return CurrentUICulture.Calendar; }
        }

        public static DateTime CurrentInvariantDateTime
        {
            get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DefaultCalendar); }
        }
    }
}