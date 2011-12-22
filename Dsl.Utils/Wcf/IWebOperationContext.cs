namespace Dsl.Utils.Wcf
{
    public interface IWebOperationContext
    {
        IWebOperationContextIncomingReqest IncomingRequest { get; }
        IWebOperationContextOutgoingResponse OutgoingResponse { get; }
    }
}