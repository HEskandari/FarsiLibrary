using System;
using System.Globalization;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Utils
{
    public static class CultureInfoExtensions
    {
        public static bool IsFarsiCulture(this CultureInfo culture)
        {
            return culture.Equals(CultureHelper.FarsiCulture) || culture.Name.Equals("fa", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsArabicCulture(this CultureInfo culture)
        {
            return culture.Equals(CultureHelper.ArabicCulture) || culture.Name.Equals("ar", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNeutralCulture(this CultureInfo culture)
        {
            return culture.Equals(CultureHelper.NeutralCulture);
        }
    }
}
