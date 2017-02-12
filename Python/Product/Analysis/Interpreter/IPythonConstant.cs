
namespace Microsoft.PythonTools.Interpreter {
    public interface IPythonConstant : IMember {
        IPythonType Type {
            get;
        }
    }
}
