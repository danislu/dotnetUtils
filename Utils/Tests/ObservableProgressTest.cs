namespace Utils.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utils.Tap;

    [TestFixture]
    class ObservableProgressTest
    {
        [Test]
        public void ProgressTest()
        {
            var observableProgress = new ObservableProgress<int>();

            var queue = new Queue<int>();
            var observable = observableProgress.Progress;
            observable.Subscribe(onNext: arg => queue.Enqueue(arg.Value));

            for (int i = 0; i < 10; i++)
            {
                observableProgress.Report(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, queue.Dequeue());
            }
        }
    }
}
