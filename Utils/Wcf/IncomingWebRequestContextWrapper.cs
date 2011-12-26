namespace Utils.Wcf
{
    using System;
    using System.Linq;
    using System.ServiceModel.Web;

    public class IncomingWebRequestContextWrapper : IWebOperationContextIncomingReqest
    {
        private readonly IncomingWebRequestContext incomingRequest;
        private ReadOnlyDictionary<string, string> headers;

        public IncomingWebRequestContextWrapper(IncomingWebRequestContext incomingRequest)
        {
            if (incomingRequest == null)
            {
                throw new ArgumentNullException("incomingRequest");
            }

            this.incomingRequest = incomingRequest;
        }

        public string ContentType
        {
            get { return this.incomingRequest.ContentType; }
        }

        public string Accept
        {
            get { return this.incomingRequest.Accept ?? string.Empty; }
        }

        public string GetQueryParameter(string key)
        {
            return this.incomingRequest.UriTemplateMatch.QueryParameters.Get(key);
        }

        public ReadOnlyDictionary<string, string> Headers
        {
            get
            {
                return this.headers ??
                       (this.headers = new ReadOnlyDictionary<string, string>(
                                           this.incomingRequest.Headers.AllKeys.ToDictionary(key => key, key => this.incomingRequest.Headers.Get((string) key))));
            }
        }
    }
}