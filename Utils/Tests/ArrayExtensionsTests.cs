namespace Utils.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ArrayExtensionsTests
    {
        [Test]
        public void SubArray()
        {
            var a1 = new[] { 1, 2, 3, 4 };
            var a2 = a1.SubArray(1, 2);
            CollectionAssert.AreEqual(new[] { 2, 3 }, a2);
        }

        [Test]
        public void AppendTo()
        {
            var a1 = new[] { 3, 4 };
            var a2 = new[] { 1, 2 };
            a1.AppendTo(ref a2);

            Assert.AreEqual(4, a2.Length);
            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4 }, a2);
        }

        [Test]
        public void ClearTest()
        {
            var array = new[] { 1, 2, 3, 4 };
            array.Clear();

            CollectionAssert.AreEquivalent(new[] { 0, 0, 0, 0 }, array);
        }

        [Test]
        public void ConcatTest()
        {
            var a1 = new[] { 1, 2 };
            var a2 = new[] { 3, 4 };

            var actual = a1.Concat(a2);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4 }, actual);
        }
    }
}
