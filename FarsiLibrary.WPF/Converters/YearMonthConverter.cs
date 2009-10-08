using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.WPF.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class YearMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is DateTime)
            {
                DateTime dt = (DateTime)value;
                string mode = "Year";

                if(parameter != null)
                    mode = parameter.ToString().ToUpper();

                switch(mode.ToUpper())
                {
                    case "YEAR":
                        return GetYearValue(dt, CultureHelper.CurrentCulture);

                    case "MONTH":
                        return GetMonthValue(dt, CultureHelper.CurrentCulture);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("One way conversion.");
        }

        private static string GetYearValue(DateTime dt, CultureInfo culture)
        {
            if(culture.IsFarsiCulture())
            {
                PersianDate pd = dt;
                return pd.Year.ToString(culture);
            }
            
            return dt.Year.ToString(culture);
        }

        private static string GetMonthValue(DateTime dt, CultureInfo culture)
        {
            if(culture.IsFarsiCulture())
            {
                PersianDate pd = dt;
                return PersianDateTimeFormatInfo.MonthNames[pd.Month - 1];
            }

            return CultureHelper.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
        }
    }
}