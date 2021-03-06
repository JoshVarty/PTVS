using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Html.Editor.Document;
using Microsoft.PythonTools.Django.TemplateParsing;
using Microsoft.PythonTools.Intellisense;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.Web.Core.Text;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class DjangoCompletionSource : DjangoCompletionSourceBase {
        private readonly IServiceProvider _serviceProvider;

        public DjangoCompletionSource(IGlyphService glyphService, VsProjectAnalyzer analyzer, IServiceProvider serviceProvider, ITextBuffer textBuffer)
            : base(glyphService, analyzer, textBuffer) {
            _serviceProvider = serviceProvider;
        }

        public override void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets) {
            var doc = TemplateClassifier.HtmlEditorDocumentFromTextBuffer(_buffer);
            if (doc == null) {
                return;
            }
            var tree = doc.HtmlEditorTree;
            if (tree == null) {
                return;
            }
            tree.EnsureTreeReady();

            var primarySnapshot = tree.TextSnapshot;
            var nullableTriggerPoint = session.GetTriggerPoint(primarySnapshot);
            if (!nullableTriggerPoint.HasValue) {
                return;
            }
            var triggerPoint = nullableTriggerPoint.Value;

            var artifacts = doc.HtmlEditorTree.ArtifactCollection;
            var index = artifacts.GetItemContaining(triggerPoint.Position);
            if (index < 0) {
                return;
            }

            var artifact = artifacts[index] as TemplateArtifact;
            if (artifact == null) {
                return;
            }

            var artifactText = doc.HtmlEditorTree.ParseTree.Text.GetText(artifact.InnerRange);
            artifact.Parse(artifactText);

            ITrackingSpan applicableSpan;
            var completionSet = GetCompletionSet(session.GetOptions(_serviceProvider), _analyzer, artifact.TokenKind, artifactText, artifact.InnerRange.Start, triggerPoint, out applicableSpan);
            completionSets.Add(completionSet);
        }

        protected override IEnumerable<DjangoBlock> GetBlocks(IEnumerable<CompletionInfo> results, SnapshotPoint triggerPoint) {
            var doc = HtmlEditorDocument.FromTextBuffer(_buffer);
            if (doc == null) {
                yield break;
            }

            var artifacts = doc.HtmlEditorTree.ArtifactCollection.ItemsInRange(new TextRange(0, triggerPoint.Position));
            foreach (var artifact in artifacts.OfType<TemplateBlockArtifact>().Reverse()) {
                var artifactText = doc.HtmlEditorTree.ParseTree.Text.GetText(artifact.InnerRange);
                artifact.Parse(artifactText);
                if (artifact.Block != null) {
                    yield return artifact.Block;
                }
            }
        }
    }
}
