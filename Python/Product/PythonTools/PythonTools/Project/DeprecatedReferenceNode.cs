using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    sealed class DeprecatedReferenceNode : ReferenceNode {
        private readonly string _caption, _message;

        public DeprecatedReferenceNode(ProjectNode root, string name, string message) : base(root) {
            _caption = name;
            _message = message;
        }

        public DeprecatedReferenceNode(ProjectNode root, ProjectElement element, string name, string message) : base(root, element) {
            _caption = name;
            _message = message;
        }

        public override string Caption => _caption;
        public string Message => _message;
        protected override ImageMoniker GetIconMoniker(bool open) => KnownMonikers.ReferenceWarning;
        protected override NodeProperties CreatePropertiesObject() => new DeprecatedReferenceNodeProperties(this);

        protected override void BindReferenceData() { }
    }

    [ComVisible(true)]
    public sealed class DeprecatedReferenceNodeProperties : NodeProperties {
        internal DeprecatedReferenceNodeProperties(DeprecatedReferenceNode node) : base(node) {
        }

        public override string GetClassName() => SR.GetString(SR.ReferenceProperties);

        private new DeprecatedReferenceNode Node => (DeprecatedReferenceNode)HierarchyNode;

        [Browsable(true)]
        [SRCategory(SR.Misc)]
        [SRDisplayName(SR.RefName)]
        [SRDescription(SR.RefNameDescription)]
        [AutomationBrowsable(true)]
        public override string Name => Node.Caption;


        [Browsable(true)]
        [SRCategory(SR.Misc)]
        [SRDisplayName("RefDeprecatedMessage")]
        [SRDescription("RefDeprecatedMessageDescription")]
        [AutomationBrowsable(true)]
        public string Message => Node.Message;
    }
}
