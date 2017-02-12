using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudioTools;

namespace PythonToolsMockTests {
    class MockClipboardService : IClipboardService {
        private IDataObject _data;

        public void SetClipboard(IDataObject dataObject) {
            _data = dataObject;
        }

        public IDataObject GetClipboard() {
            return _data;
        }

        public void FlushClipboard() {
            // TODO: We could try and copy the data locally, instead we just keep it alive.
        }

        public bool OpenClipboard() {
            return true;
        }

        public void EmptyClipboard() {
            _data = null;
        }

        public void CloseClipboard() {
        }
    }

}
