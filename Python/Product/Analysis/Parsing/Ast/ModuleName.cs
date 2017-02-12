namespace Microsoft.PythonTools.Parsing.Ast {
    public class ModuleName : DottedName {
        public ModuleName(NameExpression[] names)
            : base(names) {
        }
    }
}
