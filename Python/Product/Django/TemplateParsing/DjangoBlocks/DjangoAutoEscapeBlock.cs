using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    /// <summary>
    /// args: 'on' or 'off'
    /// </summary>
    class DjangoAutoEscapeBlock : DjangoBlock {
        private readonly int _argStart, _argLength;

        public DjangoAutoEscapeBlock(BlockParseInfo parseInfo, int argStart, int argLength)
            : base(parseInfo) {
            _argStart = argStart;
            _argLength = argLength;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            var args = parseInfo.Args.Split(' ');
            int argStart = -1, argLength = -1;
            for (int i = 0; i < args.Length; i++) {
                var word = args[i];
                if (!String.IsNullOrEmpty(word)) {
                    if (word.StartsWith("\r") || word.StartsWith("\n")) {
                        // unterminated tag
                        break;
                    }
                    argStart = parseInfo.Start + parseInfo.Command.Length + i;
                    argLength = args[i].Length;
                    break;
                }
            }

            return new DjangoAutoEscapeBlock(parseInfo, argStart, argLength);
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            if (_argStart == -1) {
                return new[] {
                    new CompletionInfo(
                        "on",
                        StandardGlyphGroup.GlyphGroupVariable
                    ),
                    new CompletionInfo(
                        "off",
                        StandardGlyphGroup.GlyphGroupVariable
                    )
                };
            }
            return new CompletionInfo[0];
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            foreach (var span in base.GetSpans()) {
                yield return span;
            }

            if (_argStart != -1) {
                yield return new BlockClassification(
                    new Span(_argStart, _argLength),
                    Classification.Keyword
                );
            }
        }
    }
}
