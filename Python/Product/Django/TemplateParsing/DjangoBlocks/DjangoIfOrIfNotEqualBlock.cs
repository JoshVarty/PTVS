using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoIfOrIfNotEqualBlock : DjangoBlock {
        private readonly DjangoVariable[] _args;

        public DjangoIfOrIfNotEqualBlock(BlockParseInfo parseInfo, params DjangoVariable[] args)
            : base(parseInfo) {
            _args = args;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            return new DjangoIfOrIfNotEqualBlock(
                parseInfo,
                ParseVariables(parseInfo.Args.Split(' '), parseInfo.Start + parseInfo.Command.Length, 2)
            );
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return GetCompletions(context, position, _args, 2);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            foreach (var span in base.GetSpans()) {
                yield return span;
            }

            foreach (var variable in _args) {
                foreach (var span in variable.GetSpans()) {
                    yield return span;
                }
            }
        }
    }
}
