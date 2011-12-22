namespace Dsl.Utils.Wcf
{
    public interface IWebOperationContextIncomingReqest
    {
        string ContentType { get; }
        string Accept { get; }
        ReadOnlyDictionary<string, string> Headers { get; }
    }
}