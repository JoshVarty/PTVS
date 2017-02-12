using System;
using Microsoft.IronPythonTools.Interpreter;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Mocks;
using TestUtilities.Python;

namespace IronPythonTests {
    [TestClass]
    public class IronPythonDatabaseTest {
        [ClassInitialize]
        public static void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        [TestMethod, Priority(1)]
        public void InvalidIronPythonDatabase() {
            using (var db = MockCompletionDB.Create(PythonLanguageVersion.V27,
                // __bad_builtin__ is missing str
                Tuple.Create("__bad_builtin__", "__builtin__")
            )) {
                var ptd = db.Database;

                Assert.IsNotNull(ptd.GetModule("__builtin__"));

                var factory = new IronPythonInterpreterFactory(InterpreterArchitecture.x86);
                // Explicitly create an IronPythonInterpreter from factory that
                // will use the database in db.Factory.
                using (var analyzer = PythonAnalyzer.CreateSynchronously(factory, factory.MakeInterpreter(db.Factory))) {
                    // String type should have been loaded anyway
                    Assert.IsNotNull(analyzer.ClassInfos[BuiltinTypeId.Str]);
                }
            }
        }

    }
}
