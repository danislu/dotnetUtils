using System.Collections.Generic;
using System.Net;

namespace Dsl.Utils.Wcf
{
    public interface IWebOperationContextOutgoingResponse
    {
        string ContentType { get; set; }
        HttpStatusCode StatusCode { get; set; }
        IDictionary<string, string> Headers { get; }
    }
}