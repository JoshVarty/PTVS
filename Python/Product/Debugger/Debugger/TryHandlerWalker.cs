using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Debugger {
    /// <summary>
    /// Extracts a flat list of all the sections of code protected by exception
    /// handlers.
    /// </summary>
    class TryHandlerWalker : PythonWalker {
        private readonly List<TryStatement> _statements;

        public TryHandlerWalker() {
            _statements = new List<TryStatement>();
        }

        public ICollection<TryStatement> Statements {
            get {
                return _statements;
            }
        }

        public override bool Walk(TryStatement node) {
            _statements.Add(node);
            return base.Walk(node);
        }
    }
}
