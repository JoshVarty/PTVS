using System.Collections.Generic;

namespace Microsoft.PythonTools.TestAdapter {
    internal sealed class TestCaseInfo {
        private readonly string _filename;
        private readonly string _method;
        private readonly string _class;
        private readonly int _startLine, _startColumn, _endLine;

        public TestCaseInfo(string filename, string className, string methodName, int startLine, int startColumn, int endLine) {
            _filename = filename;
            _class = className;
            _method = methodName;
            _startLine = startLine;
            _startColumn = startColumn;
            _endLine = endLine;
        }

        public string Filename => _filename;
        public string MethodName => _method;
        public int StartLine => _startLine;
        public int EndLine => _endLine;
        public int StartColumn => _startColumn;
        public string ClassName => _class;

        public TestCaseKind Kind {
            get {
                // Currently we don't support other test case kinds
                return TestCaseKind.UnitTest;
            }
        }

        public Dictionary<string, object> AsDictionary() {
            return new Dictionary<string, object>() {
                { TestAnalyzer.Serialize.Filename, Filename },
                { TestAnalyzer.Serialize.ClassName, ClassName },
                { TestAnalyzer.Serialize.MethodName, MethodName },
                { TestAnalyzer.Serialize.StartLine, StartLine},
                { TestAnalyzer.Serialize.StartColumn, StartColumn},
                { TestAnalyzer.Serialize.EndLine, EndLine },
                { TestAnalyzer.Serialize.Kind, Kind.ToString() },
            };
        }
    }

    internal enum TestCaseKind {
        None,
        UnitTest
    }
}
