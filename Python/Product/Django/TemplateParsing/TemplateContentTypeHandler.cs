using Microsoft.Html.Core.Artifacts;
using Microsoft.Html.Editor.ContentType.Handlers;
using Microsoft.Html.Editor.Tree;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    internal class TemplateContentTypeHandler : HtmlServerCodeContentTypeHandler {
        public override void Init(HtmlEditorTree editorTree) {
            base.Init(editorTree);
            ContainedLanguageBlockHandler = new TemplateBlockHandler(editorTree);
        }

        public override ArtifactCollection CreateArtifactCollection() {
            return new TemplateArtifactCollection();
        }
    }
}