using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    using AP = AnalysisProtocol;

    internal class PythonParameter : IParameter {
        private readonly ISignature _signature;
        private readonly AP.Parameter _param;
        private readonly string _documentation;
        private readonly AnalysisVariable[] _variables;
        private readonly Span _locus, _ppLocus;

        public PythonParameter(ISignature signature, AP.Parameter param, Span locus, Span ppLocus, AnalysisVariable[] variables) {
            _signature = signature;
            _param = param;
            _locus = locus;
            _ppLocus = ppLocus;
            _documentation = _param.doc.LimitLines(15, stopAtFirstBlankLine: true);
            _variables = variables;
        }

        public string Documentation {
            get { return _documentation; }
        }

        public Span Locus {
            get { return _locus; }
        }

        public string Name {
            get { return _param.name; }
        }

        public ISignature Signature {
            get { return _signature; }
        }

        public Span PrettyPrintedLocus {
            get { return _ppLocus; }
        }

        public AnalysisVariable[] Variables {
            get {
                return _variables;
            }
        }
    }
}
