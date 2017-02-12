using System.Collections.Generic;
using Microsoft.PythonTools.Analysis.Analyzer;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    interface IModule {
        IModule GetChildPackage(IModuleContext context, string name);
        IEnumerable<KeyValuePair<string, AnalysisValue>> GetChildrenPackages(IModuleContext context);

        void SpecializeFunction(string name, CallDelegate callable, bool mergeOriginalAnalysis);

        IDictionary<string, IAnalysisSet> GetAllMembers(IModuleContext context, GetMemberOptions options = GetMemberOptions.None);
        IEnumerable<string> GetModuleMemberNames(IModuleContext context);
        IAnalysisSet GetModuleMember(Node node, AnalysisUnit unit, string name, bool addRef = true, InterpreterScope linkedScope = null, string linkedName = null);
        void Imported(AnalysisUnit unit);
    }
}
