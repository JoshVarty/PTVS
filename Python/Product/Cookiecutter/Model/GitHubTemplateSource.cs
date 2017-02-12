using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    class GitHubTemplateSource : ITemplateSource {
        private readonly IGitHubClient _client;
        private const string TemplateDefinitionFileName = "cookiecutter.json";

        public GitHubTemplateSource(IGitHubClient client) {
            _client = client;
        }

        public async Task<TemplateEnumerationResult> GetTemplatesAsync(string filter, string continuationToken, CancellationToken cancellationToken) {
            var terms = new List<string>();
            terms.Add("cookiecutter");

            var keywords = SearchUtils.ParseKeywords(filter);
            if (keywords != null && keywords.Length > 0) {
                terms.AddRange(keywords);
            }

            var templates = new List<Template>();

            try {
                GitHubRepoSearchResult result;
                if (continuationToken == null) {
                    result = await _client.StartSearchRepositoriesAsync(terms.ToArray());
                } else {
                    result = await _client.SearchRepositoriesAsync(continuationToken);
                }

                foreach (var repo in result.Items) {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (await _client.FileExistsAsync(repo, TemplateDefinitionFileName)) {
                        var template = new Template();
                        template.RemoteUrl = repo.HtmlUrl;
                        template.Name = repo.FullName;
                        template.Description = repo.Description;
                        template.AvatarUrl = repo.Owner.AvatarUrl;
                        template.OwnerUrl = repo.Owner.HtmlUrl;
                        templates.Add(template);
                    }
                }

                return new TemplateEnumerationResult(templates, result.Links.Next);
            } catch (WebException ex) {
                throw new TemplateEnumerationException(Strings.GitHubSearchError, ex);
            }
        }

        public void InvalidateCache() {
        }
    }
}
