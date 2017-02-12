using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Interpreter {
    public interface IPythonInterpreterWithProjectReferences {
        /// <summary>
        /// Asynchronously loads the assocated project reference into the interpreter.
        /// 
        /// Returns a new task which can be waited upon for completion of the reference being added.
        /// </summary>
        /// <remarks>New in 2.0.</remarks>
        Task AddReferenceAsync(ProjectReference reference, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes the associated project reference from the interpreter.
        /// </summary>
        /// <remarks>New in 2.0.</remarks>
        void RemoveReference(ProjectReference reference);
    }

    public interface IPythonInterpreterWithProjectReferences2 : IPythonInterpreterWithProjectReferences {
        IEnumerable<ProjectReference> GetReferences();
    }
}
