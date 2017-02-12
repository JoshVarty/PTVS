using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    /// <summary>
    /// Captures information about a Django variable expression's filter and its associate argument if one was present.
    /// </summary>
    class DjangoFilter {
        public readonly int FilterStart, ArgStart;
        public readonly string Filter;
        public readonly DjangoVariableValue Arg;

        public DjangoFilter(string filterName, int filterStart, DjangoVariableValue arg = null, int groupStart = 0) {
            Filter = filterName;
            FilterStart = filterStart;
            ArgStart = groupStart;
            Arg = arg;
        }

        public static DjangoFilter Variable(string filterName, int filterStart, string variable, int groupStart) {
            return new DjangoFilter(filterName, filterStart, new DjangoVariableValue(variable, DjangoVariableKind.Variable), groupStart);
        }

        public static DjangoFilter Constant(string filterName, int filterStart, string variable, int groupStart) {
            return new DjangoFilter(filterName, filterStart, new DjangoVariableValue(variable, DjangoVariableKind.Constant), groupStart);
        }

        public static DjangoFilter Number(string filterName, int filterStart, string variable, int groupStart) {
            return new DjangoFilter(filterName, filterStart, new DjangoVariableValue(variable, DjangoVariableKind.Number), groupStart);
        }

        internal IEnumerable<BlockClassification> GetSpans(int expressionStart = 0) {
            yield return new BlockClassification(
                new Span(FilterStart + expressionStart, Filter.Length),
                Classification.Identifier
            );

            if (Arg != null) {
                foreach (var span in Arg.GetSpans(ArgStart + expressionStart)) {
                    yield return span;
                }
            }
        }
    }

}
