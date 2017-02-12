using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    class PythonReferenceContainerNode : CommonReferenceContainerNode {
        public PythonReferenceContainerNode(PythonProjectNode root)
            : base(root) {
        }

        protected override ProjectReferenceNode CreateProjectReferenceNode(ProjectElement element) {
            return PythonProjectReferenceNode.Create(ProjectMgr, element);
        }

        protected override ProjectReferenceNode CreateProjectReferenceNode(VSCOMPONENTSELECTORDATA selectorData) {
            return PythonProjectReferenceNode.Create(ProjectMgr, selectorData.bstrTitle, selectorData.bstrFile, selectorData.bstrProjRef);
        }

        protected override AssemblyReferenceNode CreateAssemblyReferenceNode(ProjectElement element) {
            AssemblyReferenceNode node = null;
            try {
                node = new PythonAssemblyReferenceNode((PythonProjectNode)this.ProjectMgr, element);
            } catch (ArgumentNullException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (FileNotFoundException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (BadImageFormatException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (FileLoadException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (System.Security.SecurityException e) {
                Trace.WriteLine("Exception : " + e.Message);
            }

            return node;
        }

        protected override AssemblyReferenceNode CreateAssemblyReferenceNode(string fileName) {
            AssemblyReferenceNode node = null;
            try {
                node = new PythonAssemblyReferenceNode((PythonProjectNode)this.ProjectMgr, fileName);
            } catch (ArgumentNullException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (FileNotFoundException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (BadImageFormatException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (FileLoadException e) {
                Trace.WriteLine("Exception : " + e.Message);
            } catch (System.Security.SecurityException e) {
                Trace.WriteLine("Exception : " + e.Message);
            }

            return node;
        }

        protected override ReferenceNode CreateReferenceNode(string referenceType, ProjectElement element) {
            if (referenceType == ProjectFileConstants.Reference) {
                if (Path.GetExtension(element.Url).Equals(".pyd", StringComparison.OrdinalIgnoreCase)) {
                    return new DeprecatedReferenceNode(
                        ProjectMgr,
                        element,
                        element.GetMetadata(ProjectFileConstants.Include),
                        Strings.PydReferenceDeprecated
                    );
                }
            } else if (referenceType == ProjectFileConstants.WebPiReference) {
                return new DeprecatedReferenceNode(
                    ProjectMgr,
                    element,
                    element.GetMetadata("FriendlyName"),
                    Strings.WebPIReferenceDeprecated
                );
            }

            return base.CreateReferenceNode(referenceType, element);
        }
    }
}
