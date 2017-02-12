using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    interface ITemplateSource {
        Task<TemplateEnumerationResult> GetTemplatesAsync(string filter, string continuationToken, CancellationToken cancellationToken);
    }
}
