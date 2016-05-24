using System.Globalization;
using System.Threading;
using FarsiLibrary.Localization;
using FarsiLibrary.Win.Controls;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class MonthViewCultureTest
    {
        private readonly FAMonthView mockMonthView;
        private readonly CultureInfo farsiCulture;
        private readonly CultureInfo arabicCulture;
        private readonly CultureInfo spanishCulture;

        public MonthViewCultureTest()
        {
            FALocalizeManager.Instance.CustomCulture = null;
            mockMonthView = new FAMonthView(false);
            farsiCulture = new CultureInfo("fa-IR");
            arabicCulture = new CultureInfo("ar-SA");
            spanishCulture = new CultureInfo("es-ES");
        }

        [Test]
        public void Thread_UICulture_Change_Should_Reflect_DefaultCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = farsiCulture;
            
            Assert.AreEqual(mockMonthView.DefaultCulture, farsiCulture);
        }

        [Test]
        public void Thread_Culture_Change_Should_Not_Reflect_DefaultCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
            Thread.CurrentThread.CurrentCulture = farsiCulture;
 
            Assert.AreNotEqual(mockMonthView.DefaultCulture, farsiCulture);
        }

        [Test]
        public void Default_Culture_Should_Return_LocalizeManager_Instances()
        {
            Thread.CurrentThread.CurrentUICulture = farsiCulture;

            Assert.AreNotSame(mockMonthView.DefaultCulture, farsiCulture);
            Assert.AreSame(mockMonthView.DefaultCulture, FALocalizeManager.Instance.FarsiCulture);
        }

        [Test]
        public void Setting_Thread_Culture_To_Uknown_Culture_Will_Return_Invariant()
        {
            Thread.CurrentThread.CurrentUICulture = spanishCulture;

            Assert.AreEqual(mockMonthView.DefaultCulture, CultureInfo.InvariantCulture);
            Assert.AreSame(mockMonthView.DefaultCulture, FALocalizeManager.Instance.InvariantCulture);
        }
    }
}