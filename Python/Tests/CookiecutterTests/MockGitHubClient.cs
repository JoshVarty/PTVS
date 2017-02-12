using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools;
using Microsoft.CookiecutterTools.Model;

namespace CookiecutterTests {
    class MockGitHubClient : IGitHubClient {
        public Dictionary<Tuple<string, string>, string> Descriptions { get; } = new Dictionary<Tuple<string, string>, string>();

        public Task<bool> FileExistsAsync(GitHubRepoSearchItem repo, string filePath) {
            throw new NotImplementedException();
        }

        public Task<GitHubRepoSearchItem> GetRepositoryDetails(string owner, string name) {
            string description;
            if (Descriptions.TryGetValue(Tuple.Create(owner, name), out description)) {
                var item = new GitHubRepoSearchItem();
                item.Description = description;

                return Task.FromResult(item);
            }

            throw new WebException();
        }

        public Task<GitHubRepoSearchResult> SearchRepositoriesAsync(string requestUrl) {
            throw new NotImplementedException();
        }

        public Task<GitHubRepoSearchResult> StartSearchRepositoriesAsync(string[] terms) {
            throw new NotImplementedException();
        }
    }
}
