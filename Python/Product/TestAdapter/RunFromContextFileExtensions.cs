using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestWindow.Extensibility;

namespace Microsoft.PythonTools.TestAdapter {
    [Export(typeof(IRunFromContextFileExtensions))]
    class RunFromContextFileExtensions : IRunFromContextFileExtensions {
        #region IRunFromContextFileExtensions Members

        public IEnumerable<string> FileTypes {
            get { 
                return new[] { PythonConstants.FileExtension, PythonConstants.WindowsFileExtension };
            }
        }

        #endregion
    }
}
