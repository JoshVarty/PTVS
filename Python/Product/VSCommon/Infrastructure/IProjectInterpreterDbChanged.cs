using System;

namespace Microsoft.PythonTools.Infrastructure {
    public interface IProjectInterpreterDbChanged {
        event EventHandler InterpreterDbChanged;
    }
}
