using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Operations;
using TestUtilities.Mocks;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IEditorOperationsFactoryService))]
    class MockEditorOperationsFactoryService : IEditorOperationsFactoryService {
        public IEditorOperations GetEditorOperations(VisualStudio.Text.Editor.ITextView textView) {
            return new MockEditorOperations((MockTextView)textView);
        }
    }
}
