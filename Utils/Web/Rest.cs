namespace Utils.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Rest
    {
        public IDictionary<HttpRequestHeader, string> RequestHeaders { get; set; }

        public Stream Get(Uri uri)
        {
            return GetAsync(uri).Result;
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

        public string GetString(Uri uri)
        {
            return GetStringAsync(uri).Result;
        }

        public Task<string> GetStringAsync(Uri uri)
        {
            return GetStringAsync(uri, new CancellationToken(), null);
        }

        public Task<string> GetStringAsync(Uri uri, CancellationToken token)
        {
            return GetStringAsync(uri, token, null);
        }

        public Task<string> GetStringAsync(Uri uri, CancellationToken token, IProgress<double> progress)
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

        public Task<Stream> GetAsync(Uri uri)
        {
            return GetAsync(uri, new CancellationToken(), null);
        }

        public Task<Stream> GetAsync(Uri uri, CancellationToken token)
        {
            return GetAsync(uri, token, null);
        }

        public Task<Stream> GetAsync(Uri uri, CancellationToken token, IProgress<double> progress)
        {
            return Request(uri, HttpMethods.Get, token, progress);
        }

        public Task<Stream> PostAsync(Uri uri, Stream body)
        {
            return PostAsync(uri, body, new CancellationToken(), null);
        }

        public Task<Stream> PostAsync(Uri uri, Stream body, CancellationToken token)
        {
            return PostAsync(uri, body, token, null);
        }

        public Task<Stream> PostAsync(Uri uri, Stream body, CancellationToken token, IProgress<double> progress)
        {
            return Request(uri, HttpMethods.Post, body, token, progress);
        }

        public Task<Stream> PutAsync(Uri uri, Stream body)
        {
            return PutAsync(uri, body, new CancellationToken(), null);
        }

        public Task<Stream> PutAsync(Uri uri, Stream body, CancellationToken token)
        {
            return PutAsync(uri, body, token, null);
        }

        public Task<Stream> PutAsync(Uri uri, Stream body,  CancellationToken token, IProgress<double> progress)
        {
            return Request(uri, HttpMethods.Put, body, token, progress);
        }

        public Task<Stream> DeleteAsync(Uri uri)
        {
            return DeleteAsync(uri, new CancellationToken(), null);
        }

        public Task<Stream> DeleteAsync(Uri uri, CancellationToken token)
        {
            return DeleteAsync(uri, token, null);
        }

        public Task<Stream> DeleteAsync(Uri uri, CancellationToken token, IProgress<double> progress)
        {
            return Request(uri, HttpMethods.Delete, token, progress);
        }

        protected virtual WebRequest GetWebRequest(Uri uri, string method)
        {
            var request = WebRequest.Create(uri);
            request.Method = method;
            if (RequestHeaders != null)
            {
                request.AddHeaders(RequestHeaders);
            } 
            
            return request;
        }

        private Task<Stream> Request(Uri uri, string method, CancellationToken token, IProgress<double> progress)
        {
            return Request(uri, method, null, token, progress);
        }

        private Task<Stream> Request(Uri uri, string method, Stream body, CancellationToken token, IProgress<double> progress)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (string.IsNullOrEmpty(method)) throw new ArgumentNullException("method", "can not be null or empty");

            Action<double> reportAndCheckToken = p =>
                                                     {
                                                         token.ThrowIfCancellationRequested();
                                                         if (progress != null)
                                                         {
                                                             progress.Report(p);
                                                         }
                                                     };

            var request = GetWebRequest(uri, method);
            var task = body != null
                       ? request.GetRequestStreamAsync().ContinueWith(t =>
                                               {
                                                   body.CopyTo(t.Result);
                                                   reportAndCheckToken(0.3);
                                               }).ContinueWith(_ =>
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
