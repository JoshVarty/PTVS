using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.PythonTools.CodeCoverage {
    /// <summary>
    /// Loads code coverage data from the XML format saved by coverage.py.
    /// </summary>
    class CoveragePyConverter {
        private readonly Stream _input;
        private readonly string _coverageXmlBasePath;

        public CoveragePyConverter(string baseDir, Stream input) {
            _coverageXmlBasePath = baseDir;
            _input = input;
        }

        public CoverageFileInfo[] Parse() {
            XmlDocument doc = new XmlDocument();
            doc.Load(_input);
            Dictionary<string, HashSet<int>> data = new Dictionary<string, HashSet<int>>();

            var root = doc.DocumentElement.CreateNavigator();
            string basePath = "";
            foreach (XPathNavigator source in root.Select("/coverage/sources/source")) {
                basePath = source.Value;
            }

            foreach (XPathNavigator node in root.Select("/coverage/packages/package/classes/class/lines/line")) {
                var hits = node.GetAttribute("hits", "");
                var number = node.GetAttribute("number", "");

                int hitsNo, lineNo;
                if (Int32.TryParse(hits, out hitsNo) &&
                    hitsNo != 0 &&
                    Int32.TryParse(number, out lineNo) &&
                    node.MoveToParent() &&
                    node.MoveToParent()) {

                    var filename = GetFilename(basePath, node);

                    HashSet<int> lineHits;
                    if (!data.TryGetValue(filename, out lineHits)) {
                        data[filename] = lineHits = new HashSet<int>();
                    }

                    lineHits.Add(lineNo);
                }
            }

            return data.Select(x => new CoverageFileInfo(x.Key, x.Value))
                .ToArray();
        }

        private string GetFilename(string basePath, XPathNavigator node) {
            // Try and find the source relative to the coverage file first...
            var filename = node.GetAttribute("filename", "");
            string relativePath = Path.Combine(_coverageXmlBasePath, filename);
            if (File.Exists(relativePath)) {
                return relativePath;
            }

            // Then try the absolute path.
            return Path.Combine(basePath, filename);
        }
    }
}
