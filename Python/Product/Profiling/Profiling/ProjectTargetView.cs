using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.PythonTools.Profiling {
    /// <summary>
    /// Provides a view model for the ProjectTarget class.
    /// </summary>
    public class ProjectTargetView {
        readonly string _name;
        readonly Guid _guid;
        
        /// <summary>
        /// Create a ProjectTargetView with values from an EnvDTE.Project.
        /// </summary>
        public ProjectTargetView(IVsHierarchy project) {
            object value;
            ErrorHandler.ThrowOnFailure(project.GetProperty(
                (uint)VSConstants.VSITEMID.Root,
                (int)__VSHPROPID.VSHPROPID_Name,
                out value
            ));
            _name = value as string ?? Strings.ProjectTargetUnknownName;
            ErrorHandler.ThrowOnFailure(project.GetGuidProperty(
                (uint)VSConstants.VSITEMID.Root,
                (int)__VSHPROPID.VSHPROPID_ProjectIDGuid,
                out _guid
            ));
        }

        /// <summary>
        /// Create a ProjectTargetView with values from a ProjectTarget.
        /// </summary>
        public ProjectTargetView(ProjectTarget project) {
            _name = project.FriendlyName;
            _guid = project.TargetProject;
        }

        /// <summary>
        /// Returns a ProjectTarget created with the values from the view model.
        /// </summary>
        public ProjectTarget GetTarget() {
            return new ProjectTarget {
                FriendlyName = _name,
                TargetProject = _guid
            };
        }

        /// <summary>
        /// The display name of the project.
        /// </summary>
        public string Name {
            get {
                return _name;
            }
        }

        /// <summary>
        /// The Guid identifying the project.
        /// </summary>
        public Guid Guid {
            get {
                return _guid;
            }
        }

        public override string ToString() {
            return Name;
        }

        public override bool Equals(object obj) {
            var other = obj as ProjectTargetView;
            if (other == null) {
                return false;
            } else {
                return Guid.Equals(other.Guid);
            }
        }

        public override int GetHashCode() {
            return Guid.GetHashCode();
        }
    }
}

 
