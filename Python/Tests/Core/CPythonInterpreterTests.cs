using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.Parsing.Ast;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using TestUtilities;
using TestUtilities.Python;

namespace PythonToolsTests {
    [TestClass]
    public class CPythonInterpreterTests {
        internal static readonly CPythonInterpreterFactoryProvider InterpFactory = new CPythonInterpreterFactoryProvider(false);

        [ClassInitialize]
        public static void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        [TestMethod, Priority(0)]
        public void FactoryProvider() {
            var provider = InterpFactory;
            var factories = provider.GetInterpreterFactories().ToArray();

            Console.WriteLine("Discovered:");
            foreach (var factory in factories) {
                var id = factory.Configuration.Id;
                Console.WriteLine("  {0} - {1}".FormatInvariant(id, factory.Configuration.Description));


                Assert.IsTrue(id.StartsWith("Global|"), "Expected 'Global' prefix on '{0}'".FormatInvariant(factory.Configuration.Id));

                Assert.IsNotNull(factory.CreateInterpreter(), "failed to create interpreter");

                if (id.StartsWith("Global|PythonCore|")) {
                    var description = factory.Configuration.Description;
                    var sysVersion = factory.Configuration.Version;
                    var sysArch = factory.Configuration.Architecture;

                    AssertUtil.Contains(description, "Python", sysVersion.ToString(), sysArch.ToString());

                    Assert.IsTrue(sysVersion.Major == 2 || sysVersion.Major == 3, "unknown SysVersion '{0}'".FormatInvariant(sysVersion));

                    Assert.AreEqual(PythonRegistrySearch.PythonCoreCompanyDisplayName, provider.GetProperty(id, PythonRegistrySearch.CompanyPropertyKey));
                    Assert.AreEqual(PythonRegistrySearch.PythonCoreSupportUrl, provider.GetProperty(id, PythonRegistrySearch.SupportUrlPropertyKey));
                }
            }
        }

        [TestMethod, Priority(1)]
        public void DiscoverRegistryRace() {
            // https://github.com/Microsoft/PTVS/issues/558

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Python\PythonCore")) {
                for (int changes = 0; changes < 1000; ++changes) {
                    // Doesn't matter about the name - we just want to trigger
                    // discovery and then remove the key during GetSubKeyNames.
                    key.CreateSubKey("NotARealInterpreter").Close();
                    key.DeleteSubKey("NotARealInterpreter", false);
                }
            }
        }

        [TestMethod, Priority(0)]
        public void ImportFromSearchPath() {
            var analyzer = new PythonAnalysis(PythonLanguageVersion.V35);
            analyzer.AddModule("test-module", "from test_package import *");
            analyzer.WaitForAnalysis();
            AssertUtil.CheckCollection(analyzer.GetAllNames(), null, new[] { "package_method", "package_method_two", "test_package" });

            analyzer.SetSearchPaths(TestData.GetPath("TestData\\AddImport"));
            analyzer.ReanalyzeAll();

            AssertUtil.CheckCollection(analyzer.GetAllNames(), new[] { "package_method", "package_method_two" }, new[] { "test_package" });
        }

        [TestMethod, Priority(0)]
        public void ImportPydFromSearchPath() {
            PythonTypeDatabase.ExtensionModuleLoader.AlwaysGenerateDb = true;
            try {
                var analyzer = new PythonAnalysis("Global|PythonCore|2.7-32");

                analyzer.AddModule("test-module", "from spam import *");
                analyzer.WaitForAnalysis();
                AssertUtil.CheckCollection(analyzer.GetAllNames(), null, new[] { "system", "spam" });

                analyzer.SetSearchPaths(TestData.GetPath("TestData"));
                analyzer.ReanalyzeAll(CancellationTokens.After60s);

                AssertUtil.CheckCollection(analyzer.GetAllNames(), new[] { "system" }, new[] { "spam" });
            } finally {
                PythonTypeDatabase.ExtensionModuleLoader.AlwaysGenerateDb = false;
            }
        }

        [TestMethod, Priority(0)]
        public void ImportFromZipFile() {
            var analyzer = new PythonAnalysis(PythonLanguageVersion.V35);
            analyzer.AddModule("test-module", "from test_package import *");
            analyzer.WaitForAnalysis();
            AssertUtil.CheckCollection(analyzer.GetAllNames(), null, new[] { "package_method", "package_method_two", "test_package" });

            analyzer.SetSearchPaths(TestData.GetPath("TestData\\AddImport.zip"));
            analyzer.ReanalyzeAll();

            AssertUtil.CheckCollection(analyzer.GetAllNames(), new[] { "package_method", "package_method_two" }, new[] { "test_package" });
        }

        private static void AnalyzeCode(
            out PythonAnalyzer analyzer,
            out IPythonProjectEntry entry,
            string code,
            Version preferredVersion = null,
            InterpreterArchitecture preferredArch = null,
            string module = "test-module"
        ) {
            var provider = InterpFactory;
            var factory = provider.GetInterpreterFactories().OrderByDescending(f => f.Configuration.Version)
                .Where(f => preferredVersion == null || f.Configuration.Version == preferredVersion)
                .Where(f => preferredArch == null || f.Configuration.Architecture == preferredArch)
                .FirstOrDefault();
            Assert.IsNotNull(factory, "no factory found");

            analyzer = PythonAnalyzer.CreateSynchronously(factory);
            var path = Path.Combine(TestData.GetTempPath(randomSubPath: true), module.Replace('.', '\\'));
            Directory.CreateDirectory(PathUtils.GetParent(path));
            File.WriteAllText(path, code);

            entry = analyzer.AddModule(module, path);
            PythonAst ast;
            using (var p = Parser.CreateParser(new StringReader(code), factory.GetLanguageVersion())) {
                ast = p.ParseFile();
                entry.UpdateTree(ast, null);
            }

            entry.Analyze(CancellationToken.None, true);
        }
    }
}
