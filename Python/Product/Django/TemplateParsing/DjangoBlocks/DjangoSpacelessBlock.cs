using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoSpacelessBlock : DjangoBlock {
        public DjangoSpacelessBlock(BlockParseInfo parseInfo)
            : base(parseInfo) {
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            return new DjangoSpacelessBlock(parseInfo);
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return new CompletionInfo[0];
        }
    }
}
