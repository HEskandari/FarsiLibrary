using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace FarsiLibrary.Utils.Internals
{
    /// <summary>
    /// Base culture information
    /// </summary>
    internal static class CultureHelper
    {
        private static CultureInfo faCulture;
        private static CultureInfo arCulture;
        private static CultureInfo internalfaCulture;
        private static readonly CultureInfo neuCulture = CultureInfo.InvariantCulture;
        private static readonly PersianCalendar pc = new PersianCalendar();
        private static readonly HijriCalendar hc = new HijriCalendar();
        private static readonly GregorianCalendar gc = new GregorianCalendar();
        private static readonly Dictionary<int, DayOfWeek> PersianDoW = new Dictionary<int, DayOfWeek>();
        private static readonly Dictionary<int, DayOfWeek> GregorianDoW = new Dictionary<int, DayOfWeek>();

        static CultureHelper()
        {
            CreatePersianDayOfWeekMap();
            CreateGregorianDayOfWeekMap();
        }

        private static void CreatePersianDayOfWeekMap()
        {
            PersianDoW.Add(0, DayOfWeek.Saturday);
            PersianDoW.Add(1, DayOfWeek.Sunday);
            PersianDoW.Add(2, DayOfWeek.Monday);
            PersianDoW.Add(3, DayOfWeek.Tuesday);
            PersianDoW.Add(4, DayOfWeek.Wednesday);
            PersianDoW.Add(5, DayOfWeek.Thursday);
            PersianDoW.Add(6, DayOfWeek.Friday);
        }

        private static void CreateGregorianDayOfWeekMap()
        {
            GregorianDoW.Add(0, DayOfWeek.Sunday);
            GregorianDoW.Add(1, DayOfWeek.Monday);
            GregorianDoW.Add(2, DayOfWeek.Tuesday);
            GregorianDoW.Add(3, DayOfWeek.Wednesday);
            GregorianDoW.Add(4, DayOfWeek.Thursday);
            GregorianDoW.Add(5, DayOfWeek.Friday);
            GregorianDoW.Add(6, DayOfWeek.Saturday);
        }

        public static Calendar PersianCalendar
        {
            get { return pc; }
        }

        /// <summary>
        /// Currently selected UICulture
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            get { return CultureInfo.CurrentUICulture; }
        }

        /// <summary>
        /// Instance of Arabic culture
        /// </summary>
        public static CultureInfo ArabicCulture
        {
            get
            {
                if(arCulture == null)
                    arCulture = new CultureInfo("ar-SA");

                return arCulture;
            }
        }

        /// <summary>
        /// Instance of Farsi culture
        /// </summary>
        public static CultureInfo FarsiCulture
        {
            get
            {
                if(faCulture == null)
                    faCulture = new CultureInfo("fa-IR");

                return faCulture;
            }
        }

        /// <summary>
        /// Instance of Persian Culture with correct date formatting.
        /// </summary>
        public static CultureInfo PersianCulture
        {
            get
            {
                if (internalfaCulture == null)
                    internalfaCulture = new PersianCultureInfo();

                return internalfaCulture;
            }
        }

        /// <summary>
        /// Instance of Neutral culture
        /// </summary>
        public static CultureInfo NeutralCulture
        {
            get { return neuCulture; }
        }

        /// <summary>
        /// Returns the day of week based on calendar.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public static int GetDayOfWeek(DateTime dt, Calendar calendar)
        {
            var calendarType = calendar.GetType();
            if (calendarType == typeof (PersianCalendar) ||
                calendarType == typeof (System.Globalization.PersianCalendar))
            {
                return PersianDateTimeFormatInfo.GetDayIndex(dt.DayOfWeek);
            }

            return (int) dt.DayOfWeek;
        }

        /// <summary>
        /// Returns the default calendar for the current culture.
        /// </summary>
        /// <returns></returns>
        public static Calendar CurrentCalendar
        {
            get
            {
                if (IsFarsiCulture())
                {
                    return pc;
                }
                
                if (IsArabicCulture())
                {
                    return hc;
                }
                
                return gc;
            }
        }

        /// <summary>
        /// Finds the corresponding DayOfWeek in specified culture
        /// </summary>
        /// <param name="day"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static DayOfWeek GetCultureDayOfWeek(int day, CultureInfo culture)
        {
            if(culture.IsFarsiCulture())
            {
                return PersianDoW[day];
            }
            
            return GregorianDoW[day];
        }

        public static DateTime MinCultureDateTime
        {
            get { return CurrentCalendar.MinSupportedDateTime; }
        }

        public static DateTime MaxCultureDateTime
        {
            get { return CurrentCalendar.MaxSupportedDateTime; }
        }

        public static bool IsArabicCulture()
        {
            return IsArabicCulture(CurrentCulture);
        }

        public static bool IsArabicCulture(this CultureInfo culture)
        {
            return culture.Equals(ArabicCulture) || 
                   culture.Name.Equals("ar", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsFarsiCulture()
        {
            return IsFarsiCulture(CurrentCulture);
        }

        public static bool IsFarsiCulture(this CultureInfo culture)
        {
            return culture.Name.Equals(FarsiCulture.Name) ||
                   culture.Name.Equals(PersianCulture.Name) ||
                   culture.Name.Equals("fa", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}