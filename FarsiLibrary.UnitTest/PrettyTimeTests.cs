using System;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Utils.Formatter;
using NUnit.Framework;
using System.Globalization;
using System.Threading;
using FarsiLibrary.Utils;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PrettyTimeTests
    {
        [TestCase(-5, "5 days ago", "en-US") ]
        [TestCase(5, "5 days from now", "en-US")]
        [TestCase(5, "پنج روز بعد", "fa-IR")]
        [TestCase(-5, "پنج روز قبل", "fa-IR")]
        public void Can_Format_Days(int days, string expected, string cultureName)
        {
            using (new CultureSwitchContext(new CultureInfo(cultureName)))
            {
                var datetime = DateTime.Now;
                var then = DateTime.Now.AddDays(days);
                var pretty = new PrettyTime(datetime);

                var result = pretty.Format(then);

                Assert.NotNull(result);
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(-5, "moments ago", "en-US")]
        [TestCase(5, "moments from now", "en-US")]
        [TestCase(5, "چند لحظه بعد", "fa-IR")]
        [TestCase(-5, "چند لحظه قبل", "fa-IR")]
        public void Can_Format_Seconds(int seconds, string expected, string cultureName)
        {
            using (new CultureSwitchContext(new CultureInfo(cultureName)))
            {
                var date = DateTime.Now.AddSeconds(seconds);
                var pretty = new PrettyTime();

                var result = pretty.Format(date);

                Assert.NotNull(result);
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(-10, "10 minutes ago", "en-US")]
        [TestCase(10, "10 minutes from now", "en-US")]
        [TestCase(10, "ده دقیقه بعد", "fa-IR")]
        [TestCase(-10, "ده دقیقه قبل", "fa-IR")]
        public void Can_Format_Minutes(int minutes, string expected, string cultureName)
        {
            using (new CultureSwitchContext(new CultureInfo(cultureName)))
            {
                var datetime = DateTime.Now;
                var then = DateTime.Now.AddMinutes(minutes);
                var pretty = new PrettyTime(datetime);

                var result = pretty.Format(then);

                Assert.NotNull(result);
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(-2, "2 hours ago", "en-US")]
        [TestCase(2, "2 hours from now", "en-US")]
        [TestCase(2, "دو ساعت بعد", "fa-IR")]
        [TestCase(-2, "دو ساعت قبل", "fa-IR")]
        public void Can_Format_Hours(int hours, string expected, string cultureName)
        {
            using (new CultureSwitchContext(new CultureInfo(cultureName)))
            {
                var datetime = DateTime.Now;
                var then = DateTime.Now.AddHours(hours);
                var pretty = new PrettyTime(datetime);

                var result = pretty.Format(then);

                Assert.NotNull(result);
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(-1, "1 year ago", "en-US")]
        [TestCase(2, "2 years from now", "en-US")]
        [TestCase(50, "5 decades from now", "en-US")]
        [TestCase(100, "10 decades from now", "en-US")]
        [TestCase(101, "1 century from now", "en-US")]
        [TestCase(-1, "يک سال قبل", "fa-IR")]
        [TestCase(2, "دو سال بعد", "fa-IR")]
        [TestCase(50, "پنج دهه بعد", "fa-IR")]
        [TestCase(100, "ده دهه بعد", "fa-IR")]
        [TestCase(101, "يک قرن بعد", "fa-IR")]
        public void Can_Format_Years(int years, string expected, string cultureName)
        {
            using (new CultureSwitchContext(new CultureInfo(cultureName)))
            {
                var datetime = DateTime.Now;
                var then = DateTime.Now.AddYears(years);
                var pretty = new PrettyTime(datetime);

                var result = pretty.Format(then);

                Assert.NotNull(result);
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void Can_Compare_Two_Dates()
        {
            var from = new DateTime(2000, 1, 1);
            var to = new DateTime(2001, 1, 1);

            var duration = new PrettyTime(to).Format(from);

            Assert.AreEqual("1 year ago", duration);
        }

        [Test]
        public void Can_Compare_Two_Dates_Regardless_Of_Precedence()
        {
            var from = new DateTime(2000, 1, 1);
            var to = new DateTime(2001, 1, 1);

            var duration = new PrettyTime(from).Format(to);

            Assert.AreEqual("1 year from now", duration);
        }

        [Test]
        public void Default_Comparison_Compares_To_DateTimeNow()
        {
            var justNow = new PrettyTime().Format(DateTime.Now);

            Assert.AreEqual("moments from now", justNow);
        }

        [Test]
        public void Can_Convert_Dates_Using_ExtensionMethod()
        {
            var date = DateTime.Now;
            
            Thread.Sleep(1000); //to simulate delay

            var pretty = date.ToPrettyTime();

            Assert.AreEqual("moments ago", pretty);
        }
    }
}