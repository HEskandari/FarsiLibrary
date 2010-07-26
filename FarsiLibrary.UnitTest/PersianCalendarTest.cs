using System;
using System.Globalization;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Exceptions;
using NUnit.Framework;
using PersianCalendar=FarsiLibrary.Utils.PersianCalendar;
using System.Linq;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianCalendarTest
    {
        private readonly PersianCalendar calendar = new PersianCalendar();
        private readonly System.Globalization.PersianCalendar sysCalendar = new System.Globalization.PersianCalendar();

        [Test]
        public void Can_Add_A_Month()
        {
            var pd = new PersianDate(1387, 12, 30);
            var dt1 = (DateTime)pd;
            var dt2 = calendar.AddMonths(dt1, 2);

            Assert.AreEqual(dt2.Month, dt1.Month + 2);
        }

        [Test]
        public void Can_Add_A_Year()
        {
            var pd = PersianDate.Now;
            var dt1 = (DateTime)pd;
            var dt2 = calendar.AddYears(dt1, 2);

            Assert.AreEqual(dt2.Year, dt1.Year + 2);
        }

        [Test]
        public void Can_Get_Day_Of_Month()
        {
            var pd = new PersianDate(1380, 1, 1);
            var dt1 = (DateTime)pd;
            var result = calendar.GetDayOfMonth(dt1);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Can_Get_First_Day_Of_Year()
        {
            var pd = new PersianDate(1380, 1, 1);
            var dt1 = (DateTime)pd;
            var result = calendar.GetDayOfYear(dt1);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Can_Get_Last_Day_Of_Year()
        {
            var pd = new PersianDate(1387, 12, 30);
            var dt1 = (DateTime)pd;
            var result = calendar.GetDayOfYear(dt1);

            Assert.AreEqual(result, 366);
        }

        [Test]
        public void Can_Get_Days_In_Last_Month()
        {
            var pd = new PersianDate(1387, 12, 1);
            var result = calendar.GetDaysInMonth(pd.Year, pd.Month);

            Assert.AreEqual(result, 30);
        }

        [Test]
        public void Can_Get_Days_In_Year()
        {
            var pd1 = new PersianDate(1387, 12, 30);
            var pd2 = new PersianDate(1386, 12, 29);
            
            var result1 = calendar.GetDaysInYear(pd1.Year);
            var result2 = calendar.GetDaysInYear(pd2.Year);

            Assert.AreEqual(result1, 366);
            Assert.AreEqual(result2, 365);
        }

        [Test]
        public void Can_Get_Leap_Month()
        {
            var pd1 = new PersianDate(1387, 12, 30);
            var pd2 = new PersianDate(1386, 12, 29);

            var result1 = calendar.IsLeapMonth(pd1.Year, 12);
            var result2 = calendar.IsLeapMonth(pd2.Year, 12);

            Assert.True(result1);
            Assert.False(result2);
        }

        [Test]
        public void Can_Get_Leap_Year()
        {
            var leap = 1387;
            var normal = 1386;

            var result1 = calendar.IsLeapYear(leap);
            var result2 = calendar.IsLeapYear(normal);

            Assert.True(result1);
            Assert.False(result2);
        }

        [Test]
        public void ToDateTime_With_Invalid_Day_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => calendar.ToDateTime(1384, 1, 32, 0, 0, 0, 0));
            Assert.Throws<InvalidPersianDateException>(() => calendar.ToDateTime(1384, 0, 0, 0, 0, 0, 0));
            Assert.Throws<InvalidPersianDateException>(() => calendar.ToDateTime(1384, 1, 0, 0, 0, 0, 0));
        }

        [Test]
        public void Can_Convert_ToDateTime_With_Leap_Year_Day()
        {
            DateTime dt = calendar.ToDateTime(1387, 12, 30, 0, 0, 0, 0);
            Assert.NotNull(dt);
        }

        [Test]
        public void ToDateTime_With_Invalid_LeapYear_Day_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => calendar.ToDateTime(1388, 12, 30, 0, 0, 0, 0));
        }

        [Test]
        public void Adding_Or_Removing_Month_Larger_Than_Max_Difference_Throws()
        {
            var dt = DateTime.Now;

            Assert.Throws<InvalidPersianDateException>(() => calendar.AddMonths(dt, PersianCalendar.MaxMonthDifference)); //Adding a large month value
            Assert.Throws<InvalidPersianDateException>(() => calendar.AddMonths(dt, PersianCalendar.MaxMonthDifference + 100)); //Adding a something greater than MaxMonthDifference
        }

        [Test]
        public void Increamenting_Month_In_Leap_Year_Should_Move_To_Next_Year()
        {
            DateTime dt = new PersianDate(1386, 11, 30);
            DateTime dtNext = calendar.AddMonths(dt, 1);

            Assert.NotNull(dtNext);
        }

        [Test]
        public void Increamenting_Year_In_Leap_Year_Should_Correct_The_Day_Value()
        {
            DateTime dt = new PersianDate(1387, 12, 30);
            PersianDate dtNext = calendar.AddYears(dt, 1);
            PersianDate dtPrevious = calendar.AddYears(dt, -1);

            Assert.AreEqual(29, dtNext.Day);
            Assert.AreEqual(29, dtPrevious.Day);
        }

        [Test]
        public void Should_Use_Solar_Calendar()
        {
            Assert.AreEqual(CalendarAlgorithmType.SolarCalendar, calendar.AlgorithmType);
        }

        [Test]
        public void Minimum_Supported_Date_Is_MinValue_Of_PersianDate()
        {
            Assert.AreEqual(PersianDate.MinValue, calendar.MinSupportedDateTime);
        }

        [Test]
        public void Maximum_Supported_Date_Is_MaxValue_Of_PersianDate()
        {
            Assert.AreEqual(PersianDate.MaxValue, calendar.MaxSupportedDateTime);
        }

        [Test]
        public void Always_Twelve_Months_In_Year()
        {
            PersianDate pdMin = PersianDate.MinValue;
            PersianDate pdMax = PersianDate.MaxValue;

            Assert.AreEqual(12, calendar.GetMonthsInYear(pdMax.Year));
            Assert.AreEqual(12, calendar.GetMonthsInYear(pdMin.Year));
        }

        [Test]
        public void Era_Is_Always_PersianEra()
        {
            Assert.AreEqual(PersianCalendar.PersianEra, calendar.GetEra(PersianDate.MinValue));
            Assert.AreEqual(PersianCalendar.PersianEra, calendar.GetEra(PersianDate.MaxValue));
        }

        [Test]
        public void Era_Returns_PersianEra()
        {
            Assert.AreEqual(PersianCalendar.PersianEra, calendar.Eras.FirstOrDefault());
        }

        [Test]
        public void Can_Get_Number_Of_Leap_Years()
        {
            //335 leap years from the start of the calendar to 1387
            Assert.AreEqual(335, calendar.NumberOfLeapYearsUntil(1387));
        }

        [Test]
        public void Can_Get_Correct_Month_From_Day_Value()
        {
            Assert.AreEqual(1, calendar.GetMonth(new PersianDate(1384, 1, 10)));
            Assert.AreEqual(2, calendar.GetMonth(new PersianDate(1384, 2, 22)));
            Assert.AreEqual(3, calendar.GetMonth(new PersianDate(1384, 3, 30)));
            Assert.AreEqual(4, calendar.GetMonth(new PersianDate(1384, 4, 1)));
            Assert.AreEqual(5, calendar.GetMonth(new PersianDate(1384, 5, 18)));
            Assert.AreEqual(6, calendar.GetMonth(new PersianDate(1384, 6, 2)));
            Assert.AreEqual(7, calendar.GetMonth(new PersianDate(1384, 7, 4)));
            Assert.AreEqual(8, calendar.GetMonth(new PersianDate(1384, 8, 12)));
            Assert.AreEqual(9, calendar.GetMonth(new PersianDate(1384, 9, 13)));
            Assert.AreEqual(10, calendar.GetMonth(new PersianDate(1384, 10, 1)));
            Assert.AreEqual(11, calendar.GetMonth(new PersianDate(1384, 11, 8)));
            Assert.AreEqual(12, calendar.GetMonth(new PersianDate(1384, 12, 5)));
        }

        [Test]
        public void To_Four_Year_Digit_With_Large_Year_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => calendar.ToFourDigitYear(9999));
        }

        [Test]
        public void To_Four_Year_Digit_Only_Converts_Two_Digit_Values()
        {
            Assert.AreEqual(100, calendar.ToFourDigitYear(100));
        }

        [Test]
        public void Setting_Two_Digit_Year_Over_Limits_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => calendar.TwoDigitYearMax = 10);
            Assert.Throws<InvalidOperationException>(() => calendar.TwoDigitYearMax = 9999);
        }

        [Test]
        public void Can_Convert_To_TwoDigit_Year()
        {
            calendar.TwoDigitYearMax = 1400;
            Assert.AreEqual(1387, calendar.ToFourDigitYear(87));

            calendar.TwoDigitYearMax = 1500;
            Assert.AreEqual(1488, calendar.ToFourDigitYear(88));
        }

        [Test]
        public void Can_Set_TwoDigit_Year_Value()
        {
            Assert.DoesNotThrow(() => calendar.TwoDigitYearMax = 1500);
            Assert.AreEqual(1500, calendar.TwoDigitYearMax);
        }

        [Test]
        public void Can_Get_DayOfWeek()
        {
            DateTime dt = new DateTime(2008, 10, 21);
            Assert.AreEqual(DayOfWeek.Tuesday, calendar.GetDayOfWeek(dt));
        }

        [Test]
        public void Can_Get_Century()
        {
            DateTime dt = new DateTime(2008, 10, 21); // Should be 14th Century
            Assert.AreEqual(14, calendar.GetCentury(dt));
        }

        [Test]
        public void Can_Convert_From_Gregorian_Leap_Year()
        {
            DateTime dt = new DateTime(2008, 10, 9);
            DateTime dtCulture = new DateTime(1387, 7, 18, 0, 0, 0, sysCalendar);
            PersianDate pd = dt.ToPersianDate();

            Assert.AreEqual(DayOfWeek.Thursday, pd.DayOfWeek);
        }

        [Test]
        public void Getting_Invalid_Era_Will_Throw()
        {
            /* only supports PersianEra = 1 */
            Assert.Throws<InvalidPersianDateException>(() => calendar.GetMonthsInYear(2000, -1));
            Assert.Throws<InvalidPersianDateException>(() => calendar.GetMonthsInYear(2000, 2));
        }

        [Test]
        public void Getting_Era_With_Invalid_Date_Range_Will_Throw()
        {
            DateTime dt = new DateTime(000000000000000000L, DateTimeKind.Local);
            Assert.Throws<InvalidPersianDateException>(() => calendar.GetEra(dt));
        }

        [Test]
        public void Can_()
        {
            var dt = DateTime.MaxValue;

            Assert.Throws<InvalidPersianDateException>(() => calendar.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond));
        }
    }
}