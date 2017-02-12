using System;

namespace Microsoft.VisualStudioTools.Project {
    internal partial class ProjectNode {
        public event EventHandler<ProjectPropertyChangedArgs> OnProjectPropertyChanged;

        protected virtual void RaiseProjectPropertyChanged(string propertyName, string oldValue, string newValue) {
            var onPropChanged = OnProjectPropertyChanged;
            if (onPropChanged != null) {
                onPropChanged(this, new ProjectPropertyChangedArgs(propertyName, oldValue, newValue));
            }
        }
    }
}
