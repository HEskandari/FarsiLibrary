using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.WPF.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class DaysHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is Int32)
            {
                return GetDayName((int)value);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("One way conversion.");
        }

        /// <summary>
        /// Gets DayName based on the culture.
        /// </summary>
        /// <param name="dayValue"></param>
        /// <returns></returns>
        private static string GetDayName(int dayValue)
        {
            string dayName;
            var dow = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayValue.ToString(CultureInfo.InvariantCulture));

            if (CultureHelper.IsFarsiCulture())
            {
                dayName = PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(dayValue);
            }
            else if (CultureHelper.IsArabicCulture())
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dow);
                dayName = dayName.Remove(0, 2).Substring(0, 1);
            }
            else
            {
                dayName = CultureHelper.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dow).Substring(0, 1);
            }

            return dayName;
        }
    }
}