using Microsoft.Html.Editor.ContainedLanguage.Common;
using Microsoft.Html.Editor.ContainedLanguage.Generators;
using Microsoft.Html.Editor.ContentType;
using Microsoft.Html.Editor.Tree;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    internal class TemplateBufferGenerator : ArtifactBasedBufferGenerator {
        public TemplateBufferGenerator(HtmlEditorTree editorTree, LanguageBlockCollection languageBlocks)
            : base(editorTree, languageBlocks, ContentTypeManager.GetContentType(TemplateTagContentType.ContentTypeName)) {
        }
    }
}
