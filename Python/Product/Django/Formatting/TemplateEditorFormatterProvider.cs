using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.Formatting;

namespace Microsoft.PythonTools.Django.Formatting {
    [Export(typeof(IEditorFormatterProvider))]
    [ContentType(TemplateHtmlContentType.ContentTypeName)]
    internal class TemplateEditorFormatterProvider : IEditorFormatterProvider {
        public IEditorFormatter CreateFormatter() {
            return null;
        }

        public IEditorRangeFormatter CreateRangeFormatter() {
            return null;
        }
    }
}
