using System;
using FarsiLibrary.Utils;
using NUnit.Framework;


namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianDateTimeFormatTests
    {
        [Test]
        public void First_Month_Is_Farvardin()
        {
            string s = PersianDateTimeFormatInfo.MonthNames[0];
            Assert.That(s.Contains("فروردین"));
        }

        [Test]
        public void Day_Name_Index_Has_Correct_Mapping()
        {
            Assert.AreEqual("شنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(0));
            Assert.AreEqual("یکشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(1));
            Assert.AreEqual("دوشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(2));
            Assert.AreEqual("ﺳﻪشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(3));
            Assert.AreEqual("چهارشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(4));
            Assert.AreEqual("پنجشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(5));
            Assert.AreEqual("جمعه", PersianDateTimeFormatInfo.GetWeekDayByIndex(6));
        }

        [Test]
        public void Abbr_Day_Index_Has_Correct_Mapping()
        {
            Assert.AreEqual("ش", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(0));
            Assert.AreEqual("ی", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(1));
            Assert.AreEqual("د", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(2));
            Assert.AreEqual("س", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(3));
            Assert.AreEqual("چ", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(4));
            Assert.AreEqual("پ", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(5));
            Assert.AreEqual("ج", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(6));            
        }

        [Test]
        public void Getting_Invalid_Day_Name_Index_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(7));
            Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayByIndex(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayByIndex(7));
        }

        [Test]
        public void Day_Index_Has_Correct_Mapping()
        {
            Assert.AreEqual(0, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Saturday));
            Assert.AreEqual(1, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Sunday));
            Assert.AreEqual(2, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Monday));
            Assert.AreEqual(3, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Tuesday));
            Assert.AreEqual(4, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Wednesday));
            Assert.AreEqual(5, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Thursday));
            Assert.AreEqual(6, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Friday));
        }
    }
}