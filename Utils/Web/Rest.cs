namespace Utils.Web
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Rest
    {
        #region Get Methods

        public string GetString(Uri uri)
        {
            var task = GetStringAsync(uri);
            task.Wait();
            return task.Result;
        }

        public Task<string> GetStringAsync(Uri uri)
        {
            return GetStringAsync(uri, new CancellationToken(), null);
        }

        public Task<string> GetStringAsync(Uri uri, CancellationToken token)
        {
            return GetStringAsync(uri, token, null);
        }

        public Task<string> GetStringAsync(Uri uri, CancellationToken token, IProgress<int> progress)
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

        public Stream Get(Uri uri)
        {
            var task = GetAsync(uri);
            task.Wait();
            return task.Result;
        }

        public Task<Stream> GetAsync(Uri uri)
        {
            return GetAsync(uri, new CancellationToken(), null);
        }

        public Task<Stream> GetAsync(Uri uri, CancellationToken token)
        {
            return GetAsync(uri, token, null);
        }

        public Task<Stream> GetAsync(Uri uri, CancellationToken token, IProgress<int> progress)
        {
            var request = GetWebRequest(uri, HttpMethods.Get);
            return request.GetResponseAsync().ContinueWith(t =>
                                                               {
                                                                   token.ThrowIfCancellationRequested();
                                                                   return t.Result.GetResponseStream();
                                                               });
        }

        #endregion

        #region Post Methods

        public Task<Stream> PostAsync(Uri uri, Stream body, CancellationToken token, IProgress<int> progress)
        {
            var request = GetWebRequest(uri, HttpMethods.Post);
            return request.GetRequestStreamAsync()
                .ContinueWith(t => body.CopyTo(t.Result))
                .ContinueWith(_ => request.GetResponse())
                .ContinueWith(t => t.Result.GetResponseStream());
        }

        #endregion

        private static WebRequest GetWebRequest(Uri uri, string method)
        {
            var request = WebRequest.Create(uri);
            request.Method = method;
            return request;
        }
    }
}
