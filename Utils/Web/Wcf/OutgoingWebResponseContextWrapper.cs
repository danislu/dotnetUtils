namespace Utils.Web.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Web;

    public class OutgoingWebResponseContextWrapper : IWebOperationContextOutgoingResponse
    {
        private readonly OutgoingWebResponseContext outgoingResponse;
        private IDictionary<string, string> headers;

        public OutgoingWebResponseContextWrapper(OutgoingWebResponseContext outgoingResponse)
        {
            if (outgoingResponse == null)
            {
                throw new ArgumentNullException("outgoingResponse");
            }

            this.outgoingResponse = outgoingResponse;
        }


        public string ContentType
        {
            get { return this.outgoingResponse.ContentType; }
            set { this.outgoingResponse.ContentType = value; }
        }

        public HttpStatusCode StatusCode
        {
            get { return this.outgoingResponse.StatusCode; }
            set { this.outgoingResponse.StatusCode = value; }
        }

        public IDictionary<string, string> Headers
        {
            get
            {
                return this.headers ??
                       (this.headers = this.outgoingResponse.Headers.AllKeys.ToDictionary(key => key, key => this.outgoingResponse.Headers.Get((string)key)));
            }
        }
    }
}