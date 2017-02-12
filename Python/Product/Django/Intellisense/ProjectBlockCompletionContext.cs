using System.Linq;
using Microsoft.Html.Editor.Document;
using Microsoft.PythonTools.Django.TemplateParsing;
using Microsoft.PythonTools.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class ProjectBlockCompletionContext : ProjectBlockCompletionContextBase {
        public ProjectBlockCompletionContext(VsProjectAnalyzer analyzer, ITextBuffer buffer)
            : base(analyzer, buffer.GetFileName()) {

            var doc = HtmlEditorDocument.FromTextBuffer(buffer);
            if (doc == null) {
                return;
            }

            var artifacts = doc.HtmlEditorTree.ArtifactCollection;
            foreach (var artifact in artifacts.OfType<TemplateBlockArtifact>()) {
                var artifactText = doc.HtmlEditorTree.ParseTree.Text.GetText(artifact.InnerRange);
                artifact.Parse(artifactText);
                if (artifact.Block != null) {
                    var varNames = artifact.Block.GetVariables();
                    foreach (var varName in varNames) {
                        AddLoopVariable(varName);
                    }
                }
            }
        }
    }
}
