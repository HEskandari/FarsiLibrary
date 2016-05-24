using System;
using System.Globalization;
using System.Windows.Forms;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Utils;
using NUnit.Framework;
using PersianCalendar=FarsiLibrary.Utils.PersianCalendar;
using System.Linq;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianCultureInfoTests
    {
        [Test]
        public void Can_Create_CultureInfo()
        {
            var ci = new PersianCultureInfo();
            Assert.NotNull(ci);
        }

        [Test]
        public void Can_Set_Thread_Culture()
        {
            PersianCultureInfo ci;

            using (new CultureSwitchContext(ci = new PersianCultureInfo()))
            {
                Assert.AreEqual(ci, System.Threading.Thread.CurrentThread.CurrentUICulture);
                Assert.AreEqual(ci, System.Threading.Thread.CurrentThread.CurrentCulture);
            }
        }

        [Test]
        public void Creating_PersianCultureInfo_Will_Set_Correct_Calendar()
        {
            PersianCultureInfo ci = new PersianCultureInfo();

            Assert.That(ci.Calendar, Is.Not.TypeOf(typeof(PersianCalendar)));
            Assert.That(ci.IsReadOnly, Is.True);
        }

        [Test]
        public void Setting_ThreadCulture_To_PersianCulture_Will_Set_Correct_Calendar()
        {
            using (new CultureSwitchContext(new PersianCultureInfo()))
            {
                Assert.That(System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar, Is.Not.TypeOf(typeof (FarsiLibrary.Utils.PersianCalendar)));
                Assert.That(System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar, Is.TypeOf(typeof (System.Globalization.PersianCalendar)));
            }
        }

        [Test]
        public void Can_Clone_CultureInfo()
        {
            var original = new PersianCultureInfo();
            var clone = original.Clone();

            Assert.AreNotSame(original, clone);
            Assert.AreEqual(original, clone);
        }

        [Test]
        public void Optional_Calendar_Contains_CorrectCalendars()
        {
            var ci = new PersianCultureInfo();
            var localCalendar = ci.OptionalCalendars.OfType<FarsiLibrary.Utils.PersianCalendar>().FirstOrDefault();
            var frameworkCalendar = ci.OptionalCalendars.OfType<System.Globalization.PersianCalendar>().FirstOrDefault();

            Assert.NotNull(frameworkCalendar);
            Assert.NotNull(localCalendar);
        }

        [Test]
        public void Setting_DateFormat_To_Null_Throws()
        {
            var ci = new PersianCultureInfo();

            Assert.Throws<ArgumentNullException>(() => ci.DateTimeFormat = null);
        }

        [Test]
        public void Can_Set_DateTimeFormat_To_Other_Instances()
        {
            var ci = new PersianCultureInfo();
            Assert.DoesNotThrow(() => ci.DateTimeFormat = DateTimeFormatWrapper.GetFormatInfo());
        }

        [Test]
        public void Can_Convert_To_String_With_InvariantCulture()
        {
            using (new CultureSwitchContext(new PersianCultureInfo()))
            {
                DateTime dt = DateTime.MinValue;
                Assert.DoesNotThrow(() => dt.ToString(CultureInfo.InvariantCulture));
            }
        }

        [Test]
        public void Converts_To_Correct_DayOfWeek()
        {
            PersianDate pd = new DateTime(2008, 10, 17);
            DateTime dt = new DateTime(2008, 10, 18, 0, 0, 0, new System.Globalization.PersianCalendar());

            Assert.AreEqual(DayOfWeek.Friday, pd.DayOfWeek);
            Assert.AreEqual(DayOfWeek.Friday, dt.DayOfWeek);
        }

        [Test]
        public void Converts_To_Correct_DayOfWeek_String()
        {
            var ci = new CultureInfo("fa-ir");
            var cip = new PersianCultureInfo();

            using (new CultureSwitchContext(cip))
            {
                DateTime dt1 = new DateTime(2008, 10, 17);
                DateTime dt2 = new DateTime(1387, 7, 26, 0, 0, 0, new System.Globalization.PersianCalendar());

                Assert.AreEqual(dt1.ToString("dddd"), dt1.ToString("dddd"));
                Assert.AreEqual(dt1.ToString("dddd", ci), dt2.ToString("dddd", ci));
                Assert.AreEqual(dt1.ToString("dddd", cip), dt2.ToString("dddd", cip));
            }
        }

        [Test]
        public void Should_Return_Right_DayOfWeek_Translation()
        {
            var cip = new PersianCultureInfo();

            using (new CultureSwitchContext(cip))
            {
                var friday = cip.DateTimeFormat.GetDayName(DayOfWeek.Friday);
                Assert.AreEqual("جمعه", friday);
            }
        }

        [Test]
        public void Seven_DaysOfWeek_Index_Value_Validity_In_PersianCalendar()
        {
            var cip = new PersianCultureInfo();

            using (new CultureSwitchContext(cip))
            {
                var pd1 = new DateTime(1387, 8, 2, cip.Calendar);
                Assert.AreEqual(pd1.DayOfWeek, DayOfWeek.Thursday);

                var pd2 = new DateTime(1387, 8, 3, cip.Calendar);
                Assert.AreEqual(pd2.DayOfWeek, DayOfWeek.Friday);

                var pd3 = new DateTime(1387, 8, 4, cip.Calendar);
                Assert.AreEqual(pd3.DayOfWeek, DayOfWeek.Saturday);

                var pd4 = new DateTime(1387, 8, 5, cip.Calendar);
                Assert.AreEqual(pd4.DayOfWeek, DayOfWeek.Sunday);

                var pd5 = new DateTime(1387, 8, 6, cip.Calendar);
                Assert.AreEqual(pd5.DayOfWeek, DayOfWeek.Monday);

                var pd6 = new DateTime(1387, 8, 7, cip.Calendar);
                Assert.AreEqual(pd6.DayOfWeek, DayOfWeek.Tuesday);

                var pd7 = new DateTime(1387, 8, 8, cip.Calendar);
                Assert.AreEqual(pd7.DayOfWeek, DayOfWeek.Wednesday);
            }
        }

        [Test]
        public void Can_Create_Readonly_Copy()
        {
            var cip = new PersianCultureInfo();

            Assert.DoesNotThrow(() => CultureInfo.ReadOnly(cip), "Should be able to create readonly copy");
        }

        [Test]
        public void MaskTextBox_Problem_With_Persian_CultureInfo()
        {
            var cip = new PersianCultureInfo();

            using (new CultureSwitchContext(cip))
            {
                Assert.DoesNotThrow(() => { var tb = new MaskedTextBox(); }, "Should be able to create a MaskedTextBox control");
            }
        }

        [Test]
        public void Setting_Culture_To_PersianCultureInfo_Will_Set_DateTimeFormat()
        {
            var cip = new PersianCultureInfo();
            var format = cip.CreateDateTimeFormatInfo();

            using(new CultureSwitchContext(cip))
            {
                Assert.That(cip.DateTimeFormat, Is.Not.Null);
                Assert.AreEqual(format.AbbreviatedDayNames, cip.DateTimeFormat.AbbreviatedDayNames);
                Assert.AreEqual(format.AbbreviatedMonthGenitiveNames, cip.DateTimeFormat.AbbreviatedMonthGenitiveNames);
                Assert.AreEqual(format.AbbreviatedMonthNames, cip.DateTimeFormat.AbbreviatedMonthNames);
                Assert.AreEqual(format.AMDesignator, cip.DateTimeFormat.AMDesignator);
                Assert.AreEqual(format.PMDesignator, cip.DateTimeFormat.PMDesignator);
                Assert.AreEqual(format.Calendar, cip.DateTimeFormat.Calendar);
                Assert.AreEqual(format.DayNames, cip.DateTimeFormat.DayNames);
                Assert.AreEqual(format.DateSeparator, cip.DateTimeFormat.DateSeparator);
                Assert.AreEqual(format.ShortDatePattern, cip.DateTimeFormat.ShortDatePattern);
                Assert.AreEqual(format.ShortestDayNames, cip.DateTimeFormat.ShortestDayNames);
                Assert.AreEqual(format.ShortTimePattern, cip.DateTimeFormat.ShortTimePattern);
                Assert.AreEqual(format.YearMonthPattern, cip.DateTimeFormat.YearMonthPattern);
                Assert.AreEqual(format.TimeSeparator, cip.DateTimeFormat.TimeSeparator);
            }
        }
    }
}