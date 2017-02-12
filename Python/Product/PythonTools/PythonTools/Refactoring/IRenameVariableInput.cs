using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Provides inputs/UI to the extract method refactoring.  Enables driving of the refactoring programmatically
    /// or via UI.
    /// </summary>
    interface IRenameVariableInput {
        RenameVariableRequest GetRenameInfo(string originalName, PythonLanguageVersion languageVersion);

        void CannotRename(string message);

        void ClearRefactorPane();

        void OutputLog(string message);

        ITextBuffer GetBufferForDocument(string filename);

        IVsLinkedUndoTransactionManager BeginGlobalUndo();

        void EndGlobalUndo(IVsLinkedUndoTransactionManager undo);

    }
}
