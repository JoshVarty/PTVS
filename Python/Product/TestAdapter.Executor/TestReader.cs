using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.XPath;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.TestAdapter {
    static class TestReader {
        public struct TestCase {
            public string DisplayName;
            public string FullyQualifiedName;
            public string FileName;
            public string SourceFile;
            public int LineNo;
        }

        public static IEnumerable<TestCase> ReadTests(
            XPathDocument doc,
            HashSet<string> validSources,
            Action<string> warning
        ) {
            XPathNodeIterator nodes = doc.CreateNavigator().Select("/RunSettings/Python/TestCases/Project/Test");
            foreach (XPathNavigator test in nodes) {
                var className = test.GetAttribute("className", "");
                var file = test.GetAttribute("file", "");

                if (!validSources.Contains(file)) {
                    continue;
                }

                var line = test.GetAttribute("line", "");
                var column = test.GetAttribute("column", "");
                var methodName = test.GetAttribute("method", "");
                string projectHome = null;
                if (!test.MoveToParent()) {
                    continue;
                }

                projectHome = test.GetAttribute("home", "");

                int lineNo, columnNo;
                if (Int32.TryParse(line, out lineNo) &&
                    Int32.TryParse(column, out columnNo) &&
                    !String.IsNullOrWhiteSpace(className) &&
                    !String.IsNullOrWhiteSpace(methodName) &&
                    !String.IsNullOrWhiteSpace(file)) {
                    var moduleName = PathUtils.CreateFriendlyFilePath(projectHome, file);
                    var fullyQualifiedName = MakeFullyQualifiedTestName(moduleName, className, methodName);

                    // If this is a runTest test we should provide a useful display name
                    var displayName = methodName == "runTest" ? className : methodName;

                    yield return new TestCase {
                        FileName = PathUtils.GetAbsoluteFilePath(projectHome, file),
                        DisplayName = displayName,
                        LineNo = lineNo,
                        FullyQualifiedName = fullyQualifiedName,
                        SourceFile = file
                    };
                } else {
                    warning?.Invoke("Bad test case: {0} {1} {2} {3} {4}".FormatUI(
                        className,
                        methodName,
                        file,
                        line,
                        column
                    ));
                }
            }
        }

        public static string MakeFullyQualifiedTestName(string modulePath, string className, string methodName) {
            return modulePath + "::" + className + "::" + methodName;
        }

        internal static void ParseFullyQualifiedTestName(
            string fullyQualifiedName,
            out string modulePath,
            out string className,
            out string methodName
        ) {
            string[] parts = fullyQualifiedName.Split(new string[] { "::" }, StringSplitOptions.None);
            Debug.Assert(parts.Length == 3);
            modulePath = parts[0];
            className = parts[1];
            methodName = parts[2];
        }
    }
}
