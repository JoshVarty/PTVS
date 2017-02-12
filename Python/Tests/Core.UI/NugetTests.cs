using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Python;
using TestUtilities.UI;

namespace PythonToolsUITests {
    [TestClass]
    public class NugetTests {
        [ClassInitialize]
        public static void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public void AddDifferentFileType() {
            using (var app = new VisualStudioApp()) {
                var project = app.OpenProject(@"TestData\HelloWorld.sln");
                string fullPath = TestData.GetPath(@"TestData\HelloWorld.sln");

                // "Python Environments", "References", "Search Paths", "Program.py"
                Assert.AreEqual(4, project.ProjectItems.Count);

                var item = project.ProjectItems.AddFromFileCopy(TestData.GetPath(@"TestData\Xaml\EmptyXName.xaml"));
                Assert.AreEqual("EmptyXName.xaml", item.Properties.Item("FileName").Value);
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public void FileNamesResolve() {
            using (var app = new VisualStudioApp()) {
                var project = app.OpenProject(@"TestData\HelloWorld.sln");

                var ps = System.Management.Automation.PowerShell.Create();
                ps.AddScript(@"
                        param($project)
                        $folderProjectItem = $project.ProjectItems.Item(""Program.py"")
                        $result =  $folderProjectItem.FileNames(1)
                ");
                ps.AddParameter("project", project);
                ps.Invoke();
                var result = ps.Runspace.SessionStateProxy.GetVariable("result");

                var folder = project.ProjectItems.Item("Program.py");
                string path = folder.get_FileNames(1);
                
                Assert.AreEqual(path, result);
            }
        }
    }
}
