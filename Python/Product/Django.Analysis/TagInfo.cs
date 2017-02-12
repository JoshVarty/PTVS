using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Django.Analysis {

    class TagInfo {
        public readonly string Documentation;
        public readonly IPythonProjectEntry Entry;
        public TagInfo(string doc, IPythonProjectEntry entry) {
            Documentation = doc;
            Entry = entry;
        }

    }

}
