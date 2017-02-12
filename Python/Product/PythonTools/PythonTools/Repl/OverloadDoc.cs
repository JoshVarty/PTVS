using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Repl {
    class OverloadDoc {
        private readonly string _doc;
        private readonly ParameterResult[] _params;

        public OverloadDoc(string doc, ParameterResult[] parameters) {
            _doc = doc;
            _params = parameters;
        }

        public string Documentation { get { return _doc; } }

        public ParameterResult[] Parameters { get { return _params; } }
    }
}
