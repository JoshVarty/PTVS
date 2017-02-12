using System;
using System.ComponentModel.Composition;

namespace TestUtilities.SharedProject {
    /// <summary>
    /// Registers the sample code used for a project.  See ProjectTypeDefinition
    /// for how this is used.  This attribute is optional.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SampleCodeAttribute : Attribute {
        public readonly string _sampleCode;

        public SampleCodeAttribute(string sampleCode) {
            _sampleCode = sampleCode;
        }

        public string SampleCode {
            get {
                return _sampleCode;
            }
        }
    }
}
