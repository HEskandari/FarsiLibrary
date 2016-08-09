using System;
using System.Globalization;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using NUnit.Framework;
using PersianCalendar=FarsiLibrary.Utils.PersianCalendar;
using Guard = FarsiLibrary.Utils.Internals.Guard;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class InternalTests
    {
        #region CultureHelper

        [Test]
        public void Can_Get_IndexOfDay_For_PersianCalendar_Using_CultureHelper()
        {
            var pc = new PersianCalendar();
            var dt = new DateTime(2009, 5, 11); //Should be Monday
            var dow = CultureHelper.GetDayOfWeek(dt, pc);

            Assert.AreEqual(2, dow);
        }

        [Test]
        public void Can_Get_IndexOfDay_For_HijriCalendar_Using_CultureHelper()
        {
            var hc = new HijriCalendar();
            var dt = new DateTime(2009, 5, 11); //Should be Monday
            var dow = CultureHelper.GetDayOfWeek(dt, hc);

            Assert.AreEqual(1, dow);
        }

        [Test]
        public void Can_Get_IndexOfDay_For_Other_Calendars_Using_CultureHelper()
        {
            var calendar = new GregorianCalendar();
            var dt = new DateTime(2009, 5, 11); //Should be Monday
            var dow = CultureHelper.GetDayOfWeek(dt, calendar);

            Assert.AreEqual(1, dow);
        }

        [Test]
        public void Can_Get_Correct_Current_Calendar()
        {
            using(new CultureSwitchContext(new PersianCultureInfo()))
            {
                var calendar = CultureHelper.CurrentCalendar;
                Assert.IsInstanceOf<PersianCalendar>(calendar);
            }

            using(new CultureSwitchContext(new CultureInfo("ar-sa")))
            {
                var calendar = CultureHelper.CurrentCalendar;
                Assert.IsInstanceOf<HijriCalendar>(calendar);                
            }

            using (new CultureSwitchContext(new CultureInfo("en-us")))
            {
                var calendar = CultureHelper.CurrentCalendar;
                Assert.IsInstanceOf<GregorianCalendar>(calendar);
            }
        }

        [Test]
        public void Can_Get_Correct_DayOfWeek_Using_CultureHelper()
        {
            using(new CultureSwitchContext(new PersianCultureInfo()))
            {
                var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); //It is a zero based index
                Assert.That(dow, Is.EqualTo(DayOfWeek.Monday));
            }

            using(new CultureSwitchContext(new CultureInfo("en-us")))
            {
                var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); //It is a zero based index
                Assert.That(dow, Is.EqualTo(DayOfWeek.Tuesday));                
            }
        }

        [Test]
        public void Max_Min_Supported_Value_Equals_PersianDate_Max_Min_Values()
        {
            using(new CultureSwitchContext(new CultureInfo("fa-ir")))
            {
                var min = CultureHelper.MinCultureDateTime;
                var max = CultureHelper.MaxCultureDateTime;

                Assert.That(min, Is.EqualTo(PersianDate.MinValue));
                Assert.That(max, Is.EqualTo(PersianDate.MaxValue));
            }
        }

        #endregion

        #region Guard

        [Test]
        public void Can_Guard_Against_Wrong_Conditions()
        {
            Assert.Throws<InvalidOperationException>(() => Guard.Against(true, "Value Wrong"));
        }

        [Test]
        public void Can_Guard_With_Specific_Exception()
        {
            Assert.Throws<OutOfMemoryException>(() => Guard.Against<OutOfMemoryException>(true, "Out Of memory"));
        }

        [Test]
        public void Will_Not_Throw_If_Condition_Is_Not_Met()
        {
            Assert.DoesNotThrow(() => Guard.Against(false, string.Empty));
            Assert.DoesNotThrow(() => Guard.Against<Exception>(false, string.Empty));
        }

        #endregion

        #region ReflectionHelper

        [Test]
        public void Can_Get_Field_Value()
        {
            var test = new ReflectionTestClass();
            var value = ReflectionHelper.GetField<string>(test, "TestField");

            Assert.AreEqual("TestValue", value);
        }

        [Test]
        public void Can_Set_Field_Value()
        {
            var test = new ReflectionTestClass();
            ReflectionHelper.SetField(test, "TestField" ,"NewValue");
            var value = ReflectionHelper.GetField<string>(test, "TestField");

            Assert.AreEqual("NewValue", value);
        }

        [Test]
        public void Getting_Field_Value_With_Null_Owner_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetField((object)null, "TestField"));
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetField(new ReflectionTestClass(), "NonExistingField"));
        }

        [Test]
        public void Getting_Property_Value_With_Null_Owner_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetProperty(null, "TestField"));
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetProperty(new ReflectionTestClass(), "NonExistingField"));
        }

        [Test]
        public void Setting_Field_Value_With_Null_Owner_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.SetField((object)null, "TestField", "TestValue"));
        }

        [Test]
        public void Can_Get_Property_Value()
        {
            var test = new ReflectionTestClass();
            var value = ReflectionHelper.GetProperty<string>(test, "TestProperty");

            Assert.AreEqual("TestValue", value);
        }

        [Test]
        public void Can_Invoke_Method()
        {
            var test = new ReflectionTestClass();
            ReflectionHelper.InvokeMethod(test, "Method");

            Assert.That(test.MethodInvoked);
        }

        [Test]
        public void Can_Invoke_StaticMethod()
        {
            ReflectionHelper.InvokeStaticMethod(typeof(ReflectionTestClass), "StaticMethod");

            Assert.That(ReflectionTestClass.StaticMethodInvoked);
        }

        #endregion

        #region TestClass

        public class ReflectionTestClass
        {
            public ReflectionTestClass()
            {
                TestField = TestProperty = "TestValue";
            }

            private string TestField;

            public string TestProperty
            {
                get; set;
            }

            public static bool StaticMethodInvoked = false;
            public bool MethodInvoked = false;

            private void Method()
            {
                MethodInvoked = true;
            }

            private static void StaticMethod()
            {
                StaticMethodInvoked = true;
            }
        }

        #endregion
    }
}