using System.Runtime.Remoting;
using Microsoft.PythonTools.Parsing;
using Microsoft.IronPythonTools.Interpreter;
using Microsoft.PythonTools.Interpreter;
using TestUtilities.Python;

namespace IronPythonTests {
    class IronPythonAnalysis : PythonAnalysis {
        public IronPythonAnalysis(PythonLanguageVersion version) : base(version) { }

        public IronPythonAnalysis(string idOrDescription) : base(idOrDescription) { }

        public IronPythonAnalysis(
            IPythonInterpreterFactory factory,
            IPythonInterpreter interpreter = null,
            string builtinName = null
        ) : base(factory, interpreter, builtinName) {
            ((IronPythonInterpreter)Analyzer.Interpreter).Remote.AddAssembly(new ObjectHandle(typeof(IronPythonAnalysisTest).Assembly));
        }

        public override BuiltinTypeId BuiltinTypeId_Str => BuiltinTypeId.Unicode;

        // IronPython does not distinguish between string iterators, and
        // since BytesIterator < UnicodeIterator, it is the one returned
        // for iter("").
        public override BuiltinTypeId BuiltinTypeId_StrIterator => BuiltinTypeId.UnicodeIterator;
    }
}
