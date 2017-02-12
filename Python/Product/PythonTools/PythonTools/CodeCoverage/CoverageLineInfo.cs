using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.CodeCoverage {
    class CoverageLineInfo {
        public bool Covered;
        public int ColumnStart = int.MaxValue, ColumnEnd = int.MinValue;
    }
}
