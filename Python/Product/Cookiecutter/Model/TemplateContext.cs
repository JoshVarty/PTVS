using System.Collections.Generic;

namespace Microsoft.CookiecutterTools.Model {
    class TemplateContext {
        public List<ContextItem> Items { get; } = new List<ContextItem>();
        public List<DteCommand> Commands { get; } = new List<DteCommand>();
    }
}
