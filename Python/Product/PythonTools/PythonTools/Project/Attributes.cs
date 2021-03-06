using System;
using System.ComponentModel;

namespace Microsoft.PythonTools.Project {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal sealed class SRDisplayNameAttribute : DisplayNameAttribute {
        private readonly string _name;

        public SRDisplayNameAttribute(string name) {
            _name = name;
        }

        public override string DisplayName {
            get {
                return Strings.ResourceManager.GetString(_name) ?? VisualStudioTools.Project.SR.GetString(_name);
            }
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute {
        private bool _replaced;

        public SRDescriptionAttribute(string description)
            : base(description) {
        }

        public override string Description {
            get {
                if (!_replaced) {
                    _replaced = true;
                    DescriptionValue = Strings.ResourceManager.GetString(base.Description) ??
                        VisualStudioTools.Project.SR.GetString(base.Description);
                }
                return base.Description;
            }
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRCategoryAttribute : CategoryAttribute {
        public SRCategoryAttribute(string category)
            : base(category) {
        }

        protected override string GetLocalizedString(string value) {
            return Strings.ResourceManager.GetString(value) ?? VisualStudioTools.Project.SR.GetString(value);
        }
    }
}
