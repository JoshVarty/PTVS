using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Analysis {
    public class LocationInfo : IEquatable<LocationInfo>, ILocationResolver {
        private readonly int _line, _column;
        private readonly int? _endLine, _endColumn;
        private readonly string _path;
        internal static LocationInfo[] Empty = new LocationInfo[0];

        private static readonly IEqualityComparer<LocationInfo> _fullComparer = new FullLocationComparer();

        internal LocationInfo(string path, int line, int column) {
            _path = path;
            _line = line;
            _column = column;
        }

        internal LocationInfo(string path, int line, int column, int? endLine, int? endColumn) {
            _path = path;
            _line = line;
            _column = column;
            _endLine = endLine;
            _endColumn = endColumn;
        }

        public string FilePath {
            get { return _path; }
        }

        public int StartLine {
            get { return _line; }
        }

        public int StartColumn {
            get {
                return _column;
            }
        }

        public int? EndLine => _endLine;

        public int? EndColumn => _endColumn;

        public override bool Equals(object obj) {
            LocationInfo other = obj as LocationInfo;
            if (other != null) {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() {
            return StartLine.GetHashCode() ^ FilePath.GetHashCode();
        }

        public bool Equals(LocationInfo other) {
            // currently we filter only to line & file - so we'll only show 1 ref per each line
            // This works nicely for get and call which can both add refs and when they're broken
            // apart you still see both refs, but when they're together you only see 1.
            return StartLine == other.StartLine &&
                FilePath == other.FilePath;
        }

        /// <summary>
        /// Provides an IEqualityComparer that compares line, column and project entries.  By
        /// default locations are equaitable based upon only line/project entry.
        /// </summary>
        public static IEqualityComparer<LocationInfo> FullComparer {
            get{
                return _fullComparer;
            }
        }

        sealed class FullLocationComparer : IEqualityComparer<LocationInfo> {
            public bool Equals(LocationInfo x, LocationInfo y) {
                return x.StartLine == y.StartLine &&
                    x.StartColumn == y.StartColumn &&
                    x.FilePath == y.FilePath &&
                    x.EndLine == y.EndLine &&
                    x.EndColumn == x.EndColumn;
            }

            public int GetHashCode(LocationInfo obj) {
                return obj.StartLine.GetHashCode() ^ obj.StartColumn.GetHashCode() ^ obj.FilePath.GetHashCode();
            }
        }

        #region ILocationResolver Members

        LocationInfo ILocationResolver.ResolveLocation(object location) {
            return this;
        }

        #endregion
    }
}
