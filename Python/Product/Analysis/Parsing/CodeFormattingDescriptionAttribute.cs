using System;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Parsing {
    /// <summary>
    /// Provides the localized description of a code formatting option.
    /// 
    /// There is both a short description for use in lists, and a longer description
    /// which is available for tooltips or other UI elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CodeFormattingDescriptionAttribute : Attribute {
        private readonly string _short, _long;

        internal CodeFormattingDescriptionAttribute(string shortDescriptionResourceId, string longDescriptionResourceId) {
            _short = shortDescriptionResourceId;
            _long = longDescriptionResourceId;
        }

        public string ShortDescription {
            get {
                return Resources.ResourceManager.GetString(_short);
            }
        }

        public string LongDescription {
            get {
                return Resources.ResourceManager.GetString(_long);
            }
        }
    }
}
