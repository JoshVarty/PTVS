using System;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Intellisense {
    using AP = AnalysisProtocol;

    public sealed class CompletionResult {
        private readonly string _completion;
        private readonly PythonMemberType _memberType;
        private readonly string _name, _doc;
        private readonly AP.CompletionValue[] _values;

        internal CompletionResult(string name, PythonMemberType memberType) {
            _name = name;
            _completion = name;
            _memberType = memberType;
        }

        internal CompletionResult(string name, string completion, string doc, PythonMemberType memberType, AP.CompletionValue[] values) {
            _name = name;
            _memberType = memberType;
            _completion = completion;
            _doc = doc;
            _values = values;
        }

        public string Completion => _completion;
        public string Documentation => _doc;
        public PythonMemberType MemberType => _memberType;
        public string Name => _name;

        internal AP.CompletionValue[] Values => _values ?? Array.Empty<AP.CompletionValue>();
    }
}
