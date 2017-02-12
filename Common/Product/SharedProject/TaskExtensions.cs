using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.VisualStudio.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudioTools.Infrastructure {
    static class TaskExtensions {
        /// <summary>
        /// Suppresses warnings about unawaited tasks and ensures that unhandled
        /// errors will cause the process to terminate.
        /// </summary>
        public static async void DoNotWait(this Task task) {
            await task;
        }
        
        /// <summary>
        /// Waits for a task to complete. If an exception occurs, the exception
        /// will be raised without being wrapped in a
        /// <see cref="AggregateException"/>.
        /// </summary>
        public static void WaitAndUnwrapExceptions(this Task task) {
            task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Waits for a task to complete. If an exception occurs, the exception
        /// will be raised without being wrapped in a
        /// <see cref="AggregateException"/>.
        /// </summary>
        public static T WaitAndUnwrapExceptions<T>(this Task<T> task) {
            return task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Waits for a task to complete. If current thread is UI thread then switches thread 
        /// context to <see cref="NoMessagePumpSyncContext"/>. If an exception occurs, the 
        /// exception will be raised without being wrapped in a <see cref="AggregateException"/>. 
        /// </summary>
        public static void WaitAndUnwrapExceptions(this Task task, Dispatcher dispatcher) {
            if (dispatcher.CheckAccess()) {
                using (ThreadingTools.Apply(new NoMessagePumpSyncContext(), true)) {
                    task.GetAwaiter().GetResult();
                }
            } else {
                task.GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Waits for a task to complete. If current thread is UI thread then switches thread 
        /// context to <see cref="NoMessagePumpSyncContext"/>. If an exception occurs, the 
        /// exception will be raised without being wrapped in a <see cref="AggregateException"/>. 
        /// </summary>
        public static T WaitAndUnwrapExceptions<T>(this Task<T> task, Dispatcher dispatcher) {
            if (dispatcher.CheckAccess()) {
                using (ThreadingTools.Apply(new NoMessagePumpSyncContext(), true)) {
                    return task.GetAwaiter().GetResult();
                }
            } else {
                return task.GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Silently handles the specified exception.
        /// </summary>
        public static Task SilenceException<T>(this Task task) where T : Exception {
            return task.ContinueWith(t => {
                try {
                    t.Wait();
                } catch (AggregateException ex) {
                    ex.Handle(e => e is T);
                }
            });
        }

        /// <summary>
        /// Silently handles the specified exception.
        /// </summary>
        public static Task<U> SilenceException<T, U>(this Task<U> task) where T : Exception {
            return task.ContinueWith(t => {
                try {
                    return t.Result;
                } catch (AggregateException ex) {
                    ex.Handle(e => e is T);
                    return default(U);
                }
            });
        }
    }
}