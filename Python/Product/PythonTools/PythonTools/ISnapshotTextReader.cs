using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    public interface ISnapshotTextReader {
        ITextSnapshot Snapshot {
            get;
        }
    }
}
