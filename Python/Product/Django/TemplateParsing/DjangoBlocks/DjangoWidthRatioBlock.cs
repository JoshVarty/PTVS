using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoWidthRatioBlock : DjangoBlock {
        private readonly DjangoVariable[] _variables;

        public DjangoWidthRatioBlock(BlockParseInfo parseInfo, params DjangoVariable[] variables)
            : base(parseInfo) {
            _variables = variables;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            return new DjangoWidthRatioBlock(parseInfo,
                ParseVariables(parseInfo.Args.Split(' '), parseInfo.Command.Length + parseInfo.Start, 3));
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return GetCompletions(context, position, _variables, 3);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            foreach (var span in base.GetSpans()) {
                yield return span;
            }

            foreach (var variable in _variables) {
                foreach (var span in variable.GetSpans()) {
                    yield return span;
                }
            }
        }
    }
}
