using System.Collections.Generic;

namespace FarsiLibrary.Utils
{
    internal class PersianMonthNames
    {
        #region Fields

        public string Farvardin = "فروردین";
        public string Ordibehesht = "ارديبهشت";
        public string Khordad = "خرداد";
        public string Tir = "تير";
        public string Mordad = "مرداد";
        public string Shahrivar = "شهریور";
        public string Mehr = "مهر";
        public string Aban = "آبان";
        public string Azar = "آذر";
        public string Day = "دی";
        public string Bahman = "بهمن";
        public string Esfand = "اسفند";
        private readonly List<string> months;
        private static PersianMonthNames instance;

        #endregion

        #region Ctor

        private PersianMonthNames()
        {
            months = new List<string>
                         {
                             Farvardin,
                             Ordibehesht,
                             Khordad,
                             Tir,
                             Mordad,
                             Shahrivar,
                             Mehr,
                             Aban,
                             Azar,
                             Day,
                             Bahman,
                             Esfand,
                             ""
                         };
        }

        #endregion

        #region Singleton

        public static PersianMonthNames Default
        {
            get
            {
                if(instance == null)
                    instance = new PersianMonthNames();

                return instance;
            }
        }

        #endregion

        #region Indexer

        internal List<string> Months
        {
            get { return months; }
        }

        public string this[int month]
        {
            get { return months[month]; }
        }

        #endregion
    }
}
