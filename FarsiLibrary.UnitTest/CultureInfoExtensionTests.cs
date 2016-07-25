using System.Globalization;
using NUnit.Framework;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class CultureInfoExtensionTests
    {
        [Test]
        public void Can_Determine_Farsi_Culture()
        {
            Assert.True(new CultureInfo("fa-ir").IsFarsiCulture());
            Assert.True(new PersianCultureInfo().IsFarsiCulture());
            Assert.True(new CultureInfo("fa").IsFarsiCulture());
            Assert.False(new CultureInfo("fr").IsFarsiCulture());
        }

        [Test]
        public void Can_Determine_Arabic_Culture()
        {
            Assert.True(new CultureInfo("ar").IsArabicCulture());
            Assert.True(new CultureInfo("ar-sa").IsArabicCulture());
            Assert.False(new CultureInfo("fa-ir").IsArabicCulture());
        }
    }
}
