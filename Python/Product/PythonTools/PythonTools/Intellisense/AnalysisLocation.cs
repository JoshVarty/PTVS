using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Represents a location that comes out of an analysis.  Includes a file, line, and column
    /// number.
    /// </summary>
    public sealed class AnalysisLocation : IEquatable<AnalysisLocation> {
        private readonly string _filePath;
        public readonly int Line, Column;
        private static readonly IEqualityComparer<AnalysisLocation> _fullComparer = new FullLocationComparer();

        internal AnalysisLocation(string filePath, int line, int column) {
            _filePath = filePath;
            Line = line;
            Column = column;
        }

        public string FilePath {
            get {
                return _filePath;
            }
        }

        internal void GotoSource(IServiceProvider serviceProvider) {
            if (File.Exists(_filePath)) {
                PythonToolsPackage.NavigateTo(
                    serviceProvider,
                    _filePath,
                    Guid.Empty,
                    Line - 1,
                    Column - 1
                );
            }
        }

        public override bool Equals(object obj) {
            AnalysisLocation other = obj as AnalysisLocation;
            if (other != null) {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() {
            return Line.GetHashCode() ^ _filePath.GetHashCode();
        }

        public bool Equals(AnalysisLocation other) {
            // currently we filter only to line & file - so we'll only show 1 ref per each line
            // This works nicely for get and call which can both add refs and when they're broken
            // apart you still see both refs, but when they're together you only see 1.
            return Line == other.Line &&
                String.Equals(_filePath, other._filePath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Provides an IEqualityComparer that compares line, column and project entries.  By
        /// default locations are equaitable based upon only line/project entry.
        /// </summary>
        public static IEqualityComparer<AnalysisLocation> FullComparer {
            get {
                return _fullComparer;
            }
        }

        sealed class FullLocationComparer : IEqualityComparer<AnalysisLocation> {
            public bool Equals(AnalysisLocation x, AnalysisLocation y) {
                return x.Line == y.Line &&
                    x.Column == y.Column &&
                    String.Equals(x._filePath, y._filePath, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(AnalysisLocation obj) {
                return obj.Line.GetHashCode() ^ obj.Column.GetHashCode() ^ obj._filePath.GetHashCode();
            }
        }
    }
}
