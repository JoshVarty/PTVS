using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Infrastructure {
    public static class TaskExtensions {
        /// <summary>
        /// Suppresses warnings about unawaited tasks and ensures that unhandled
        /// errors will cause the process to terminate.
        /// </summary>
        public static async void DoNotWait(this Task task) {
            await task;
        }

        public static T WaitOrDefault<T>(this Task<T> task, int milliseconds) {
            if (task.Wait(milliseconds)) {
                return task.Result;
            }
            return default(T);
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
        /// Waits for a task to complete. If an exception occurs, the exception
        /// will be raised without being wrapped in a
        /// <see cref="AggregateException"/>.
        /// </summary>
        public static T WaitAndUnwrapExceptions<T>(this Task<T> task, Func<T> onCancel) {
            try {
                return task.GetAwaiter().GetResult();
            } catch (OperationCanceledException) {
                return onCancel();
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

        private sealed class SemaphoreLock : IDisposable {
            private SemaphoreSlim _semaphore;

            public SemaphoreLock(SemaphoreSlim semaphore) {
                _semaphore = semaphore;
            }

            public void Reset() {
                _semaphore = null;
            }

            void IDisposable.Dispose() {
                _semaphore?.Release();
                _semaphore = null;
                GC.SuppressFinalize(this);
            }

            ~SemaphoreLock() {
                _semaphore?.Release();
            }
        }

        public static async Task<IDisposable> LockAsync(this SemaphoreSlim semaphore, CancellationToken cancellationToken) {
            var res = new SemaphoreLock(semaphore);
            try {
                await semaphore.WaitAsync(cancellationToken);
                var res2 = res;
                res = null;
                return res2;
            } finally {
                res?.Reset();
            }
        }
    }
}