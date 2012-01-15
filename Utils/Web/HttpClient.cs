namespace Utils.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class HttpClient
    {
        public IDictionary<HttpRequestHeader, string> RequestHeaders { get; set; }

        public Stream Get(Uri uri)
        {
            return GetAsync(uri).Result;
        }

        public string GetString(Uri uri)
        {
            return GetStringAsync(uri).Result;
        }

        public Stream Post(Uri uri, Stream body)
        {
            return PostAsync(uri, body).Result;
        }

        public Stream Put(Uri uri, Stream body)
        {
            return PutAsync(uri, body).Result;
        }

        public Stream Delete(Uri uri)
        {
            return DeleteAsync(uri).Result;
        }

        public Task<string> GetStringAsync(Uri uri, CancellationToken token = new CancellationToken(), IProgress<double> progress = null)
        {
            var task = GetAsync(uri, token, progress);
            return task.ContinueWith(t =>
                                         {
                                             token.ThrowIfCancellationRequested();
                                             using (var streamReader = new StreamReader(t.Result))
                                             {
                                                 return streamReader.ReadToEnd();
                                             }
                                         });
        }

        public Task<Stream> GetAsync(Uri uri, CancellationToken token = new CancellationToken(), IProgress<double> progress = null)
        {
            return DoRequestAsync(uri, HttpMethods.Get, token, progress);
        }

        public Task<Stream> PostAsync(Uri uri, Stream body, CancellationToken token = new CancellationToken(), IProgress<double> progress = null)
        {
            return DoRequestAsync(uri, HttpMethods.Post, body, token, progress);
        }

        public Task<Stream> PutAsync(Uri uri, Stream body, CancellationToken token = new CancellationToken(), IProgress<double> progress = null)
        {
            return DoRequestAsync(uri, HttpMethods.Put, body, token, progress);
        }

        public Task<Stream> DeleteAsync(Uri uri, CancellationToken token = new CancellationToken(), IProgress<double> progress = null)
        {
            return DoRequestAsync(uri, HttpMethods.Delete, token, progress);
        }

        protected virtual WebRequest GetWebRequest(Uri uri)
        {
            return WebRequest.Create(uri);
        }

        private Task<Stream> DoRequestAsync(Uri uri, string method, CancellationToken token, IProgress<double> progress)
        {
            return DoRequestAsync(uri, method, null, token, progress);
        }

        private Task<Stream> DoRequestAsync(Uri uri, string method, Stream body, CancellationToken token, IProgress<double> progress)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException("method", "can't be null or empty");
            }

            Action<double> reportAndCheckToken = p =>
            {
                token.ThrowIfCancellationRequested();
                if (progress != null)
                {
                    progress.Report(p);
                }
            };

            var request = GetWebRequest(uri);
            request.Method = method;
            if (RequestHeaders != null)
            {
                request.AddHeaders(RequestHeaders);
            }

            var task = body != null
                        ? request.GetRequestStreamAsync()
                        .ContinueWith(t =>
                        {
                            body.CopyTo(t.Result);
                            reportAndCheckToken(0.3);
                        })
                        .ContinueWith(_ =>
                        {
                            var response = request.GetResponse();
                            reportAndCheckToken(0.6);
                            return response;
                        })
                       : request.GetResponseAsync();

            return task.ContinueWith(t =>
            {
                var stream = t.Result.GetResponseStream();
                reportAndCheckToken(0.9);
                return stream;
            });
        }
    }
}
