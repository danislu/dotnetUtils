using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NUnit.Framework;

namespace Utils.WPF.Tests
{
    [TestFixture]
    public class OverrideCursorTests
    {
        [STAThread]
        [Test]
        public void TestSetOverrideCursor()
        {
            using(new OverrideCursor(Cursors.Cross))
            {
                Assert.AreEqual(Cursors.Cross, Mouse.OverrideCursor);
            }
            Assert.IsNull(Mouse.OverrideCursor);
        }

        [STAThread]
        [Test]
        public void TestChangeOverrideCursor()
        {
            Mouse.OverrideCursor = Cursors.Hand;
            using (new OverrideCursor(Cursors.Cross))
            {
                Assert.AreEqual(Cursors.Cross, Mouse.OverrideCursor);
            }
            Assert.AreEqual(Cursors.Hand, Mouse.OverrideCursor);
        }
    }
}
