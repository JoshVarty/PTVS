using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoFilterBlock : DjangoBlock {
        private readonly DjangoVariable _variable;

        public DjangoFilterBlock(BlockParseInfo parseInfo, DjangoVariable variable)
            : base(parseInfo) {
            _variable = variable;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            int start = 0;
            for (int i = 0; i < parseInfo.Args.Length && parseInfo.Args[i] == ' '; i++, start++) {
            }

            var variable = DjangoVariable.Parse(
                "var|" + parseInfo.Args.Substring(start),
                parseInfo.Start + start + parseInfo.Command.Length
            );

            return new DjangoFilterBlock(parseInfo, variable);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            foreach (var span in base.GetSpans()) {
                yield return span;
            }

            if (_variable.Filters != null) {
                foreach (var filter in _variable.Filters) {
                    foreach (var span in filter.GetSpans(-4)) {
                        yield return span;
                    }
                }
            }
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return _variable.GetCompletions(context, position + 4);
        }
    }
}
