using System.Collections.Generic;

namespace Microsoft.PythonTools.Parsing.Ast {
    public abstract class SequenceExpression : Expression {
        private readonly Expression[] _items;

        protected SequenceExpression(Expression[] items) {
            _items = items;
        }

        public IList<Expression> Items {
            get { return _items; }
        }

        internal override string CheckAssign() {
            for (int i = 0; i < Items.Count; i++) {
                Expression e = Items[i];
                if (e.CheckAssign() != null) {
                    // we don't return the same message here as CPython doesn't seem to either, 
                    // for example ((yield a), 2,3) = (2,3,4) gives a different error than
                    // a = yield 3 = yield 4.
                    return "can't assign to " + e.NodeName;
                }
            }
            return null;

        }

        internal override string CheckDelete() {
            for (int i = 0; i < Items.Count; i++) {
                Expression e = Items[i];
                if (e.CheckDelete() != null) {
                    // we don't return the same message here as CPython doesn't seem to either, 
                    // for example ((yield a), 2,3) = (2,3,4) gives a different error than
                    // a = yield 3 = yield 4.
                    return "can't delete " + e.NodeName;
                }
            }
            return null;
        }

        internal override string CheckAugmentedAssign() {
            return "illegal expression for augmented assignment";
        }

        private static bool IsComplexAssignment(Expression expr) {
            return !(expr is NameExpression);
        }
    }
}
