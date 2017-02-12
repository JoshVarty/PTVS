using System;
using System.ComponentModel.Composition;

namespace TestUtilities.SharedProject {
    /// <summary>
    /// Registers the extension used for code files.  See ProjectTypeDefinition
    /// for how this is used.  This attribute is required.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CodeExtensionAttribute : Attribute {
        public readonly string _codeExtension;

        public CodeExtensionAttribute(string extension) {
            _codeExtension = extension;
        }

        public string CodeExtension {
            get {
                return _codeExtension;
            }
        }
    }

}
