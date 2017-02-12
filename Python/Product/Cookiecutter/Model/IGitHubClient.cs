using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microsoft.CookiecutterTools.Model {
    interface IGitHubClient {
        Task<GitHubRepoSearchResult> SearchRepositoriesAsync(string requestUrl);
        Task<GitHubRepoSearchResult> StartSearchRepositoriesAsync(string[] terms);
        Task<GitHubRepoSearchItem> GetRepositoryDetails(string owner, string name);
        Task<bool> FileExistsAsync(GitHubRepoSearchItem repo, string filePath);
    }

    struct GitHubRepoSearchResult {
        [JsonProperty("total_count")]
        public int TotalCount;

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults;

        [JsonProperty("items")]
        public GitHubRepoSearchItem[] Items;

        public GitHubPaginationLinks Links;
    }

    struct GitHubRepoSearchItem {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("full_name")]
        public string FullName;

        [JsonProperty("html_url")]
        public string HtmlUrl;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("fork")]
        public bool IsFork;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("owner")]
        public GitHubRepoOwner Owner;
    }

    struct GitHubRepoOwner {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("html_url")]
        public string HtmlUrl;

        [JsonProperty("avatar_url")]
        public string AvatarUrl;
    }

    struct GitHubPaginationLinks {
        public string Next;
        public string Prev;
        public string First;
        public string Last;
    }
}
