using Microsoft.VisualStudio;
using TestUtilities;
using TestUtilities.SharedProject;

namespace Microsoft.VisualStudioTools.MockVsTests {
    public static class MockVsTestExtensions {
        public static IVisualStudioInstance ToMockVs(this SolutionFile self) {
            MockVs vs = new MockVs();
            vs.Invoke(() => ErrorHandler.ThrowOnFailure(vs.Solution.OpenSolutionFile(0, self.Filename)));
            return vs;
        }

    }
}
