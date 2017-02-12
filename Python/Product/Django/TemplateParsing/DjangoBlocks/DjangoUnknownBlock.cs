using Microsoft.VisualStudio.Text;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoUnknownBlock : DjangoBlock {
        public DjangoUnknownBlock(BlockParseInfo parseInfo)
            : base(parseInfo) {
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            return new DjangoUnknownBlock(parseInfo);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            yield return new BlockClassification(
                new Span(ParseInfo.Start, ParseInfo.Command.Length),
                Classification.Keyword
            );

            if (ParseInfo.Args.Length > 0) {
                yield return new BlockClassification(
                    new Span(ParseInfo.Start + ParseInfo.Command.Length, ParseInfo.Args.Length),
                    Classification.ExcludedCode
                );
            }
        }
    }
}
