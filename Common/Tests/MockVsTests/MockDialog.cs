using System;
using System.Threading;
using TestUtilities;

namespace Microsoft.VisualStudioTools.MockVsTests {
    class MockDialog {
        public readonly string Title;
        public readonly MockVs Vs;
        public int DialogResult = 0;
        private AutoResetEvent _dismiss = new AutoResetEvent(false);

        public MockDialog(MockVs vs, string title) {
            Title = title;
            Vs = vs;
        }

        public virtual void Type(string text) {
            switch (text) {
                case "\r":
                    Close((int)MessageBoxButton.Ok);
                    break;
                default:
                    throw new NotImplementedException("Unhandled dialog text: " + text);
            }
        }

        public virtual void Run() {
            Vs.RunMessageLoop(_dismiss);
        }

        public virtual void Close(int result) {
            DialogResult = result;
            _dismiss.Set();
        }
    }
}
