using System;
using FarsiLibrary.Utils;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class DateConversionTest
    {
        private readonly PersianCalendar calendar = new PersianCalendar();

        [Test]
        public void DateTime_Automatically_Cast_To_PersianDate_Equality()
        {
            var pd = new PersianDate(1403, 12, 30);
            PersianDate dt = new DateTime(2025, 3, 20);

            Assert.AreEqual(dt.Year, pd.Year);
            Assert.AreEqual(dt.Month, pd.Month);
            Assert.AreEqual(dt.Day, pd.Day);
        }

        [Test]
        public void Check_Equality_After_Conversion()
        {
            var dt = new DateTime(2025, 3, 20);
            PersianDate pd1 = new DateTime(2025, 3, 20);
            PersianDate pd2 = dt;

            Assert.AreEqual(pd1.Year, pd2.Year);
            Assert.AreEqual(pd1.Month, pd2.Month);
            Assert.AreEqual(pd1.Day, pd2.Day);
            Assert.AreEqual(pd1, pd2);
            Assert.True(pd1 == pd2);
        }

        [Test]
        public void Leap_Years_Work_When_Converted()
        {
            var dt1 = new DateTime(2025, 3, 20);
            PersianDate pd1 = dt1;
            Assert.True(calendar.IsLeapYear(pd1.Year), "Should be a leap year.");
            Assert.True(calendar.IsLeapYear(pd1.Year, PersianCalendar.PersianEra));

            var dt2 = new DateTime(2026, 3, 20);
            PersianDate pd2 = dt2;
            Assert.False(calendar.IsLeapYear(pd2.Year), "Should not be a leap year.");
            Assert.False(calendar.IsLeapYear(pd2.Year, PersianCalendar.PersianEra));
        }

        [Test]
        public void Normal_Years_Work_When_Converted()
        {
            var pd = new PersianDate(1387, 1, 1);
            Assert.True(calendar.IsLeapYear(pd.Year));
        }
    }
}
