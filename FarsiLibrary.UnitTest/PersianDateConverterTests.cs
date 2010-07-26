using System;
using System.Globalization;
using FarsiLibrary.Utils;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianDateConverterTests
    {
        [Test]
        public void Can_Get_DayOfWeek_From_PersianDate_Instance()
        {
            var pd = new PersianDate(1387, 7, 7); //7 Mehr equals Doshanbeh
            var weekDay = PersianDateConverter.DayOfWeek(pd);

            Assert.AreEqual(PersianWeekDayNames.Default.Yekshanbeh, weekDay);
        }

        [Test]
        public void Can_Get_DayOfWeek_From_DateTime_Instance()
        {
            var dt = new DateTime(2008, 10, 21); //October 30th, equals Seshanbeh
            var weekday = PersianDateConverter.DayOfWeek(dt);

            Assert.AreEqual(PersianWeekDayNames.Default.Seshanbeh, weekday);
        }

        [Test]
        public void Converting_Out_Of_Range_Dates_Will_ReturnEmpty()
        {
            var pd = (PersianDate)PersianDate.MinValue;
            var weekday = PersianDateConverter.DayOfWeek(pd);

            Assert.AreEqual(string.Empty, weekday);
        }

        [Test]
        public void Day_Of_Week_Has_Correct_Mapping()
        {
            var dt = new DateTime(2008, 10, 1);
            var weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Chaharshanbeh, weekday);

            dt = new DateTime(2008, 10, 2);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Panjshanbeh, weekday);

            dt = new DateTime(2008, 10, 3);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Jomeh, weekday);

            dt = new DateTime(2008, 10, 4);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Shanbeh, weekday);

            dt = new DateTime(2008, 10, 5);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Yekshanbeh, weekday);

            dt = new DateTime(2008, 10, 6);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Doshanbeh, weekday);

            dt = new DateTime(2008, 10, 7);
            weekday = PersianDateConverter.DayOfWeek(dt);
            Assert.AreEqual(PersianWeekDayNames.Default.Seshanbeh, weekday);
        }

        [Test]
        public void Can_Get_Correct_Number_Of_Months()
        {
            var pd = new PersianDate(1380, 7, 1);
            Assert.AreEqual(30, pd.MonthDays);
        }

        [Test]
        public void Can_Convert_Strings_ToDateTime()
        {
            PersianDate pd = new PersianDate(1380, 1, 1);
            DateTime converted = pd.ToDateTime();
            DateTime dt = PersianDateConverter.ToGregorianDateTime(pd.ToString());

            Assert.AreEqual(dt, converted);
        }

        [Test]
        public void Can_Convert_LeapYears_Correctly()
        {
            DateTime dt = new DateTime(2009, 3, 20); //Converts to a leap year in Persian Date (30th Esfand 1387)
            PersianDate pd = PersianDateConverter.ToPersianDate(dt);

            Assert.AreEqual(12, pd.Month);
            Assert.AreEqual(30, pd.Day);
            Assert.AreEqual(30, pd.MonthDays);
        }

        [Test]
        public void Can_Convert_Leap_PersianDate_To_DateTime_String()
        {
            var dt = new DateTime(2009, 3, 20);
            var pd = dt.ToPersianDate();
            string date = PersianDateConverter.ToGregorianDate(pd);

            Assert.AreEqual(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture), date);
        }

        [Test]
        public void Can_Convert_Normal_PersianDate_To_DateTime_String()
        {
            var dt = new DateTime(2008, 10, 20); // 29th Mehr 1387
            var pd = dt.ToPersianDate();
            string date = PersianDateConverter.ToGregorianDate(pd);

            Assert.AreEqual(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture), date);
        }

        [Test]
        public void Can_Convert_With_Time_Part()
        {
            var dt = new DateTime(2000, 1, 1); //1378/10/11
            var pd = PersianDateConverter.ToPersianDate(dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), new TimeSpan(11, 45, 26));

            Assert.AreEqual(11, pd.Hour);
            Assert.AreEqual(45, pd.Minute);
            Assert.AreEqual(26, pd.Second);
            Assert.AreEqual(1378, pd.Year);
            Assert.AreEqual(10, pd.Month);
            Assert.AreEqual(11, pd.Day);
        }

        [Test]
        public void Can_Convert_Directly_To_Gregorian_Date_From_String()
        {
            var dt = PersianDateConverter.ToGregorianDateTime("1387/07/29 02:31:30"); //2008/10/20

            Assert.That(dt.Year, Is.EqualTo(2008));
            Assert.That(dt.Month, Is.EqualTo(10));
            Assert.That(dt.Day, Is.EqualTo(20));
            Assert.That(dt.Hour, Is.EqualTo(2));
            Assert.That(dt.Minute, Is.EqualTo(31));
            Assert.That(dt.Second, Is.EqualTo(30));
        }
    }
}
