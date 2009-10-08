using System;
using NUnit.Framework;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class HelpersTest
    {
        [Test]
        public void Can_Guard_Against_Wrong_Conditions()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => Guard.Against(true, "GuardOperation"));
            Assert.That(ex, Is.TypeOf(typeof (InvalidOperationException)));
            Assert.That(ex.Message.Contains("GuardOperation"));
        }

        [Test]
        public void Can_Guard_Against_Wrong_Conditions_With_Specific_Exceptions()
        {
            Assert.Throws<NullReferenceException>(() => Guard.Against<NullReferenceException>(true, "NULL"));
        }
    }
}
