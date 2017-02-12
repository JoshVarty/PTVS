using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoLoadBlock : DjangoBlock {
        private readonly int _fromStart, _nameStart, _fromNameStart;

        public DjangoLoadBlock(BlockParseInfo parseInfo, int fromStart, int nameStart, int fromNameStart)
            : base(parseInfo) {
            _fromStart = fromStart;
            _nameStart = nameStart;
            _fromNameStart = fromNameStart;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            // TODO: Need to handle whitespace better
            // TODO: Need to split identifiers into individual components
            var words = parseInfo.Args.Split(' ');
            int fromNameStart = -1;
            int fromStart = -1;
            int nameStart = parseInfo.Start + 1;
            for (int i = 1; i < words.Length; i++) {
                if (String.IsNullOrWhiteSpace(words[i])) {
                    nameStart += words[i].Length + 1;
                } else {
                    break;
                }
            }

            if (words.Length >= 4 && words[words.Length - 2] == "from") {
                // load fob from oar

            }

            return new DjangoLoadBlock(parseInfo, fromStart, nameStart, fromNameStart);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            yield return new BlockClassification(new Span(ParseInfo.Start, 4), Classification.Keyword);
            if (_fromStart != -1) {
                yield return new BlockClassification(new Span(_fromStart, 4), Classification.Keyword);
            }
        }
    }
}
