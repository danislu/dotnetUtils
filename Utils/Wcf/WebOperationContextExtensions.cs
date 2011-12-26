namespace Utils.Wcf
{
    public static class WebOperationContextExtensions
    {
        public static string GetQueryParam(this IWebOperationContext context, string key)
        {
            return context.IncomingRequest.GetQueryParameter(key);
        }
    }
}