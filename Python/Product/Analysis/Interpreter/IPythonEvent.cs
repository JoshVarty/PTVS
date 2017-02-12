using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    public interface IPythonEvent : IMember, IMemberContainer {
        IPythonType EventHandlerType {
            get;
        }

        IList<IPythonType> GetEventParameterTypes();

        string Documentation {
            get;
        }
    }
}
