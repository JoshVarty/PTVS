using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;

namespace PythonToolsMockTests {
    [Export(typeof(IIntellisenseSessionStackMapService))]
    class MockIntellisenseSessionStackMapService : IIntellisenseSessionStackMapService {
        public IIntellisenseSessionStack GetStackForTextView(ITextView textView) {
            MockIntellisenseSessionStack stack;
            if (!textView.Properties.TryGetProperty<MockIntellisenseSessionStack>(typeof(MockIntellisenseSessionStack), out stack)) {
                textView.Properties[typeof(MockIntellisenseSessionStack)] = stack = new MockIntellisenseSessionStack();
            }
            return stack;
        }
    }
}
