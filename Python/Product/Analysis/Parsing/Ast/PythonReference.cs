namespace Microsoft.PythonTools.Parsing.Ast {
    /// <summary>
    /// Represents a reference to a name.  A PythonReference is created for each locatio
    /// where a name is referred to in a scope (global, class, or function).  
    /// </summary>
    public class PythonReference {
        private readonly string/*!*/ _name;
        private PythonVariable _variable;

        public PythonReference(string/*!*/ name) {
            _name = name;
        }

        public string/*!*/ Name {
            get { return _name; }
        }

        public PythonVariable Variable {
            get { return _variable; }
            set { _variable = value; }
        }
    }
}
