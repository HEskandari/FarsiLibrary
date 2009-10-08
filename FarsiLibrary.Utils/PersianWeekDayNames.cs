using System.Collections.Generic;

namespace FarsiLibrary.Utils
{
    internal class PersianWeekDayNames
    {
        #region fields

        public string Shanbeh = "شنبه";
        public string Yekshanbeh = "یکشنبه";
        public string Doshanbeh = "دوشنبه";
        public string Seshanbeh = "ﺳﻪشنبه";
        public string Chaharshanbeh = "چهارشنبه";
        public string Panjshanbeh = "پنجشنبه";
        public string Jomeh = "جمعه";

        public string Sh = "ش";
        public string Ye = "ی";
        public string Do = "د";
        public string Se = "س";
        public string Ch = "چ";
        public string Pa = "پ";
        public string Jo = "ج";

        private readonly List<string> days;
        private readonly List<string> daysAbbr;
        private static PersianWeekDayNames instance;

        #endregion

        #region Ctor

        private PersianWeekDayNames()
        {
            days = new List<string>
                       {
                           Yekshanbeh,
                           Doshanbeh,
                           Seshanbeh,
                           Chaharshanbeh,
                           Panjshanbeh,
                           Jomeh,
                           Shanbeh,
                       };

            daysAbbr = new List<string>
                           {
                               Ye,
                               Do,
                               Se,
                               Ch,
                               Pa,
                               Jo,
                               Sh,
                           };
        }

        #endregion

        #region Indexer

        public static PersianWeekDayNames Default
        {
            get
            {
                if(instance == null)
                    instance = new PersianWeekDayNames();

                return instance;
            }
        }

        #endregion

        #region Props

        internal List<string> Days
        {
            get { return days; }
        }

        internal List<string> DaysAbbr
        {
            get { return daysAbbr; }
        }

        #endregion
    }
}
