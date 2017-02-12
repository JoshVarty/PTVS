using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PythonTools.Interpreter.Ast {
    class AstPythonFunctionOverload : IPythonFunctionOverload {
        private readonly IReadOnlyList<IParameterInfo> _parameters;

        public AstPythonFunctionOverload(
            string doc,
            string returnDoc,
            IEnumerable<IParameterInfo> parameters,
            IEnumerable<IPythonType> returns
        ) {
            Documentation = doc;
            ReturnDocumentation = returnDoc;
            _parameters = parameters.ToArray();
            ReturnType = returns.ToList();
        }

        public string Documentation { get; }
        public string ReturnDocumentation { get; }
        public IParameterInfo[] GetParameters() => _parameters.ToArray();
        public IList<IPythonType> ReturnType { get; }
    }
}
