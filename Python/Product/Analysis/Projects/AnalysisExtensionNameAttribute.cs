using System;
using System.ComponentModel.Composition;

namespace Microsoft.PythonTools.Projects {
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]

    public sealed class AnalysisExtensionNameAttribute : Attribute {
        private readonly string _name;

        public AnalysisExtensionNameAttribute(string name) {
            _name = name;
        }

        public string Name => _name;
    }

}
