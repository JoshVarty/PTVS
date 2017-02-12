using System.Collections.Generic;

namespace Microsoft.PythonTools.Analysis.Analyzer {
    interface IReferenceableContainer {
        IEnumerable<IReferenceable> GetDefinitions(string name);
    }

    interface IReferenceable {
        IEnumerable<EncodedLocation> Definitions {
            get;
        }
        IEnumerable<EncodedLocation> References {
            get;
        }
    }

}
