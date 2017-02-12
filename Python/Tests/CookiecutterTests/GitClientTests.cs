using System.IO;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;

namespace CookiecutterTests {
    [TestClass]
    public class GitClientTests {
        private const string GitHubWindowsIncompatibleRepoUrl = "https://github.com/huguesv/cookiecutter-pyvanguard";

       [TestMethod]
        public async Task CloneWindowsIncompatibleRepo() {
            var client = GitClientProvider.Create(null, null);

            var outputParentFolder = TestData.GetTempPath("Cookiecutter", true);

            try {
                // Clone a repo that uses folders with invalid characters on Windows
                await client.CloneAsync(GitHubWindowsIncompatibleRepoUrl, outputParentFolder);
                Assert.Fail($"Failed to generate exception when cloning repository. You should manually check that cloning '{GitHubWindowsIncompatibleRepoUrl}' still fails on Windows.");
            } catch (ProcessException ex) {
                Assert.AreNotEqual(0, ex.Result.StandardErrorLines.Length);
            }

            // Make sure failed clone is cleaned up
            Assert.AreEqual(0, Directory.GetDirectories(outputParentFolder).Length);
        }
    }
}
