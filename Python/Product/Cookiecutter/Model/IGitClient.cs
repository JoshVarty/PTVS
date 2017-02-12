using System;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    interface IGitClient {
        Task<string> CloneAsync(string repoUrl, string targetParentFolderPath);
        Task<string> GetRemoteOriginAsync(string repoFolderPath);
        Task<DateTime?> GetLastCommitDateAsync(string repoFolderPath, string branch = null);
        Task FetchAsync(string repoFolderPath);
        Task MergeAsync(string repoFolderPath);
    }
}
