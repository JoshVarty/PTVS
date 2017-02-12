using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    /// <summary>
    /// Handles blocks which take an unlimited number of variable arguments.  Includes
    /// ifchanged and firstof
    /// </summary>
    class DjangoMultiVariableArgumentBlock : DjangoBlock {
        private readonly DjangoVariable[] _variables;

        public DjangoMultiVariableArgumentBlock(BlockParseInfo parseInfo, params DjangoVariable[] variables)
            : base(parseInfo) {
            _variables = variables;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            var words = parseInfo.Args.Split(' ');
            List<BlockClassification> argClassifications = new List<BlockClassification>();

            int wordStart = parseInfo.Start + parseInfo.Command.Length;

            return new DjangoMultiVariableArgumentBlock(parseInfo, ParseVariables(words, wordStart));
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            return GetCompletions(context, position, _variables);
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
