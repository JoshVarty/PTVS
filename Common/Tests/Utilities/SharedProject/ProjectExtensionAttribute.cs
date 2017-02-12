using System;
using System.ComponentModel.Composition;

namespace TestUtilities.SharedProject {
    /// <summary>
    /// Registers the extension used for a project.  See ProjectTypeDefinition
    /// for how this is used.  This attribute is required.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ProjectExtensionAttribute : Attribute {
        public readonly string _projectExtension;

        public ProjectExtensionAttribute(string extension) {
            _projectExtension = extension;
        }

        public string ProjectExtension {
            get {
                return _projectExtension;
            }
        }
    }
}
