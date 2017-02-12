using System;

namespace Microsoft.PythonTools.Parsing {
    /// <summary>
    /// Provides binary examples for a code formatting option of how it affects the code
    /// when the option is turned on or off.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public sealed class CodeFormattingExampleAttribute : Attribute {
        private readonly string _on, _off;

        internal CodeFormattingExampleAttribute(string doc) {
            _on = _off = doc;
        }

        internal CodeFormattingExampleAttribute(string on, string off) {
            _on = on;
            _off = off;
        }

        public string On {
            get {
                return _on;
            }
        }

        public string Off {
            get {
                return _off;
            }
        }
    }
}
