using System;
using System.Globalization;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Utils;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PrettyFormatterTests
    {
        [Test]
        public void Formattting_With_Seconds_Difference()
        {
            var date = DateTime.Now.AddSeconds(-30);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("just now", result);
        }

        [Test]
        public void Formatting_With_One_Minute_Difference()
        {
            var date = DateTime.Now.AddMinutes(-1);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("a minute ago", result);
        }

        [Test]
        public void Formatting_With_Minutes_Difference()
        {
            var date = DateTime.Now.AddMinutes(-10);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("10 minutes ago", result);
        }

        [Test]
        public void Formatting_With_An_Hour_Difference()
        {
            var date = DateTime.Now.AddHours(-1);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("an hour ago", result);
        }

        [Test]
        public void Formatting_With_Hours_Difference()
        {
            var date = DateTime.Now.AddHours(-5);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("5 hours ago", result);
        }

        [Test]
        public void Formatting_With_A_Day_Difference()
        {
            var date = DateTime.Now.AddDays(-1);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("yesterday", result);
        }

        [Test]
        public void Formatting_With_Days_Difference()
        {
            var date = DateTime.Now.AddDays(-3);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("3 days ago", result);
        }

        [Test]
        public void Formatting_With_One_Week_Difference()
        {
            var date = DateTime.Now.AddDays(-7);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("a week ago", result);
        }

        [Test]
        public void Formatting_With_Weeks_Difference()
        {
            var date = DateTime.Now.AddDays(-8);
            var formatter = new PrettyFormatter();

            var result = formatter.Format(date);

            Assert.AreEqual("2 weeks ago", result);
        }

        [Test]
        public void Can_Convert_PersianDate()
        {
            using(var culture = new CultureSwitchContext(new CultureInfo("fa-ir")))
            {
                var pd = PersianDate.Now;

                var result = pd.ToPrettyDate();

                Assert.AreEqual("???? ????", result);
            }
        }
    }
}