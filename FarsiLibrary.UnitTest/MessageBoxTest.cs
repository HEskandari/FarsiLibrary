using System;
using FarsiLibrary.Win.Controls;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class MessageBoxTest
    {
        [Test]
        public void Should_Specify_MessageBox_Name_When_Creating_One()
        {
            Assert.Throws(typeof(InvalidOperationException), () => FAMessageBoxManager.CreateMessageBox(string.Empty));
        }

        [Test]
        public void Creating_MessageBox_With_Duplicate_Key_Should_Return_The_Same_One()
        {
            var msg1 = FAMessageBoxManager.CreateMessageBox("Test");
            var msg2 = FAMessageBoxManager.CreateMessageBox("Test");

            Assert.AreEqual(msg1, msg2);
            Assert.AreSame(msg1, msg2);
        }

        [Test]
        public void Creating_MessageBox_With_Duplicate_Key_Should_Return_Another_One_When_Disposed()
        {
            var msg = FAMessageBoxManager.CreateMessageBox("Test");
            ((IDisposable)msg).Dispose();
            Assert.True(msg.IsDisposed);

            msg = FAMessageBoxManager.CreateMessageBox("Test");
            Assert.False(msg.IsDisposed);
        }

        [Test]
        public void Get_Message_Box_Should_Return_Null_When_Key_Not_Found()
        {
            var msg = FAMessageBoxManager.CreateMessageBox("Test");
            FAMessageBoxManager.DeleteMessageBox("Test");

            var msg2 = FAMessageBoxManager.GetMessageBox("Test");
            Assert.Null(msg2);
        }

        [Test]
        public void Delete_Message_Box_By_Providing_Null_As_Name_Throws()
        {
            Assert.Throws(typeof(InvalidOperationException), () => FAMessageBoxManager.DeleteMessageBox(null));
        }

        [Test]
        public void Delete_NonExisting_MessageBox()
        {
            var msgboxName = Guid.NewGuid().ToString();
            var result = FAMessageBoxManager.DeleteMessageBox(msgboxName);
            Assert.False(result);

            FAMessageBoxManager.CreateMessageBox(msgboxName);
            result = FAMessageBoxManager.DeleteMessageBox(msgboxName);
            Assert.True(result);
        }
    }
}