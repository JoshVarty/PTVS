using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Python;
using TestUtilities.UI;
using TestUtilities.UI.Python;

namespace DjangoUITests {
    [TestClass]
    public class DjangoAzureProjectTests {
        [ClassInitialize]
        public static void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public void AddCloudProject() {
            using (var app = new VisualStudioApp()) {
                var project = app.CreateProject(
                    PythonVisualStudioApp.TemplateLanguageName,
                    PythonVisualStudioApp.DjangoWebProjectTemplate,
                    TestData.GetTempPath(),
                    "AddCloudProject"
                );
                app.SolutionExplorerTreeView.SelectProject(project);

                try {
                    app.ExecuteCommand("Project.ConverttoMicrosoftAzureCloudServiceProject");
                } catch (Exception ex1) {
                    try {
                        app.ExecuteCommand("Project.ConverttoWindowsAzureCloudServiceProject");
                    } catch (Exception ex2) {
                        Console.WriteLine("Unable to execute Project.ConverttoWindowsAzureCloudServiceProject.\r\n{0}", ex2);
                        try {
                            app.ExecuteCommand("Project.AddWindowsAzureCloudServiceProject");
                        } catch (Exception ex3) {
                            Console.WriteLine("Unable to execute Project.AddWindowsAzureCloudServiceProject.\r\n{0}", ex3);
                            throw ex1;
                        }
                    }
                }

                var res = app.SolutionExplorerTreeView.WaitForItem(
                    "Solution '" + app.Dte.Solution.Projects.Item(1).Name + "' (2 projects)",
                    app.Dte.Solution.Projects.Item(1).Name + ".Azure"
                );
                Assert.IsNotNull(res);
            }
        }
    }
}
