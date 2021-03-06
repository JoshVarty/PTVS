using System;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.VisualStudioTools.Navigation {
    class ProjectLibraryNode : LibraryNode {
        private readonly CommonProjectNode _project;

        public ProjectLibraryNode(CommonProjectNode project)
            : base(null, project.Caption, project.Caption, LibraryNodeType.PhysicalContainer) {
            _project = project;
        }

        public override uint CategoryField(LIB_CATEGORY category) {
            switch (category) {
                case LIB_CATEGORY.LC_NODETYPE:
                    return (uint)_LIBCAT_NODETYPE.LCNT_PROJECT;
            }
            return base.CategoryField(category);
        }

        public override VSTREEDISPLAYDATA DisplayData {
            get {
                var res = new VSTREEDISPLAYDATA();
#if DEV14_OR_LATER
                // Use the default Reference icon for projects
                res.hImageList = IntPtr.Zero;
                res.Image = res.SelectedImage = 192;
#else
                res.hImageList = _project.ImageHandler.ImageList.Handle;
                res.Image = res.SelectedImage = (ushort)_project.ImageIndex;
#endif
                return res;
            }
        }

        public override StandardGlyphGroup GlyphType {
            get {
                return StandardGlyphGroup.GlyphCoolProject;
            }
        }
    }
}
