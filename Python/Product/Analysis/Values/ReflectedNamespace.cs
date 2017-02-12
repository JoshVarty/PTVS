using System.Collections.Generic;
using Microsoft.PythonTools.Analysis.Analyzer;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    /// <summary>
    /// Represents a .NET namespace as exposed to Python
    /// </summary>
    internal class ReflectedNamespace : BuiltinNamespace<IMemberContainer>, IReferenceableContainer {
        private readonly MemberReferences _references = new MemberReferences();
        private readonly IMemberContainer _container;

        public ReflectedNamespace(IMemberContainer member, PythonAnalyzer projectState)
            : base(member, projectState) {
            _container = member;
        }

        public override IAnalysisSet GetMember(Node node, AnalysisUnit unit, string name) {
            // Must unconditionally call the base implementation of GetMember
            var res = base.GetMember(node, unit, name);
            if (res.Count > 0) {
                _references.AddReference(node, unit, name);
            }
            return res;
        }

        public override IDictionary<string, IAnalysisSet> GetAllMembers(IModuleContext moduleContext, GetMemberOptions options = GetMemberOptions.None) {
            return ProjectState.GetAllMembers(_container, moduleContext);
        }

        public override PythonMemberType MemberType {
            get {
                if (_container is IMember) {
                    return ((IMember)_container).MemberType;
                }
                return PythonMemberType.Namespace;
            }
        }

        #region IReferenceableContainer Members

        public IEnumerable<IReferenceable> GetDefinitions(string name) {
            return _references.GetDefinitions(name, _container, ProjectState._defaultContext);
        }

        #endregion
    }
}
