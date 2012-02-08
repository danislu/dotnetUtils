namespace Utils.Tests
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Utils.Web;

    [TestFixture]
    public class RestTest
    {
        private TRest target;
        private Uri uri;

        [SetUp]
        public void SetUp()
        {
            this.target = new TRest();
            this.uri = new Uri(@"http://dummy.com/item/1");
        }

        [Test]
        public void DeleteAsyncTest()
        {
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.DeleteAsync(uri);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAsyncTest()
        {
            var actual = target.GetAsync(uri);
            
            Assert.AreEqual(uri, target.TestUri);
            Assert.AreEqual(HttpMethods.Get, target.TestMethod);

            using (var sr = new StreamReader(actual.Result))
            {
                Assert.AreEqual("the response", sr.ReadToEnd());
            } 
        }

        [Test]
        public void GetStringAsyncTest()
        {
            var token = new CancellationToken(); // TODO: Initialize to an appropriate value
            Task<string> expected = null; // TODO: Initialize to an appropriate value
            Task<string> actual;
            actual = target.GetStringAsync(uri, token);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PostAsyncTest()
        {
            Stream body = null; // TODO: Initialize to an appropriate value
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.PostAsync(uri, body);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PutAsyncTest()
        {
            Stream body = null; // TODO: Initialize to an appropriate value
            Task<Stream> expected = null; // TODO: Initialize to an appropriate value
            Task<Stream> actual;
            actual = target.PutAsync(uri, body);
            Assert.AreEqual(expected, actual);
        }
    }

    internal class TRest : HttpClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            this.TestUri = uri;
            return new MWebRequest((method) => this.TestMethod = method);
        }

        public Uri TestUri { get; private set; }

        public string TestMethod { get; private set; }

        private class MWebRequest : WebRequest
        {
            private readonly Action<string> methodSet;
            private string method;

            public MWebRequest(Action<string> methodSet)
            {
                this.methodSet = methodSet;
            }

            private class MWebResponse : WebResponse
            {
                public override Stream GetResponseStream()
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("the response"));
                }
            }

            private class MAsyncResult : IAsyncResult
            {
                private object obj;
                private AutoResetEvent autoResetEvent;

                public bool IsCompleted
                {
                    get { return true; }
                }

                public WaitHandle AsyncWaitHandle
                {
                    get { return autoResetEvent ?? (autoResetEvent = new AutoResetEvent(false)); }
                }

                public object AsyncState
                {
                    get { return this.obj ?? (this.obj = new object()); }
                }

                public bool CompletedSynchronously
                {
                    get { return true; }
                }
            }

            public override string Method
            {
                get { return method; }
                set
                {
                    method = value;
                    this.methodSet(method);
                }
            }

            public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
            {
                return new MAsyncResult();
            }

            public override Stream EndGetRequestStream(IAsyncResult asyncResult)
            {
                return new MemoryStream();
            }

            public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
            {
                return new MAsyncResult();
            }

            public override WebResponse EndGetResponse(IAsyncResult asyncResult)
            {
                return new MWebResponse();
            }
        }
    }
}
