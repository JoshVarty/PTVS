using System.Collections.Generic;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Walks the AST, and indicates if execution of the node has side effects
    /// </summary>
    internal class DetectSideEffectsWalker : PythonWalker {
        public bool HasSideEffects { get; private set; }

        public override bool Walk(AwaitExpression node) {
            HasSideEffects = true;
            return false;
        }

        public override bool Walk(CallExpression node) {
            HasSideEffects = true;
            return false;
        }

        public override bool Walk(BackQuoteExpression node) {
            HasSideEffects = true;
            return false;
        }

        public override bool Walk(ErrorExpression node) {
            HasSideEffects = true;
            return false;
        }

        public override bool Walk(YieldExpression node) {
            HasSideEffects = true;
            return false;
        }

        public override bool Walk(YieldFromExpression node) {
            HasSideEffects = true;
            return false;
        }

        private static readonly HashSet<string> allowedCalls = new HashSet<string> {
                "abs", "bool", "callable", "chr", "cmp", "complex", "divmod", "float", "format",
                "getattr", "hasattr", "hash", "hex", "id", "int", "isinstance", "issubclass",
                "len", "max", "min", "oct", "ord", "pow", "repr", "round", "str", "tuple", "type"
            };

        public static bool IsSideEffectFreeCall(string name) {
            return allowedCalls.Contains(name);
        }
    }
}
