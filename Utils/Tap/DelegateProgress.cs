namespace Utils.Tap
{
    using System;
    using System.Threading;

    public class DelegateProgress<T> : IProgress<T>
    {
        private readonly Action<T> onReport;

        public DelegateProgress(Action<T> onReport)
        {
            if (onReport == null)
            {
                throw new ArgumentNullException("onReport");
            }
            this.onReport = onReport;
        }

        public void Report(T value)
        {
            this.onReport(value);
        }
    }
}
