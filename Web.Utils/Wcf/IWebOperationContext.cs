namespace Dsl.Web.Wcf
{
    public interface IWebOperationContext
    {
        IWebOperationContextIncomingReqest IncomingReqest { get; }
        IWebOperationContextOutgoingResponse OutgoingResponse { get; }
    }
}