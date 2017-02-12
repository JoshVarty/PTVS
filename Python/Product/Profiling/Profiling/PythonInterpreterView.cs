namespace Microsoft.PythonTools.Profiling {
    /// <summary>
    /// Provides a view model for the PythonInterpreter class.
    /// </summary>
    public class PythonInterpreterView {
        readonly string _name;
        readonly string _id;
        readonly string _path;
        
        /// <summary>
        /// Create a PythonInterpreterView with values from parameters.
        /// </summary>
        public PythonInterpreterView(string name, string id, string path) {
            _name = name;
            _id = id;
            _path = path;
        }

        /// <summary>
        /// Returns a PythonInterpreter with the values from the model view.
        /// </summary>
        /// <returns></returns>
        public PythonInterpreter GetInterpreter() {
            return new PythonInterpreter {
                Id = Id
            };
        }

        /// <summary>
        /// The display name of the interpreter, if available.
        /// </summary>
        public string Name {
            get {
                return _name;
            }
        }

        /// <summary>
        /// The Guid identifying the interpreter.
        /// </summary>
        public string Id { 
            get {
                return _id;
            }
        }

        /// <summary>
        /// The path to the interpreter, if available.
        /// </summary>
        public string Path { 
            get {
                return _path;
            }
        }

        public override string ToString() {
            return Name;
        }

        public override bool Equals(object obj) {
            var other = obj as PythonInterpreterView;
            if (other == null) {
                return false;
            } else {
                return Id.Equals(other.Id);
            }
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }
    }
}
 
