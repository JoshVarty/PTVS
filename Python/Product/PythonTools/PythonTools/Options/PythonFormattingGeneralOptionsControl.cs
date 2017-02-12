using System.Windows.Forms;

namespace Microsoft.PythonTools.Options {
    public partial class PythonFormattingGeneralOptionsControl : UserControl {
        public PythonFormattingGeneralOptionsControl() {
            InitializeComponent();
        }

        internal void SyncControlWithPageSettings(PythonToolsService pyService) {
            _pasteRemovesReplPrompts.Checked = pyService.AdvancedOptions.PasteRemovesReplPrompts;
        }

        internal void SyncPageWithControlSettings(PythonToolsService pyService) {
            pyService.AdvancedOptions.PasteRemovesReplPrompts = _pasteRemovesReplPrompts.Checked;
        }
    }
}
