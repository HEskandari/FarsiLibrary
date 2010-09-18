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

        [TestCase("سی و يک", 31)]
        [TestCase("چهل و دو", 42)]
        [TestCase("پنجاه و سه", 53)]
        [TestCase("شصت و چهار", 64)]
        [TestCase("هفتاد و پنج", 75)]
        [TestCase("هشتاد و شش", 86)]
        [TestCase("نود و هفت", 97)]
        public void Can_Convert_Tens(string converted, int toConvert)
        {
            Assert.AreEqual(converted, ToWords.ToString(toConvert));
        }

        [TestCase("صد و ده", 110)]
        [TestCase("دویست و بيست و يک", 221)]
        [TestCase("سیصد و سی و دو", 332)]
        [TestCase("چهارصد و چهل و سه", 443)]
        [TestCase("پانصد و پنجاه و چهار", 554)]
        [TestCase("ششصد و شصت و پنج", 665)]
        [TestCase("هفتصد و هفتاد و شش", 776)]
        [TestCase("هشتصد و هشتاد و هفت", 887)]
        [TestCase("نهصد و نود و هشت", 998)]
        public void Can_Convert_Hundreds(string converted, int toConvert)
        {
            Assert.AreEqual(converted, ToWords.ToString(toConvert));
        }

        [TestCase("يک میلیارد", 1000000000)]
        [TestCase("يک میلیون", 1000000)]
        [TestCase("يک هزار", 1000)]
        [TestCase("صد", 100)]
        [TestCase("صفر", 0)]
        public void Can_Convert_Round_Numeric_Values(string converted, int toConvert)
        {
            Assert.AreEqual(converted, ToWords.ToString(toConvert));
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