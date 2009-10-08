using System;
using FarsiLibrary.UnitTest.Mocks;
using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Enums;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PainterFactoryTests
    {
        private readonly MockStyledControl control = new MockStyledControl();

        [Test]
        public void Get_Theme_For_Invalid_Control_Throws()
        {
            Assert.Throws(typeof (InvalidOperationException), () => FAPainterFactory.GetPainter(null));
        }

        [Test]
        public void Get_Office2000_Painter_With_Themes_Available()
        {
            control.SetThemeAvailability(true);
            control.Theme = ThemeTypes.Office2000;

            IFAPainter painter = FAPainterFactory.GetPainter(control);
            Assert.That(painter, Is.TypeOf(typeof(FAPainterOffice2000)));
        }

        [Test]
        public void Get_Office2000_Painter_With_Themes_Unavailable()
        {
            control.SetThemeAvailability(false);
            control.Theme = ThemeTypes.Office2007;

            IFAPainter painter = FAPainterFactory.GetPainter(control);
            Assert.That(painter, Is.TypeOf(typeof(FAPainterOffice2000)));
        }

        [Test]
        public void Get_Office2003_Painter()
        {
            control.SetThemeAvailability(true);
            control.Theme = ThemeTypes.Office2003;

            IFAPainter painter = FAPainterFactory.GetPainter(control);
            Assert.That(painter, Is.TypeOf(typeof(FAPainterOffice2003)));
        }

        [Test]
        public void Get_Office2007_Painter()
        {
            control.SetThemeAvailability(true);
            control.Theme = ThemeTypes.Office2007;

            IFAPainter painter = FAPainterFactory.GetPainter(control);
            Assert.That(painter, Is.TypeOf(typeof(FAPainterOffice2007)));
        }

        [Test]
        public void Get_WindowsXP_Painter()
        {
            control.SetThemeAvailability(true);
            control.Theme = ThemeTypes.WindowsXP;

            IFAPainter painter = FAPainterFactory.GetPainter(control);
            Assert.That(painter, Is.TypeOf(typeof(FAPainterWindowsXP)));
        }
    }
}