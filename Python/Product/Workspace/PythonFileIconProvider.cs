using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Workspace;
using Microsoft.VisualStudio.Workspace.Extensions.VS;

namespace Microsoft.PythonTools.Workspace {
    [Export(typeof(IVsFileIconProvider))]
    class PythonFileIconProvider : IVsFileIconProvider {
        public AsyncEvent<FileIconsChangedEvent> OnFileIconsChanged { get; set; }

        public bool GetIconForFile(string fullPath, out ImageMoniker imageMoniker, out int priority) {
            if (!string.IsNullOrEmpty(fullPath)) {
                var ext = Path.GetExtension(fullPath);
                if (PythonConstants.FileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase) ||
                    PythonConstants.WindowsFileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase)) {
                    imageMoniker = KnownMonikers.PYFileNode;
                    priority = 1000;
                    return true;
                }
            }

            imageMoniker = default(ImageMoniker);
            priority = 0;

            return false;
        }
    }
}
