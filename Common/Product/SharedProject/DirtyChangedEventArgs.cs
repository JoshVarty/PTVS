using System;

namespace Microsoft.VisualStudioTools.Project {
    public sealed class DirtyChangedEventArgs : EventArgs {
        private readonly bool _isDirty;
        public static readonly DirtyChangedEventArgs DirtyValue = new DirtyChangedEventArgs(true);
        public static readonly DirtyChangedEventArgs SavedValue = new DirtyChangedEventArgs(false);

        public DirtyChangedEventArgs(bool isDirty) {
            _isDirty = isDirty;
        }

        public bool IsDirty {
            get {
                return _isDirty;
            }
        }
    }
}
