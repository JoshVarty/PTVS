using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Projects {
    /// <summary>
    /// Provides access to an abstract Python project.
    /// </summary>
    public interface IPythonProjectProvider {
        PythonProject Project {
            get;
        }
    }
}
