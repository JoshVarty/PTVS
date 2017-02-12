using System;
using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    class SnapshotCookie : IIntellisenseCookie {
        private readonly ITextSnapshot _snapshot;
        
        public SnapshotCookie(ITextSnapshot snapshot) {
            _snapshot = snapshot;
        }

        public ITextSnapshot Snapshot {
            get {
                return _snapshot;
            }
        }

        #region IAnalysisCookie Members

        public string GetLine(int lineNo) {
            try {
                return _snapshot.GetLineFromLineNumber(lineNo - 1).GetText();
            } catch (ArgumentOutOfRangeException) {
                return string.Empty;
            }
        }

        #endregion
    }
}
