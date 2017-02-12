using System.Collections.Generic;

namespace Microsoft.CookiecutterTools.Model {
    class TemplateEnumerationResult {
        public IList<Template> Templates { get; }
        public string ContinuationToken { get; }

        public TemplateEnumerationResult(IList<Template> templates, string continuationToken = null) {
            Templates = templates;
            ContinuationToken = continuationToken;
        }
    }
}
