using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Python;
using TestUtilities.UI;

namespace DjangoUITests {
    [TestClass]
    public class DjangoDebugProjectTests {
        [ClassInitialize]
        public static void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public void DebugDjangoProject() {
            using (var app = new VisualStudioApp()) {
                DebuggerUITests.DebugProject.OpenProjectAndBreak(
                    app,
                    TestData.GetPath(@"TestData\DjangoDebugProject.sln"),
                    @"TestApp\views.py",
                    5,
                    false);
            }
        }
    }
}
