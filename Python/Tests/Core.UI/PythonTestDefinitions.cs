using System.ComponentModel.Composition;
using TestUtilities.SharedProject;

namespace PythonToolsUITests {
    public sealed class PythonTestDefintions {
        [Export]
        [ProjectExtension(".pyproj")]
        [ProjectTypeGuid("888888a0-9f3d-457c-b088-3a5042f75d52")]
        [CodeExtension(".py")]
        [SampleCode("print('hi')")]
        internal static ProjectTypeDefinition ProjectTypeDefinition = new ProjectTypeDefinition();
    }
}
