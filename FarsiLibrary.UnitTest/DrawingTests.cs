using System;
using FarsiLibrary.UnitTest.Mocks;
using FarsiLibrary.Win.Enums;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class DrawingTests
    {
        [Test]
        public void Color_Initialization()
        {
            var colors = new Office2003ColorStub();

            colors.InitColor(XPThemeType.Homestead);
            colors.InitColor(XPThemeType.Metallic);
            colors.InitColor(XPThemeType.NormalColor);
        }

        [Test]
        public void Color_Initialization_With_Unknown_Theme()
        {
            var colors = new Office2003ColorStub();
            
            Assert.Throws(typeof(IndexOutOfRangeException), () => colors.InitColor(XPThemeType.Unknown));
        }

        [Test]
        public void Initialize_Standard_Colors()
        {
            var colors = new Office2003ColorStub();
            colors.InitStandard();
        }

        [Test]
        public void Can_Reinitialize_Colors()
        {
            var colors = new Office2003ColorStub();
            colors.ReinitializeColors();
        }
    }
}
