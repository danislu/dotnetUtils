using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Tap
{
    using System.Reactive.Linq;
    using System.Threading;

    public class ObservableProgress<T> : IProgress<T>
    {
        public static event EventHandler<ProgressReportedEventArgs<T>> ProgressReported;

        public IObservable<ProgressReportedEventArgs<T>> Progress
        {
            get { return null; }
        }

        public ObservableProgress()
        {
            //var a = Observable.FromEvent(
        }
        
        public void Report(T value)
        {
            ProgressReported(this, new ProgressReportedEventArgs<T>(value));
        }
    }
}
