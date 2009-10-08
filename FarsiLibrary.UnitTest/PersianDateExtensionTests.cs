using System;
using FarsiLibrary.Utils;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianDateExtensionTests
    {
        [Test]
        public void Can_Get_End_Of_Week()
        {
            var today = new PersianDate(1388, 4, 25);
            var weekend = today.EndOfWeek();

            Assert.That((int)weekend.DayOfWeek, Is.EqualTo((int)DayOfWeek.Friday));
            Assert.That(weekend.Year, Is.EqualTo(1388));
            Assert.That(weekend.Month, Is.EqualTo(4));
            Assert.That(weekend.Day, Is.EqualTo(26));
        }

        [Test]
        public void Can_Get_Start_Of_Week()
        {
            var today = new PersianDate(1388, 4, 25);
            var weekend = today.StartOfWeek();

            Assert.That((int)weekend.DayOfWeek, Is.EqualTo((int)DayOfWeek.Saturday));
            Assert.That(weekend.Year, Is.EqualTo(1388));
            Assert.That(weekend.Month, Is.EqualTo(4));
            Assert.That(weekend.Day, Is.EqualTo(20));            
        }

        [Test]
        public void Can_Get_End_Of_Month()
        {
            var today = new PersianDate(1388, 4, 20);
            var weekend = today.EndOfMonth();

            Assert.That((int)weekend.DayOfWeek, Is.EqualTo((int)DayOfWeek.Wednesday));
            Assert.That(weekend.Year, Is.EqualTo(1388));
            Assert.That(weekend.Month, Is.EqualTo(4));
            Assert.That(weekend.Day, Is.EqualTo(31));
        }

        [Test]
        public void Can_Get_Start_Of_Month()
        {
            var today = new PersianDate(1388, 4, 20);
            var weekend = today.StartOfMonth();

            Assert.That((int)weekend.DayOfWeek, Is.EqualTo((int)DayOfWeek.Monday));
            Assert.That(weekend.Year, Is.EqualTo(1388));
            Assert.That(weekend.Month, Is.EqualTo(4));
            Assert.That(weekend.Day, Is.EqualTo(1));
        }

        [Test]
        public void Can_Combine_Date_And_Time_Parts()
        {
            var prevDate = new PersianDate(1380, 1, 1, 5, 22, 30);
            var today = new PersianDate(1388, 4, 25);
            var combined = today.Combine(prevDate);

            Assert.That(combined.Year, Is.EqualTo(1388));
            Assert.That(combined.Month, Is.EqualTo(4));
            Assert.That(combined.Day, Is.EqualTo(25));
            Assert.That(combined.Hour, Is.EqualTo(5));
            Assert.That(combined.Minute, Is.EqualTo(22));
            Assert.That(combined.Second, Is.EqualTo(30));
        }

        [Test]
        public void Can_Get_End_Of_Week_For_Every_Day_Of_The_Week()
        {
            var firstDay = new PersianDate(1388, 7, 4);
            var secondDay = new PersianDate(1388, 7, 5);
            var thirdDay = new PersianDate(1388, 7, 6);
            var forthDay = new PersianDate(1388, 7, 7);
            var fifthDay = new PersianDate(1388, 7, 8);
            var sixthDay = new PersianDate(1388, 7, 9);
            var seventhDay = new PersianDate(1388, 7, 10);

            Assert.That(firstDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(secondDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(thirdDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(forthDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(fifthDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(sixthDay.EndOfWeek(), Is.EqualTo(seventhDay));
            Assert.That(seventhDay.EndOfWeek(), Is.EqualTo(seventhDay));
        }

        [Test]
        public void Can_Get_Start_Of_Week_For_Every_Day_Of_The_Week()
        {
            var firstDay = new PersianDate(1388, 7, 4);
            var secondDay = new PersianDate(1388, 7, 5);
            var thirdDay = new PersianDate(1388, 7, 6);
            var forthDay = new PersianDate(1388, 7, 7);
            var fifthDay = new PersianDate(1388, 7, 8);
            var sixthDay = new PersianDate(1388, 7, 9);
            var seventhDay = new PersianDate(1388, 7, 10);

            Assert.That(firstDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(secondDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(thirdDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(forthDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(fifthDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(sixthDay.StartOfWeek(), Is.EqualTo(firstDay));
            Assert.That(seventhDay.StartOfWeek(), Is.EqualTo(firstDay));            
        }
    }
}