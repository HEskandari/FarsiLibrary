using System.Collections.Generic;
using NUnit.Framework;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class CollectionExtensionTests
    {
        [Test]
        public void Can_Use_ForEach_On_Dictionaries()
        {
            var dictionary = new Dictionary<string, string>();
            var i = 0;

            dictionary.Add("John", "Doe");
            dictionary.ForEach(o => i++);

            Assert.AreEqual(1, i);
        }

        [Test]
        public void Using_ForEach_On_Null_Will_Not_Throw()
        {
            IDictionary<object, object> dictionary = null;
            var i = 0;

            Assert.DoesNotThrow(() => dictionary.ForEach(o => i++));
            Assert.AreEqual(0, i);
        }
    }
}