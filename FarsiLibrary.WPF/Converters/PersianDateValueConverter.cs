using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.Utils;

namespace FarsiLibrary.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(PersianDate))]
    public class PersianDateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string)
            {
                string converting = (string) value;
                if (string.IsNullOrEmpty(converting))
                {
                    return null;
                }

                DateTime dt;
                if(DateTime.TryParse(converting, out dt))
                {
                    return dt.ToPersianDate();
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is PersianDate)
            {
                PersianDate pd = (PersianDate) value;
                return pd.ToDateTime();
            }

            return null;
        }
    }
}
