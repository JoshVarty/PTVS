using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    public interface IPythonSequenceType : IPythonType {
        IEnumerable<IPythonType> IndexTypes {
            get;
        }
    }
}
