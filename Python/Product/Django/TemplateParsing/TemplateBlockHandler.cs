using System.Collections.Generic;
using Microsoft.Html.Editor.ContainedLanguage.Generators;
using Microsoft.Html.Editor.ContainedLanguage.Handlers;
using Microsoft.Html.Editor.ContentType;
using Microsoft.Html.Editor.Tree;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    internal class TemplateBlockHandler : ArtifactBasedBlockHandler {
        private readonly List<TemplateArtifact> _trackedArtifacts = new List<TemplateArtifact>();

        public TemplateBlockHandler(HtmlEditorTree editorTree)
            : base(editorTree, ContentTypeManager.GetContentType(TemplateHtmlContentType.ContentTypeName)) {
        }

        protected override BufferGenerator CreateBufferGenerator() {
            return new TemplateBufferGenerator(EditorTree, LanguageBlocks);
        }

        protected override void OnUpdateCompleted(object sender, HtmlTreeUpdatedEventArgs e) {
            base.OnUpdateCompleted(sender, e);
            if (e.FullParse) {
                UpdateBuffer(force: true);
            }
        }
    }
}
