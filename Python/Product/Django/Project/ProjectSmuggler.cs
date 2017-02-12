using System;
using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Django.Project {

    [ComVisible(true)]
    [Guid("A666B929-44D0-4D68-A62A-7440A2E96D44")]
    public sealed class ProjectSmuggler {
        internal readonly DjangoProject Project;

        internal ProjectSmuggler(DjangoProject project) {
            Project = project;
        }
    }
}
