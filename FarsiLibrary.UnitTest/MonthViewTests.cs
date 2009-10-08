using System;
using System.Globalization;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Win.Controls;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class MonthViewTests
    {
        [Test]
        public void Should_Return_Saturday_As_First_Day_Of_Week()
        {
            using (new CultureSwitchContext(new CultureInfo("fa-ir")))
            {
                var mv = new FAMonthView();
                var firstDayOfWeek = mv.GetAbbrDayName(DayOfWeek.Saturday);

                Assert.AreEqual("ش", firstDayOfWeek);
            }
        }

        [Test]
        public void Should_Return_Friday_As_Last_Day_Of_Week()
        {
            using (new CultureSwitchContext(new CultureInfo("fa-ir")))
            {
                var mv = new FAMonthView();
                var firstDayOfWeek = mv.GetAbbrDayName(DayOfWeek.Friday);

                Assert.AreEqual("ج", firstDayOfWeek);
            }            
        }

        [Test]
        public void Should_Return_Wednesday_As_Fourth_Of_March_2009()
        {
            using(new CultureSwitchContext(CultureInfo.InvariantCulture))
            {
                var mv = new FAMonthView();
                mv.SelectedDateTime = new DateTime(2009, 3, 4);

                var dayOfWeek = mv.GetAbbrDayName(mv.SelectedDateTime.Value.DayOfWeek);

                Assert.AreEqual("Wed", dayOfWeek);
            }
        }
    }
}