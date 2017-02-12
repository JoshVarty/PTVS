using System;
using System.IO;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    class ProvideFileFilterAttribute : RegistrationAttribute {
        private readonly string _id, _name, _filter;
        private readonly int _sortPriority;

        public ProvideFileFilterAttribute(string projectGuid, string name, string filter, int sortPriority) {
            _name = name;
            _id = Guid.Parse(projectGuid).ToString("B");
            _filter = filter;
            _sortPriority = sortPriority;
        }

        public override void Register(RegistrationContext context) {
            using (var engineKey = context.CreateKey("Projects\\" + _id + "\\Filters\\" + _name)) {
                engineKey.SetValue("", _filter);
                engineKey.SetValue("SortPriority", _sortPriority);
                engineKey.SetValue("CommonOpenFilesFilter", 1);
            }
        }

        public override void Unregister(RegistrationContext context) {
        }
    }
}
