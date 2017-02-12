namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Encapsulates all of the possible knobs which can be flipped when extracting a method.
    /// </summary>
    class ExtractMethodRequest {
        private readonly string _name;
        private readonly string[] _parameters;
        private readonly ScopeWrapper _targetScope;

        public ExtractMethodRequest(ScopeWrapper targetScope, string name, string[] parameters) {
            _name = name;
            _parameters = parameters;
            _targetScope = targetScope;
        }

        /// <summary>
        /// The name of the new method which should be created
        /// </summary>
        public string Name {
            get {
                return _name;
            }
        }

        /// <summary>
        /// The variables which are consumed by the method but which should be passed in as parameters
        /// (versus closing over the variables)
        /// </summary>
        public string[] Parameters {
            get {
                return _parameters;
            }
        }

        /// <summary>
        /// The target scope to extract the method to.
        /// </summary>
        public ScopeWrapper TargetScope {
            get {
                return _targetScope;
            }
        }
    }
}
