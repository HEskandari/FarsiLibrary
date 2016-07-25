using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.WPF.Controls;

namespace FarsiLibrary.WPF.Converters
{
    [ValueConversion(typeof(CalendarDay), typeof(int))]
    public class DayValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is CalendarDay)
            {
                CalendarDay cd = (CalendarDay)value;
                DateTime dt = cd.Date;
                if(CultureHelper.IsFarsiCulture())
                {
                    PersianDate pd = dt.Date;
                    return pd.Day;
                }
                
                return dt.Day;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}