using System;

namespace Microsoft.PythonTools.Parsing {
    /// <summary>
    /// Provides the default value for the code formatting option.
    /// 
    /// This is the default value that is used by an IDE or other tool.  When
    /// used programmatically the code formatting engine defaults to not altering
    /// code at all.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CodeFormattingDefaultValueAttribute : Attribute {
        private readonly object _defaultValue;

        internal CodeFormattingDefaultValueAttribute(object defaultValue) {
            _defaultValue = defaultValue;
        }

        public object DefaultValue {
            get {
                return _defaultValue;
            }
        }
    }
}
