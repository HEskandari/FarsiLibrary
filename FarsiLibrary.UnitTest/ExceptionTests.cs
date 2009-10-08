using FarsiLibrary.Utils.Exceptions;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class ExceptionTests
    {
        [Test]
        public void Can_Create_InvalidPersianDate_Exception()
        {
            var ex = new InvalidPersianDateException();
            
            Assert.Null(ex.InvalidValue);
            Assert.That(ex.Message, Is.Empty);
        }

        [Test]
        public void Can_Create_InvalidPersianDateFormat_Exception()
        {
            var ex = new InvalidPersianDateFormatException();
            Assert.That(ex.Message, Is.Empty);
        }

        [Test]
        public void Can_Create_FormatExceptionWithMessage()
        {
            var ex = new InvalidPersianDateFormatException("Invalid date format");

            Assert.That(ex.Message, Is.Not.Empty);
            Assert.AreEqual("Invalid date format", ex.Message);
        }
    }
}
