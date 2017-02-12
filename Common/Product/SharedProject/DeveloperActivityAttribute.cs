using System;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudioTools {
    class DeveloperActivityAttribute : RegistrationAttribute {
        private readonly Type _projectType;
        private readonly int _templateSet;
        private readonly string _developerActivity;

        public DeveloperActivityAttribute(string developerActivity, Type projectPackageType) {
            _developerActivity = developerActivity;
            _projectType = projectPackageType;
            _templateSet = 1;
        }

        public DeveloperActivityAttribute(string developerActivity, Type projectPackageType, int templateSet) {
            _developerActivity = developerActivity;
            _projectType = projectPackageType;
            _templateSet = templateSet;
        }

        public override void Register(RegistrationAttribute.RegistrationContext context) {
            var key = context.CreateKey("NewProjectTemplates\\TemplateDirs\\" + _projectType.GUID.ToString("B") + "\\/" + _templateSet);
            key.SetValue("DeveloperActivity", _developerActivity);
        }

        public override void Unregister(RegistrationAttribute.RegistrationContext context) {
        }
    }
}
