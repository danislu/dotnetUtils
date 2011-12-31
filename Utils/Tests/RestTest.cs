namespace Utils.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Utils.Web;

    [TestFixture]
    public class RestTest
    {
        private Rest target;

        [SetUp]
        public void SetUp()
        {
            this.target = new Rest();
        }

        [Test]
        public void DeleteAsyncTest()
        {
            Uri uri = new Uri(@"http:\\dummy\item\1");
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.DeleteAsync(uri);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAsyncTest()
        {
            Uri uri = null; // TODO: Initialize to an appropriate value
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.GetAsync(uri);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void GetStringAsyncTest()
        {
            Uri uri = null; // TODO: Initialize to an appropriate value
            CancellationToken token = new CancellationToken(); // TODO: Initialize to an appropriate value
            Task<string> expected = null; // TODO: Initialize to an appropriate value
            Task<string> actual;
            actual = target.GetStringAsync(uri, token);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void PostAsyncTest()
        {
            Uri uri = null; // TODO: Initialize to an appropriate value
            Stream body = null; // TODO: Initialize to an appropriate value
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.PostAsync(uri, body);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void PutAsyncTest()
        {
            Uri uri = null; // TODO: Initialize to an appropriate value
            Stream body = null; // TODO: Initialize to an appropriate value
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.PutAsync(uri, body);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
