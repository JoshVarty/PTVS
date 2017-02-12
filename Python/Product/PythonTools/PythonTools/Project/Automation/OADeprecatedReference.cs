using System.Runtime.InteropServices;
using Microsoft.VisualStudioTools.Project.Automation;
using VSLangProj;

namespace Microsoft.PythonTools.Project.Automation {
    [ComVisible(true)]
    public class OADeprecatedReference : OAReferenceBase {
        internal OADeprecatedReference(DeprecatedReferenceNode deprecatedReferenceNode) :
            base(deprecatedReferenceNode) {
        }

        private DeprecatedReferenceNode Node => (DeprecatedReferenceNode)BaseReferenceNode;

        public override string Name => Node.Caption;

        public override uint RefType => 0;

        public override prjReferenceType Type => prjReferenceType.prjReferenceTypeAssembly;

        public override bool CopyLocal {
            get {
                return false;
            }
            set { }
        }
    }
}
