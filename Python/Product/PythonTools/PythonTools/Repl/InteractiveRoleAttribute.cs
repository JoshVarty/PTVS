using System;

namespace Microsoft.PythonTools.Repl {
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    sealed class InteractiveWindowRoleAttribute : Attribute {
        private readonly string _name;

        public InteractiveWindowRoleAttribute(string name) {
            if (name.Contains(",")) {
                throw new ArgumentException("ReplRoleAttribute name cannot contain any commas. " +
                    "Apply multiple attributes if you want to support multiple roles.", "name");
            }

            _name = name;
        }

        public string Name {
            get { return _name; }
        }
    }
}
