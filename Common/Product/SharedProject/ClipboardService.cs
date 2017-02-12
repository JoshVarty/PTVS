using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.VisualStudioTools {

    class ClipboardService : IClipboardService {
        public void SetClipboard(IDataObject dataObject) {
            ErrorHandler.ThrowOnFailure(UnsafeNativeMethods.OleSetClipboard(dataObject));
        }

        public IDataObject GetClipboard() {
            IDataObject res;
            ErrorHandler.ThrowOnFailure(UnsafeNativeMethods.OleGetClipboard(out res));
            return res;
        }

        public void FlushClipboard() {
            ErrorHandler.ThrowOnFailure(UnsafeNativeMethods.OleFlushClipboard());
        }

        public bool OpenClipboard() {
            int res = UnsafeNativeMethods.OpenClipboard(IntPtr.Zero);
            ErrorHandler.ThrowOnFailure(res);
            return res == 1;
        }

        public void EmptyClipboard() {
            ErrorHandler.ThrowOnFailure(UnsafeNativeMethods.EmptyClipboard());
        }

        public void CloseClipboard() {
            ErrorHandler.ThrowOnFailure(UnsafeNativeMethods.CloseClipboard());
        }
    }
}