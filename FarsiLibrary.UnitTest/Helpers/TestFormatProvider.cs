using System;
using FarsiLibrary.Utils;

namespace FarsiLibrary.UnitTest.Helpers
{
    public class TestFormatProvider : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            return new CustomFormatter();
        }

        private class CustomFormatter : ICustomFormatter
        {
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                PersianDate pd = arg as PersianDate;
                if (pd != null)
                {
                    if (format == "CustomYearMonth")
                        return pd.Year + " -- " + pd.Month;
                }

                return pd.ToString(format);
            }
        }
    }
}