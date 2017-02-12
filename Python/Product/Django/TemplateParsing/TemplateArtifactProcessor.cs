using System.IO;
using Microsoft.Html.Core.Artifacts;
using Microsoft.Web.Core.Text;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    /// <summary>
    /// Produces the <see cref="TemplateArtifactCollection"/> for a given text document.
    /// </summary>
    internal class TemplateArtifactProcessor : IArtifactProcessor {
        private class TextProviderReader : TextReader {
            private readonly ITextProvider _text;
            private int _pos;

            public TextProviderReader(ITextProvider text) {
                _text = text;
            }

            public override int Read() {
                if (_pos >= _text.Length) {
                    return -1;
                }

                return _text[_pos++];
            }
        }

        public void GetArtifacts(ITextProvider text, ArtifactCollection artifactCollection) {
            var reader = new TextProviderReader(text);
            var tokenizer = new TemplateTokenizer(reader);
            foreach (var token in tokenizer.GetTokens()) {
                if (token.Kind != TemplateTokenKind.Text) {
                    var range = TextRange.FromBounds(token.Start, token.End + 1);
                    var artifact = TemplateArtifact.Create(token.Kind, range, token.IsClosed);
                    artifactCollection.Add(artifact);
                }
            }
        }

        public ArtifactCollection CreateArtifactCollection() {
            return new TemplateArtifactCollection();
        }

        public bool IsReady {
            get { return true; }
        }

        public string LeftSeparator {
            get { return ""; }
        }

        public string RightSeparator {
            get { return ""; }
        }

        public string LeftCommentSeparator {
            get { return "{#"; }
        }

        public string RightCommentSeparator {
            get { return "#}"; }
        }
    }
}
