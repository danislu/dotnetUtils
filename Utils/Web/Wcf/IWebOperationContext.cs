namespace Utils.Web.Wcf
{
    public interface IWebOperationContext
    {
        IWebOperationContextIncomingReqest IncomingRequest { get; }
        IWebOperationContextOutgoingResponse OutgoingResponse { get; }

        string GetQueryParam(string key);
    }
}