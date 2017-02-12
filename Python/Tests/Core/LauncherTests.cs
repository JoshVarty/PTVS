using System;
using System.Collections.Generic;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Project.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PythonToolsTests {
    [TestClass]
    public class LauncherTests {
        [TestMethod, Priority(1)]
        public void LaunchWebBrowserUriTests() {
            var testCases = new[] {
                new { Url = "/fob", Port = 1, Expected = "http://localhost:1/fob" },
                new { Url = "http://localhost:9999/fob", Port = 9999, Expected = "http://localhost:9999/fob" },
                new { Url = "http://localhost/fob", Port = 9999, Expected = "http://localhost:9999/fob" },
                new { Url = "fob", Port = 9999, Expected = "http://localhost:9999/fob" },
                new { Url = "/hello/world", Port = 367, Expected = "http://localhost:367/hello/world" },
                new { Url = "/fob", Port = -1, Expected = "http://localhost:{port}/fob" },
            };

            foreach (var testCase in testCases) {
                Console.WriteLine("{0} {1} == {2}", testCase.Url, testCase.Port, testCase.Expected);

                Uri url;
                int port;

                var config = new LaunchConfiguration(null, new Dictionary<string, string> {
                    { PythonConstants.WebBrowserUrlSetting, testCase.Url }
                });
                if (testCase.Port >= 0) {
                    config.LaunchOptions[PythonConstants.WebBrowserPortSetting] = testCase.Port.ToString();
                }
                PythonWebLauncher.GetFullUrl(null, config, out url, out port);
                Assert.AreEqual(
                    testCase.Expected.Replace("{port}", port.ToString()),
                    url.AbsoluteUri
                );
            }
        }
    }
}
