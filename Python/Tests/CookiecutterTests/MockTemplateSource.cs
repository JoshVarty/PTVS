using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.CookiecutterTools.Model;

namespace CookiecutterTests {
    class MockTemplateSource : ILocalTemplateSource {
        public Dictionary<Tuple<string, string>, Tuple<Template[], string>> Templates { get; } = new Dictionary<Tuple<string, string>, Tuple<Template[], string>>();
        public Dictionary<string, bool?> UpdatesAvailable { get; } = new Dictionary<string, bool?>();
        public List<string> Updated { get; } = new List<string>();
        public List<string> Added { get; } = new List<string>();
        public List<string> Deleted { get; } = new List<string>();

        public Task<TemplateEnumerationResult> GetTemplatesAsync(string filter, string continuationToken, CancellationToken cancellationToken) {
            Tuple<Template[], string> result;
            if (Templates.TryGetValue(Tuple.Create(filter, continuationToken), out result)) {
                return Task.FromResult(new TemplateEnumerationResult(result.Item1, result.Item2));
            }
            return Task.FromResult(new TemplateEnumerationResult(new Template[0]));
        }

        public Task DeleteTemplateAsync(string repoPath) {
            Deleted.Add(repoPath);
            return Task.CompletedTask;
        }

        public Task UpdateTemplateAsync(string repoPath) {
            Updated.Add(repoPath);
            return Task.CompletedTask;
        }

        Task<bool?> ILocalTemplateSource.CheckForUpdateAsync(string repoPath) {
            bool? available;
            if (!UpdatesAvailable.TryGetValue(repoPath, out available)) {
                available = false;
            }
            return Task.FromResult(available);
        }

        public Task AddTemplateAsync(string repoPath) {
            Added.Add(repoPath);
            return Task.CompletedTask;
        }
    }
}
