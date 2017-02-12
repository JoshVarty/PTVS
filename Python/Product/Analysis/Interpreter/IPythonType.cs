using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    public interface IPythonType : IMemberContainer, IMember {
        IPythonFunction GetConstructors();

        // PythonType.Get__name__(this);
        string Name {
            get;
        }

        string Documentation {
            get;
        }

        BuiltinTypeId TypeId {
            get;
        }

        IPythonModule DeclaringModule {
            get;
        }

        IList<IPythonType> Mro {
            get;
        }

        bool IsBuiltin {
            get;
        }
    }
}
