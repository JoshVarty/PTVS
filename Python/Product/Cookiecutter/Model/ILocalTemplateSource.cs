using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    interface ILocalTemplateSource : ITemplateSource {
        Task DeleteTemplateAsync(string repoPath);
        Task<bool?> CheckForUpdateAsync(string repoPath);
        Task UpdateTemplateAsync(string repoPath);
        Task AddTemplateAsync(string repoPath);
    }
}
