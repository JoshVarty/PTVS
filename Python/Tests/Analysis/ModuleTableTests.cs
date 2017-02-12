extern alias analysis;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Python;
using analysis::Microsoft.PythonTools.Interpreter;

namespace AnalysisTests {
    [TestClass]
    public class ModuleTableTests {
        class MockPythonModule : IPythonModule {
            private readonly string _name;

            public MockPythonModule(string name) {
                _name = name;
            }

            public string Documentation => "";
            public PythonMemberType MemberType => PythonMemberType.Module;
            public string Name => _name;
            public IEnumerable<string> GetChildrenModules() => Enumerable.Empty<string>();
            public IMember GetMember(IModuleContext context, string name) => null;
            public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) => Enumerable.Empty<string>();
            public void Imported(IModuleContext context) { }
        }

        [TestMethod]
        public void RemovedModule() {
            var id = Guid.NewGuid().ToString();
            var config = new InterpreterConfiguration(id, id, version: new Version(3, 5));
            var fact = new MockPythonInterpreterFactory(config);
            var interp = new MockPythonInterpreter(fact);
            var modules = new ModuleTable(null, interp);

            var orig = modules.Select(kv => kv.Key).ToSet();

            interp.AddModule("test", new MockPythonModule("test"));

            ModuleReference modref;
            Assert.IsTrue(modules.TryImport("test", out modref));

            interp.RemoveModule("test", retainName: true);

            modules.ReInit();
            Assert.IsFalse(modules.TryImport("test", out modref));
        }
    }
}
