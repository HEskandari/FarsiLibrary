using FarsiLibrary.UnitTest.Mocks;
using FarsiLibrary.Win.Enums;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class BaseStyledControlTest
    {
        private readonly MockStyledControl control;

        public BaseStyledControlTest()
        {
            control = new MockStyledControl();
        }

        [Test]
        public void Setting_Theme_Property_When_No_Style_Is_Available()
        {
            control.SetThemeAvailability(false);
            Assert.False(control.UseThemes);

            control.Theme = ThemeTypes.Office2007;
            Assert.AreEqual(control.Theme, ThemeTypes.Office2000);
        }

        [Test]
        public void Setting_Theme_Property_When_Style_Is_Available()
        {
            control.SetThemeAvailability(true);
            Assert.True(control.UseThemes);

            control.Theme = ThemeTypes.Office2007;
            Assert.AreEqual(control.Theme, ThemeTypes.Office2007);
        }
    }
}