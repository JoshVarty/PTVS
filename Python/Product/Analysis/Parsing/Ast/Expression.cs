
namespace Microsoft.PythonTools.Parsing.Ast {
    public abstract class Expression : Node {
        internal Expression() {
        }

        internal virtual string CheckAssign() {
            return "can't assign to " + NodeName;
        }

        internal virtual string CheckAugmentedAssign() {
            if (CheckAssign() != null) {
                return "illegal expression for augmented assignment";
            }

            return null;
        }

        internal virtual string CheckDelete() {
            return "can't delete " + NodeName;
        }
    }
}
