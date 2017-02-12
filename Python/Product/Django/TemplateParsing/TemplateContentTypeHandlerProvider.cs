using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Html.Editor.ContentType.Def;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    [Export(typeof(IContentTypeHandlerProvider))]
    [ContentType(TemplateHtmlContentType.ContentTypeName)]
    internal class TemplateContentTypeHandlerProvider : IContentTypeHandlerProvider {
        public IContentTypeHandler GetContentTypeHandler() {
            return new TemplateContentTypeHandler();
        }
    }
}