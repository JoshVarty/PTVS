using System.Collections.Generic;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis {
    class EmptyBuiltinModule : IBuiltinPythonModule {
        private readonly string _name;

        public EmptyBuiltinModule(string name) {
            _name = name;
        }

        #region IBuiltinPythonModule Members

        public IMember GetAnyMember(string name) {
            return null;
        }

        #endregion

        #region IPythonModule Members

        public string Name {
            get { return _name; }
        }

        public IEnumerable<string> GetChildrenModules() {
            yield break;
        }

        public void Imported(IModuleContext context) {
        }

        public string Documentation {
            get { return string.Empty; }
        }

        #endregion

        #region IMemberContainer Members

        public IMember GetMember(IModuleContext context, string name) {
            return null;
        }

        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) {
            yield break;
        }

        #endregion

        #region IMember Members

        public PythonMemberType MemberType {
            get { return PythonMemberType.Module; }
        }

        #endregion

    }
}
