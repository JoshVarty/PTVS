using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Projection;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IBufferGraphFactoryService))]
    class MockBufferGraphFactoryService : IBufferGraphFactoryService {
        public IBufferGraph CreateBufferGraph(VisualStudio.Text.ITextBuffer textBuffer) {
            throw new NotImplementedException();
        }
    }
}
