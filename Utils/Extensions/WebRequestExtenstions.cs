namespace System.Net
{
    using System.IO;
    using System.Threading.Tasks;

    public static class WebRequestExtensions
    {
        public static Task<WebResponse> GetResponseAsync(this WebRequest webRequest)
        {
            return Task.Factory.FromAsync<WebResponse>(webRequest.BeginGetResponse, webRequest.EndGetResponse, null);
        }

        public static Task<Stream> GetRequestStreamAsync(this WebRequest webRequest)
        {
            return Task.Factory.FromAsync<Stream>(webRequest.BeginGetRequestStream, webRequest.EndGetRequestStream, null);
        }
    }
}
