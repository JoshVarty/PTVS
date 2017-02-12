using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudioTools;

namespace TestUtilities.Mocks {
    public class MockUIThread : MockUIThreadBase {
        public override void Invoke(Action action) {
            action();
        }

        public override T Invoke<T>(Func<T> func) {
            return func();
        }

        public override Task InvokeAsync(Action action) {
            var tcs = new TaskCompletionSource<object>();
            UIThread.InvokeAsyncHelper(action, tcs);
            return tcs.Task;
        }

        public override Task<T> InvokeAsync<T>(Func<T> func) {
            var tcs = new TaskCompletionSource<T>();
            UIThread.InvokeAsyncHelper<T>(func, tcs);
            return tcs.Task;
        }

        public override Task InvokeAsync(Action action, CancellationToken cancellationToken) {
            var tcs = new TaskCompletionSource<object>();
            if (cancellationToken.CanBeCanceled) {
                cancellationToken.Register(() => tcs.TrySetCanceled());
            }
            UIThread.InvokeAsyncHelper(action, tcs);
            return tcs.Task;
        }

        public override Task<T> InvokeAsync<T>(Func<T> func, CancellationToken cancellationToken) {
            var tcs = new TaskCompletionSource<T>();
            if (cancellationToken.CanBeCanceled) {
                cancellationToken.Register(() => tcs.TrySetCanceled());
            }
            UIThread.InvokeAsyncHelper<T>(func, tcs);
            return tcs.Task;
        }

        public override Task InvokeTask(Func<Task> func) {
            var tcs = new TaskCompletionSource<object>();
            UIThread.InvokeTaskHelper(func, tcs);
            return tcs.Task;
        }

        public override Task<T> InvokeTask<T>(Func<Task<T>> func) {
            var tcs = new TaskCompletionSource<T>();
            UIThread.InvokeTaskHelper<T>(func, tcs);
            return tcs.Task;
        }

        public override void MustBeCalledFromUIThreadOrThrow() {
        }

        public override bool InvokeRequired {
            get { return false; }
        }
    }
}
