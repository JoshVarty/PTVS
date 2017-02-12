using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoTemplateTagBlock : DjangoBlock {
        private readonly int _argStart;
        private readonly string _tagType;

        public DjangoTemplateTagBlock(BlockParseInfo parseInfo, int argStart, string tagType)
            : base(parseInfo) {
            _argStart = argStart;
            _tagType = tagType;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            var words = parseInfo.Args.Split(' ');
            int argStart = parseInfo.Command.Length + parseInfo.Start;
            string tagType = null;

            foreach (var word in words) {
                if (!String.IsNullOrEmpty(word)) {
                    tagType = word;
                    break;
                }
                argStart += 1;
            }
            // TODO: It'd be nice to report an error if we have more than one word
            // or if it's an unrecognized tag
            return new DjangoTemplateTagBlock(parseInfo, argStart, tagType);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            foreach (var span in base.GetSpans()) {
                yield return span;
            }
            if (_tagType != null) {
                yield return new BlockClassification(
                    new Span(_argStart, _tagType.Length),
                    Classification.Keyword
                );
            }
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            if (_tagType == null) {
                return GetTagList();
            } else if (position >= _argStart && position < _argStart + _tagType.Length) {
                // filter based upon entered text
                string filter = _tagType.Substring(0, position - _argStart);
                return GetTagList().Where(tag => tag.DisplayText.StartsWith(filter));
            }
            return new CompletionInfo[0];
        }

        private static CompletionInfo[] GetTagList() {
            return new[] {
                    new CompletionInfo("openblock", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("closeblock", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("openvariable", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("closevariable", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("openbrace", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("closebrace", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("opencomment", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("closecomment", StandardGlyphGroup.GlyphKeyword),
                };
        }
    }
}
