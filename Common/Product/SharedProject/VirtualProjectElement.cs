using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.VisualStudioTools.Project {
    class VirtualProjectElement : ProjectElement {
        private readonly Dictionary<string, string> _virtualProperties;

        /// <summary>
        /// Constructor to Wrap an existing MSBuild.ProjectItem
        /// Only have internal constructors as the only one who should be creating
        /// such object is the project itself (see Project.CreateFileNode()).
        /// </summary>
        /// <param name="project">Project that owns this item</param>
        /// <param name="existingItem">an MSBuild.ProjectItem; can be null if virtualFolder is true</param>
        /// <param name="virtualFolder">Is this item virtual (such as reference folder)</param>
        internal VirtualProjectElement(ProjectNode project)
            : base(project) {
            _virtualProperties = new Dictionary<string, string>();
        }

        protected override string ItemType {
            get {
                return "";
            }
            set {
            }
        }

        /// <summary>
        /// Set an attribute on the project element
        /// </summary>
        /// <param name="attributeName">Name of the attribute to set</param>
        /// <param name="attributeValue">Value to give to the attribute</param>
        public override void SetMetadata(string attributeName, string attributeValue) {
            Debug.Assert(String.Compare(attributeName, ProjectFileConstants.Include, StringComparison.OrdinalIgnoreCase) != 0, "Use rename as this won't work");

            // For virtual node, use our virtual property collection
            _virtualProperties[attributeName] = attributeValue;
        }

        /// <summary>
        /// Get the value of an attribute on a project element
        /// </summary>
        /// <param name="attributeName">Name of the attribute to get the value for</param>
        /// <returns>Value of the attribute</returns>
        public override string GetMetadata(string attributeName) {
            // For virtual items, use our virtual property collection
            if (!_virtualProperties.ContainsKey(attributeName)) {
                return String.Empty;
            }

            return _virtualProperties[attributeName];
        }

        public override void Rename(string newPath) {
            _virtualProperties[ProjectFileConstants.Include] = newPath;
        }

        public override bool Equals(object obj) {
            return Object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode() {
            return RuntimeHelpers.GetHashCode(this);
        }
    }
}
