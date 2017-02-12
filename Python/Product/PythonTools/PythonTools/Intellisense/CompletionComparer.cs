using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;
using Microsoft.VisualStudio.Language.Intellisense;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Compares various types of completions.
    /// </summary>
    public class CompletionComparer : IEqualityComparer<CompletionResult>, IComparer<CompletionResult>, IComparer<Completion>, IComparer<string> {
        /// <summary>
        /// A CompletionComparer that sorts names beginning with underscores to
        /// the end of the list.
        /// </summary>
        public static readonly CompletionComparer UnderscoresLast = new CompletionComparer(true);
        /// <summary>
        /// A CompletionComparer that determines whether
        /// <see cref="MemberResult" /> structures are equal.
        /// </summary>
        public static readonly IEqualityComparer<CompletionResult> MemberEquality = UnderscoresLast;
        /// <summary>
        /// A CompletionComparer that sorts names beginning with underscores to
        /// the start of the list.
        /// </summary>
        public static readonly CompletionComparer UnderscoresFirst = new CompletionComparer(false);

        bool _sortUnderscoresLast;

        /// <summary>
        /// Compares two strings.
        /// </summary>
        public int Compare(string xName, string yName) {
            if (yName == null) {
                return xName == null ? 0 : -1;
            } else if (xName == null) {
                return yName == null ? 0 : 1;
            }

            if (_sortUnderscoresLast) {
                bool xUnder = xName.StartsWith("__") && xName.EndsWith("__");
                bool yUnder = yName.StartsWith("__") && yName.EndsWith("__");

                if (xUnder != yUnder) {
                    // The one that starts with an underscore comes later
                    return xUnder ? 1 : -1;
                }

                bool xSingleUnder = xName.StartsWith("_");
                bool ySingleUnder = yName.StartsWith("_");
                if (xSingleUnder != ySingleUnder) {
                    // The one that starts with an underscore comes later
                    return xSingleUnder ? 1 : -1;
                }
            }
            return String.Compare(xName, yName, StringComparison.CurrentCultureIgnoreCase);
        }

        private CompletionComparer(bool sortUnderscoresLast) {
            _sortUnderscoresLast = sortUnderscoresLast;
        }

        /// <summary>
        /// Compares two instances of <see cref="Completion"/> using their
        /// displayed text.
        /// </summary>
        public int Compare(Completion x, Completion y) {
            return Compare(x.DisplayText, y.DisplayText);
        }

        /// <summary>
        /// Compares two <see cref="MemberResult"/> structures using their
        /// names.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(CompletionResult x, CompletionResult y) {
            return Compare(x.Name, y.Name);
        }

        /// <summary>
        /// Compares two <see cref="MemberResult"/> structures for equality.
        /// </summary>
        public bool Equals(CompletionResult x, CompletionResult y) {
            return x.Name.Equals(y.Name);
        }

        /// <summary>
        /// Gets the hash code for a <see cref="MemberResult"/> structure.
        /// </summary>
        public int GetHashCode(CompletionResult obj) {
            return obj.Name.GetHashCode();
        }
    }
}
