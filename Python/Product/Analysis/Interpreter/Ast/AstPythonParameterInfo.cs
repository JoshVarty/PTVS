using System.Collections.Generic;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Interpreter.Ast {
    class AstPythonParameterInfo : IParameterInfo {
        public AstPythonParameterInfo(PythonAst ast, Parameter p) {
            Name = p.Name;
            Documentation = "";
            DefaultValue = p.DefaultValue?.ToCodeString(ast) ?? "";
            IsParamArray = p.Kind == ParameterKind.List;
            IsKeywordDict = p.Kind == ParameterKind.Dictionary;
            ParameterTypes = new IPythonType[0];
        }

        public string Name { get; }
        public string Documentation { get; }
        public string DefaultValue { get; }
        public bool IsParamArray { get; }
        public bool IsKeywordDict { get; }
        public IList<IPythonType> ParameterTypes { get; }
    }
}
