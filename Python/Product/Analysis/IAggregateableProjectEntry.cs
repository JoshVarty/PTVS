using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Analysis {

    /// <summary>
    /// Allows a project entry to know what aggregate project entries it has been put
    /// into.  The aggregate project can then call BumpVersion on the aggregate to
    /// cause the aggregate entries to be discarded.
    /// </summary>
    public interface IAggregateableProjectEntry {
        void AggregatedInto(AggregateProjectEntry into);
    }
}
