using System;
using Microsoft.PythonTools.InteractiveWindow.Shell;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.PythonTools.Repl {
    sealed class InteractiveWindowEvents : IVsWindowFrameNotify, IVsWindowFrameNotify2 {
        private readonly IVsWindowFrame2 _frame;
        private readonly IVsInteractiveWindow _window;
        private uint _cookie;

        public static InteractiveWindowEvents TryGet(IVsInteractiveWindow window) {
            InteractiveWindowEvents events;
            if (window.InteractiveWindow.Properties.TryGetProperty(typeof(InteractiveWindowEvents), out events) &&
                !(events?.IsDisposed ?? true)) {
                return events;
            }
            return null;
        }

        public static InteractiveWindowEvents GetOrCreate(IVsInteractiveWindow window) {
            return window.InteractiveWindow.Properties.GetOrCreateSingletonProperty(typeof(InteractiveWindowEvents), () => {
                var frame = (window as ToolWindowPane)?.Frame as IVsWindowFrame2;
                if (frame == null) {
                    return null;
                }
                return new InteractiveWindowEvents(frame, window);
            });
        }

        private InteractiveWindowEvents(IVsWindowFrame2 frame, IVsInteractiveWindow window) {
            _frame = frame;
            _window = window;
            ErrorHandler.ThrowOnFailure(frame.Advise(this, out _cookie));
        }

        public bool IsDisposed => _cookie == 0;

        public event EventHandler Shown;
        public event EventHandler Hidden;
        public event EventHandler Closed;
        public event EventHandler Moved;
        public event EventHandler Resized;

        int IVsWindowFrameNotify2.OnClose(ref uint pgrfSaveOptions) {
            _frame.Unadvise(_cookie);
            _cookie = 0;
            Closed?.Invoke(_window, EventArgs.Empty);
            return VSConstants.S_OK;
        }

        int IVsWindowFrameNotify.OnShow(int fShow) {
            if (fShow == 0) {
                Hidden?.Invoke(_window, EventArgs.Empty);
            } else {
                Shown?.Invoke(_window, EventArgs.Empty);
            }
            return VSConstants.S_OK;
        }

        int IVsWindowFrameNotify.OnMove() {
            Moved?.Invoke(_window, EventArgs.Empty);
            return VSConstants.S_OK;
        }

        int IVsWindowFrameNotify.OnSize() {
            Resized?.Invoke(_window, EventArgs.Empty);
            return VSConstants.S_OK;
        }

        int IVsWindowFrameNotify.OnDockableChange(int fDockable) {
            return VSConstants.E_NOTIMPL;
        }
    }
}
