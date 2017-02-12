using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudioTools.Parsing;
using Microsoft.VisualStudioTools.Project;
using ErrorHandler = Microsoft.VisualStudio.ErrorHandler;
using VSConstants = Microsoft.VisualStudio.VSConstants;

namespace Microsoft.VisualStudioTools.Navigation {

    /// <summary>
    /// This is a specialized version of the LibraryNode that handles the dynamic languages
    /// items. The main difference from the generic one is that it supports navigation
    /// to the location inside the source code where the element is defined.
    /// </summary>
    internal abstract class CommonLibraryNode : LibraryNode {
        private readonly IVsHierarchy _ownerHierarchy;
        private readonly uint _fileId;
        private readonly TextSpan _sourceSpan;
        private string _fileMoniker;

        protected CommonLibraryNode(LibraryNode parent, string name, string fullName, IVsHierarchy hierarchy, uint itemId, LibraryNodeType type, IList<LibraryNode> children = null) :
            base(parent, name, fullName, type, children: children) {
            _ownerHierarchy = hierarchy;
            _fileId = itemId;
        }
      
        public TextSpan SourceSpan {
            get {
                return _sourceSpan;
            }
        }

        protected CommonLibraryNode(CommonLibraryNode node) :
            base(node) {
            _fileId = node._fileId;
            _ownerHierarchy = node._ownerHierarchy;
            _fileMoniker = node._fileMoniker;
            _sourceSpan = node._sourceSpan;
        }

        protected CommonLibraryNode(CommonLibraryNode node, string newFullName) :
            base(node, newFullName) {
            _fileId = node._fileId;
            _ownerHierarchy = node._ownerHierarchy;
            _fileMoniker = node._fileMoniker;
            _sourceSpan = node._sourceSpan;
        }

        public override uint CategoryField(LIB_CATEGORY category) {
            switch (category) {
                case (LIB_CATEGORY)_LIB_CATEGORY2.LC_MEMBERINHERITANCE:
                    if (NodeType == LibraryNodeType.Members || NodeType == LibraryNodeType.Definitions) {
                        return (uint)_LIBCAT_MEMBERINHERITANCE.LCMI_IMMEDIATE;
                    }
                    break;
            }
            return base.CategoryField(category);
        }

        public override void SourceItems(out IVsHierarchy hierarchy, out uint itemId, out uint itemsCount) {
            hierarchy = _ownerHierarchy;
            itemId = _fileId;
            itemsCount = 1;
        }

        public override string UniqueName {
            get {
                if (string.IsNullOrEmpty(_fileMoniker)) {
                    ErrorHandler.ThrowOnFailure(_ownerHierarchy.GetCanonicalName(_fileId, out _fileMoniker));
                }
                return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", _fileMoniker, Name);
            }
        }

        public IServiceProvider Site {
            get {
                return (_ownerHierarchy as ProjectNode).Site;
            }
        }

        public ProjectNode Hierarchy {
            get {
                return _ownerHierarchy as ProjectNode;
            }
        }
    }
}
