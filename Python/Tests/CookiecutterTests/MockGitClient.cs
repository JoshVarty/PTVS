using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.CookiecutterTools.Model;

namespace CookiecutterTests {
    class MockGitClient : IGitClient {
        public Task<string> CloneAsync(Redirector redirector, string repoUrl, string targetParentFolderPath) {
            throw new NotImplementedException();
        }

        public Task<DateTime?> GetLastCommitDateAsync(string repoFolderPath, string branch = null) {
            throw new NotImplementedException();
        }

        public Task<string> GetRemoteOriginAsync(string repoFolderPath) {
            throw new NotImplementedException();
        }

        public Task MergeAsync(string repoFolderPath) {
            throw new NotImplementedException();
        }

        public Task FetchAsync(string repoFolderPath) {
            throw new NotImplementedException();
        }

        public Task<string> CloneAsync(string repoUrl, string targetParentFolderPath) {
            throw new NotImplementedException();
        }
    }
}
