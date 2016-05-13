using System;
using System.Globalization;
using FarsiLibrary.Utils;
using FarsiLibrary.WPF.Converters;

namespace FarsiLibrary.WPFDemo.Converters
{
    public class LongDateTimeConverter : DateTimeValueConverter
    {
        protected override string FormatDateValue(DateTime date, DateTimeFormatInfo dateTimeFormat)
        {
            if (CultureInfo.CurrentUICulture.Name.Equals("fa-ir", StringComparison.InvariantCultureIgnoreCase))
            {
                PersianDate pd = date.ToPersianDate();
                return pd.ToWritten();
            }

            return date.ToString("D");
        }
    }
}