namespace System.Threading.Tasks
{
    using System;

    public static class TaskExtensions
    {
        public static Task ContinueWithOnCurrentSynchronizationContext(this Task task, Action<Task> action)
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return task.ContinueWith(action, scheduler);
        }

        public static Task ContinueWithOnCurrentSynchronizationContext<T>(this Task<T> task, Action<Task<T>> action)
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return task.ContinueWith(action, scheduler);
        }
    }
}
