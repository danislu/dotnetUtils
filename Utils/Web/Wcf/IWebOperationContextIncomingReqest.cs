namespace Utils.Web.Wcf
{
    using Utils.Classes;

    public interface IWebOperationContextIncomingReqest
    {
        string ContentType { get; }
        string Accept { get; }
        string GetQueryParameter(string key);
        ReadOnlyDictionary<string, string> Headers { get; }
    }
}