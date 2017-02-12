using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace Microsoft.PythonTools.TestAdapter {
    [FileExtension(".py")]
    [DefaultExecutorUri(PythonConstants.TestExecutorUriString)]
    class TestDiscoverer : ITestDiscoverer {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink) {
            ValidateArg.NotNull(sources, "sources");
            ValidateArg.NotNull(discoverySink, "discoverySink");

            var settings = discoveryContext.RunSettings;
            
            DiscoverTests(sources, logger, discoverySink, settings);
        }

        static void DiscoverTests(IEnumerable<string> sources, IMessageLogger logger, ITestCaseDiscoverySink discoverySink, IRunSettings settings) {
            var sourcesSet = new HashSet<string>(sources, StringComparer.OrdinalIgnoreCase);

            var executorUri = new Uri(PythonConstants.TestExecutorUriString);
            // Test list is sent to us via our run settings which we use to smuggle the
            // data we have in our analysis process.
            var doc = new XPathDocument(new StringReader(settings.SettingsXml));
            foreach (var t in TestReader.ReadTests(doc, sourcesSet, m => {
                logger?.SendMessage(TestMessageLevel.Warning, m);
            })) {
                var tc = new TestCase(t.FullyQualifiedName, executorUri, t.SourceFile) {
                    DisplayName = t.DisplayName,
                    LineNumber = t.LineNo,
                    CodeFilePath = t.FileName
                };

                discoverySink.SendTestCase(tc);
            }
        }
    }
}
