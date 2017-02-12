using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    [Export(typeof(IClassifierProvider)), ContentType(TemplateTagContentType.ContentTypeName)]
    class TemplateClassifierProvider : TemplateClassifierProviderBase {
        [ImportingConstructor]
        public TemplateClassifierProvider(IContentTypeRegistryService contentTypeRegistryService, IClassificationTypeRegistryService classificationRegistry)
            : base(TemplateTagContentType.ContentTypeName, contentTypeRegistryService, classificationRegistry) {
        }
    }
}