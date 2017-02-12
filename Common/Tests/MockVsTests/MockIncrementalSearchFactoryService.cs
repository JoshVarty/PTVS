using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.IncrementalSearch;
using TestUtilities.Mocks;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IIncrementalSearchFactoryService))]
    class MockIncrementalSearchFactoryService : IIncrementalSearchFactoryService {
        public IIncrementalSearch GetIncrementalSearch(ITextView textView) {
            return new MockIncrementalSearch((MockTextView)textView);
        }
    }
}
