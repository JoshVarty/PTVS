using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class ProvideBraceCompletionAttribute : RegistrationAttribute {
        private string _languageName;

        public ProvideBraceCompletionAttribute(string languageName) {
            _languageName = languageName;
        }

        public override void Register(RegistrationContext context) {
            using (Key serviceKey = context.CreateKey(LanguageServiceName)) {
                serviceKey.SetValue("ShowBraceCompletion", (int)1);
            }
        }

        public override void Unregister(RegistrationContext context) {
        }

        private string LanguageServiceName {
            get {
                return string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", "Languages\\Language Services", _languageName);
            }
        }
    }
}
