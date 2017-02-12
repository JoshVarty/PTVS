using System;
using System.Windows.Forms;

namespace Microsoft.PythonTools.Options {
    public partial class PythonAdvancedEditorOptionsControl : UserControl {
        public PythonAdvancedEditorOptionsControl() {
            InitializeComponent();
        }

        internal void SyncControlWithPageSettings(PythonToolsService pyService) {
            _enterCommits.Checked = pyService.AdvancedOptions.EnterCommitsIntellisense;
            _intersectMembers.Checked = pyService.AdvancedOptions.IntersectMembers;
            _filterCompletions.Checked = pyService.AdvancedOptions.FilterCompletions;
            _completionCommitedBy.Text = pyService.AdvancedOptions.CompletionCommittedBy;
            _newLineAfterCompleteCompletion.Checked = pyService.AdvancedOptions.AddNewLineAtEndOfFullyTypedWord;
            _outliningOnOpen.Checked = pyService.AdvancedOptions.EnterOutliningModeOnOpen;
            _pasteRemovesReplPrompts.Checked = pyService.AdvancedOptions.PasteRemovesReplPrompts;
            _colorNames.Checked = pyService.AdvancedOptions.ColorNames;
            _autoListIdentifiers.Checked = pyService.AdvancedOptions.AutoListIdentifiers;
        }

        internal void SyncPageWithControlSettings(PythonToolsService pyService) {
            pyService.AdvancedOptions.EnterCommitsIntellisense = _enterCommits.Checked;
            pyService.AdvancedOptions.IntersectMembers = _intersectMembers.Checked;
            pyService.AdvancedOptions.FilterCompletions = _filterCompletions.Checked;
            pyService.AdvancedOptions.CompletionCommittedBy = _completionCommitedBy.Text;
            pyService.AdvancedOptions.AddNewLineAtEndOfFullyTypedWord = _newLineAfterCompleteCompletion.Checked;
            pyService.AdvancedOptions.EnterOutliningModeOnOpen = _outliningOnOpen.Checked;
            pyService.AdvancedOptions.PasteRemovesReplPrompts = _pasteRemovesReplPrompts.Checked;
            pyService.AdvancedOptions.ColorNames = _colorNames.Checked;
            pyService.AdvancedOptions.AutoListIdentifiers = _autoListIdentifiers.Checked;
        }
    }
}
