using System.Globalization;
using FarsiLibrary.Localization;
using FarsiLibrary.Win.Controls;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class LocalizerManagerTest
    {
        private CultureInfo farsiCulture;
        private CultureInfo arabicCulture;
        private CultureInfo spanishCulture;
        private FAMonthView mockMonthView;

        [SetUp]
        public void Setup()
        {
            FALocalizeManager.Instance.CustomCulture = null;
            FALocalizeManager.Instance.CustomLocalizer = null;
            mockMonthView = new FAMonthView(false);
            farsiCulture = new CultureInfo("fa-IR");
            arabicCulture = new CultureInfo("ar-SA");
            spanishCulture = new CultureInfo("es-ES");
        }

        [Test]
        public void Arabic_Culture()
        {
            Assert.AreEqual(arabicCulture, FALocalizeManager.Instance.ArabicCulture);
            Assert.AreEqual(arabicCulture.Name, FALocalizeManager.Instance.ArabicCulture.Name);
            Assert.AreNotSame(arabicCulture, FALocalizeManager.Instance.ArabicCulture);
        }

        [Test]
        public void English_Culture_Uses_Invariant()
        {
            Assert.AreEqual(FALocalizeManager.Instance.InvariantCulture, CultureInfo.InvariantCulture);
            Assert.AreSame(FALocalizeManager.Instance.InvariantCulture, CultureInfo.InvariantCulture);
        }

        [Test]
        public void Farsi_Culture()
        {
            Assert.AreEqual(farsiCulture, FALocalizeManager.Instance.FarsiCulture);
            Assert.AreEqual(farsiCulture.Name, FALocalizeManager.Instance.FarsiCulture.Name);
            Assert.AreNotSame(farsiCulture, FALocalizeManager.Instance.FarsiCulture);
        }

        [Test]
        public void Setting_LocalizeManager_To_CustomCulture()
        {
            FALocalizeManager.Instance.CustomCulture = spanishCulture;

            Assert.AreEqual(FALocalizeManager.Instance.CustomCulture, spanishCulture);
            Assert.AreSame(FALocalizeManager.Instance.CustomCulture, spanishCulture);

            Assert.AreEqual(mockMonthView.DefaultCulture, spanishCulture);
            Assert.AreSame(mockMonthView.DefaultCulture, spanishCulture);
        }


        [Test]
        public void Get_Localizer_For_Arabic_Culture()
        {
            var loc = FALocalizeManager.Instance.GetLocalizerByCulture(arabicCulture);
            
            Assert.That(loc, Is.TypeOf(typeof(ARLocalizer)));
        }

        [Test]
        public void Get_Localizer_For_Farsi_Culture()
        {
            var loc = FALocalizeManager.Instance.GetLocalizerByCulture(farsiCulture);
            
            Assert.That(loc, Is.TypeOf(typeof(FALocalizer)));
        }

        [Test]
        public void Get_Localizer_For_Invariant_Culture()
        {
            var loc = FALocalizeManager.Instance.GetLocalizer();

            Assert.That(loc, Is.TypeOf(typeof(ENLocalizer)));
        }

        [Test]
        public void Get_Localizer_For_Unknown_Culture()
        {
            var loc = FALocalizeManager.Instance.GetLocalizerByCulture(spanishCulture);
            
            Assert.NotNull(loc);
            Assert.That(loc, Is.TypeOf(typeof(ENLocalizer)));
        }
    }
}