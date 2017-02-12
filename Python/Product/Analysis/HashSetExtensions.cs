using System.Collections.Generic;

namespace Microsoft.PythonTools.Analysis {
    internal static class HashSetExtensions {
        internal static bool AddValue<T>(ref ISet<T> references, T value) {
            if (references == null) {
                references = new SetOfOne<T>(value);
                return true;
            } else if (references is SetOfOne<T>) {
                if (!references.Contains(value)) {
                    references = new SetOfTwo<T>(((SetOfOne<T>)references).Value, value);
                    return true;
                }
            } else if (references is SetOfTwo<T>) {
                if (!references.Contains(value)) {
                    references = new HashSet<T>(references);
                    return references.Add(value);
                }
            } else {
                return references.Add(value);
            }
            return false;
        }

    }
}
