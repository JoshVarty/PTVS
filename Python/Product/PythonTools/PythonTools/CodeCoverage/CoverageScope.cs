using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.CodeCoverage {
    /// <summary>
    /// Tracks data about coverage for each scope.
    /// </summary>

    sealed class CoverageScope {
        public readonly ScopeStatement Statement;
        public readonly List<CoverageScope> Children = new List<CoverageScope>();
        public readonly SortedDictionary<int, CoverageLineInfo> Lines = new SortedDictionary<int, CoverageLineInfo>();
        public int BlocksCovered, BlocksNotCovered;

        public CoverageScope(ScopeStatement node) {
            Statement = node;
        }

        public int LinesCovered {
            get {
                return Lines.Select(x => x.Value).Where(x => x.Covered).Count();
            }
        }

        public int LinesNotCovered {
            get {
                return Lines.Select(x => x.Value).Where(x => !x.Covered).Count();
            }
        }
    }
}
