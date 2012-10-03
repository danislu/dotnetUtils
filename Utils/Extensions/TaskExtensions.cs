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

        public static Task ContinueWithErrorHandling<T>(this Task<T> task, Action<Task<T>> continuationAction, ITaskErrorLogger logger)
        {
            if (task.IsFaulted)
            {
                logger.LogTaskError(task);
            }
            return task.ContinueWith(continuationAction);
        }

        public static Task<TResult> ContinueWithErrorHandling<TResult, TInput>(this Task<TInput> task, Func<Task<TInput>, TResult> continuationFunction, ITaskErrorLogger logger)
        {
            if (task.IsFaulted)
            {
                logger.LogTaskError(task);
            }
            return task.ContinueWith(continuationFunction);
        }
    }

    public interface ITaskErrorLogger
    {
        void LogTaskError(Task faultedTask);
        void LogTaskError<T>(Task<T> faultedTask);
    }
}
