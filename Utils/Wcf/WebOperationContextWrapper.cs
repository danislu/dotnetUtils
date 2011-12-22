namespace Utils.Wcf
{
    using System;
    using System.ServiceModel.Web;

    public class WebOperationContextWrapper : IWebOperationContext
    {
        private readonly WebOperationContext context;
        private IWebOperationContextIncomingReqest incomingRequest;
        private IWebOperationContextOutgoingResponse outgoingResponse;

        public WebOperationContextWrapper(WebOperationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        public IWebOperationContextIncomingReqest IncomingRequest
        {
            get
            {
                return this.incomingRequest ??
                    (this.incomingRequest = new IncomingWebRequestContextWrapper(context.IncomingRequest));
            }
        }

        public IWebOperationContextOutgoingResponse OutgoingResponse
        {
            get
            {
                return this.outgoingResponse ??
                    (this.outgoingResponse = new OutgoingWebResponseContextWrapper(context.OutgoingResponse));
            }
        }
    }
}
