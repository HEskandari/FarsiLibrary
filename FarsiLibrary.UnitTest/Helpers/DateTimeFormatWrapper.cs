using System.Globalization;

namespace FarsiLibrary.UnitTest.Helpers
{
    public class DateTimeFormatWrapper
    {
        public static DateTimeFormatInfo GetFormatInfo()
        {
            return new DateTimeFormatInfo
                       {
                           DateSeparator = "-",
                       };
        }
    }
}