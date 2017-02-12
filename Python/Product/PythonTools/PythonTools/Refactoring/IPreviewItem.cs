using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Provides a common interface for our elements of the preview list.  Currently we break this into a top-level
    /// tree node for each file (FilePreviewItem) and a leaf node for each individual rename location (LocationPreviewItem).
    /// </summary>
    interface IPreviewItem {
        ushort Glyph {
            get;
        }

        IntPtr ImageList {
            get;
        }

        bool IsExpandable {
            get;
        }

        PreviewList Children {
            get;
        }

        string GetText(VSTREETEXTOPTIONS options);

        _VSTREESTATECHANGEREFRESH ToggleState();

        __PREVIEWCHANGESITEMCHECKSTATE CheckState {
            get;
        }

        void DisplayPreview(IVsTextView view);

        void Close(VSTREECLOSEACTIONS vSTREECLOSEACTIONS);

        Span? Selection {
            get;
        }
    }
}
