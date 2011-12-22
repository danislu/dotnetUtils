namespace Utils.Wcf
{
    using System.Collections.Generic;
    using System.Net;

    public interface IWebOperationContextOutgoingResponse
    {
        string ContentType { get; set; }
        HttpStatusCode StatusCode { get; set; }
        IDictionary<string, string> Headers { get; }
    }
}