using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    /// <summary>
    /// Handles blocks which don't take any arguments.  Includes debug, csrf, comment
    /// </summary>
    class DjangoArgumentlessBlock : DjangoBlock {
        public DjangoArgumentlessBlock(BlockParseInfo parseInfo)
            : base(parseInfo) {
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            return new DjangoArgumentlessBlock(parseInfo);
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return new CompletionInfo[0];
        }
    }
}
