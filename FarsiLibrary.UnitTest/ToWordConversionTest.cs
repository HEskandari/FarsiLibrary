using System;
using FarsiLibrary.Utils;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class ToWordConversionTest
    {
        [Test]
        public void Should_Convert_Maximum_Integer_Values()
        {
            ToWords.ToString(Int32.MaxValue);
        }
        
        [Test]
        public void Should_Convert_Big_Integer_Values()
        {
            string s = ToWords.ToString(int.MaxValue);
            Assert.IsNotNullOrEmpty(s);
        }

        [Test]
        public void Should_Not_Be_Able_To_Convert_Larger_Than_Long_Values()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ToWords.ToString(long.MaxValue));
        }

        [Test]
        public void Should_Not_Be_Able_To_Convert_Minus_Values()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ToWords.ToString(-1000));
        }

        [Test]
        public void Can_Convert_Singles()
        {
            string s = ToWords.ToString(8);
            Assert.NotNull(s);
        }

        [Test]
        public void Can_Convert_Tens()
        {
            Assert.AreEqual("سی و يک", ToWords.ToString(31));
            Assert.AreEqual("چهل و دو", ToWords.ToString(42));
            Assert.AreEqual("پنجاه و سه", ToWords.ToString(53));
            Assert.AreEqual("شصت و چهار", ToWords.ToString(64));
            Assert.AreEqual("هفتاد و پنج", ToWords.ToString(75));
            Assert.AreEqual("هشتاد و شش", ToWords.ToString(86));
            Assert.AreEqual("نود و هفت", ToWords.ToString(97));
        }

        [Test]
        public void Can_Convert_Hundreds()
        {
            Assert.AreEqual("صد و ده", ToWords.ToString(110));
            Assert.AreEqual("دویست و بيست و يک", ToWords.ToString(221));
            Assert.AreEqual("سیصد و سی و دو", ToWords.ToString(332));
            Assert.AreEqual("چهارصد و چهل و سه", ToWords.ToString(443));
            Assert.AreEqual("پانصد و پنجاه و چهار", ToWords.ToString(554));
            Assert.AreEqual("ششصد و شصت و پنج", ToWords.ToString(665));
            Assert.AreEqual("هفتصد و هفتاد و شش", ToWords.ToString(776));
            Assert.AreEqual("هشتصد و هشتاد و هفت", ToWords.ToString(887));
            Assert.AreEqual("نهصد و نود و هشت", ToWords.ToString(998));
        }

        [Test]
        public void Can_Convert_Round_Numeric_Values()
        {
            Assert.AreEqual("يک میلیارد", ToWords.ToString(1000000000));
            Assert.AreEqual("يک میلیون", ToWords.ToString(1000000));
            Assert.AreEqual("يک هزار", ToWords.ToString(1000));
            Assert.AreEqual("صد", ToWords.ToString(100));
        }

        [Test]
        public void Can_Convert_Thousands()
        {
            string s = ToWords.ToString(1590);
            Assert.NotNull(s);
        }

        [Test]
        public void Can_Convert_Ten_Thousands()
        {
            string s = ToWords.ToString(18910);
            Assert.NotNull(s);
        }

        [Test]
        public void Can_Convert_Hundred_Thousands()
        {
            string s = ToWords.ToString(547230);
            Assert.NotNull(s);
        }

        [Test]
        public void Can_Convert_Numeric_Characters_To_English()
        {
            var persianNumerals = "۰۱۲۳۴۵۶۷۸۹";
            var englishNumerals = toEnglish.Convert(persianNumerals);

            Assert.AreEqual("0123456789", englishNumerals);
        }

        [Test]
        public void Can_Convert_Numeric_Characters_To_Persian()
        {
            var englishNumerals = "0123456789";
            var persianNumerals = toFarsi.Convert(englishNumerals);

            Assert.AreEqual("۰۱۲۳۴۵۶۷۸۹", persianNumerals);
        }

        [Test]
        public void Can_Convert_Mixed_Strings_To_English_Numerals()
        {
            var persianNumerals = "۱۲۳ABC";
            var result = toEnglish.Convert(persianNumerals);

            Assert.AreEqual("123ABC", result);
        }

        [Test]
        public void Can_Convert_Mixed_Strings_To_Persian_Numerals()
        {
            var persianNumerals = "123ABC";
            var result = toFarsi.Convert(persianNumerals);

            Assert.AreEqual("۱۲۳ABC", result);
        }
    }
}