using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;

namespace CookiecutterTests {
    [TestClass]
    public class GitHubDiscoveryTests {
        //[TestMethod]
        public async Task Discover() {
            // Enable this test to create a list of templates currently available on GitHub
            // Note that this takes about 3 mins due to pauses between each query,
            // which are there to avoid 403 error.
            var templates = await GetAllGitHubTemplates();
            var urls = templates.Select(t => t.RemoteUrl).OrderBy(val => val);
            var owners = templates.
                Select(t => GetTemplateOwner(t)).
                Distinct().
                Where(val => !string.IsNullOrEmpty(val)).
                OrderBy(val => val);

            var folderPath = TestData.GetTempPath("GitHubDiscovery");
            File.WriteAllLines(Path.Combine(folderPath, "CookiecutterUrls.txt"), urls);
            File.WriteAllLines(Path.Combine(folderPath, "CookiecutterOwners.txt"), owners);
        }

        private static async Task<List<Template>> GetAllGitHubTemplates() {
            var source = new GitHubTemplateSource(new GitHubClient());
            string continuation = null;
            var templates = new List<Template>();
            do {
               var result = await source.GetTemplatesAsync("", continuation, CancellationToken.None);
                continuation = result.ContinuationToken;
                foreach (var template in result.Templates) {
                    Console.WriteLine(template.RemoteUrl);
                }
                templates.AddRange(result.Templates);
                Thread.Sleep(1000);
            } while (!string.IsNullOrEmpty(continuation));
            return templates;
        }

        private string GetTemplateOwner(Template template) {
            string owner;
            string name;
            if (ParseUtils.ParseGitHubRepoOwnerAndName(template.RemoteUrl, out owner, out name)) {
                return owner;
            } else {
                return null;
            }
        }
    }
}
