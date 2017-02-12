using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    class FeedTemplateSource : ITemplateSource {
        private readonly Uri _feedLocation;
        private List<Template> _cache;

        public FeedTemplateSource(Uri feedLocation) {
            _feedLocation = feedLocation;
        }

        public async Task<TemplateEnumerationResult> GetTemplatesAsync(string filter, string continuationToken, CancellationToken cancellationToken) {
            if (_cache == null) {
                await BuildCacheAsync();
            }

            var keywords = SearchUtils.ParseKeywords(filter);

            var templates = new List<Template>();
            foreach (var template in _cache) {
                cancellationToken.ThrowIfCancellationRequested();

                if (SearchUtils.SearchMatches(keywords, template)) {
                    templates.Add(template.Clone());
                }
            }

            return new TemplateEnumerationResult(templates);
        }

        public void InvalidateCache() {
            _cache = null;
        }

        private async Task BuildCacheAsync() {
            _cache = new List<Template>();

            try {
                var client = new WebClient();
                var feed = await client.DownloadStringTaskAsync(_feedLocation);
                var feedUrls = feed.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entry in feedUrls) {
                    var template = new Template() {
                        RemoteUrl = entry,
                    };

                    string owner;
                    string name;
                    if (ParseUtils.ParseGitHubRepoOwnerAndName(entry, out owner, out name)) {
                        template.Name = owner + "/" + name;
                    } else {
                        template.Name = entry;
                    }

                    _cache.Add(template);
                }
            } catch (WebException ex) {
                throw new TemplateEnumerationException(Strings.FeedLoadError, ex);
            }
        }
    }
}
