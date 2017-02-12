using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Project {
    [ContentType(PythonCoreConstants.ContentType)]
    [Export(typeof(IEncodingDetector))]
    [Order(Before = "XmlEncodingDetector")]
    [Name("PythonEncodingDetector")]
    class PythonEncodingDetector : IEncodingDetector {
        public Encoding GetStreamEncoding(Stream stream) {
            var res = Parser.GetEncodingFromStream(stream) ?? Parser.DefaultEncodingNoFallback;
            if (res == Parser.DefaultEncoding) {
                // return a version of the fallback buffer that doesn't throw exceptions, VS will detect the failure, and inform
                // the user of the problem.
                return Parser.DefaultEncodingNoFallback;
            }
            return res;
        }
    }
}
