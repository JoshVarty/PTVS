
using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Provides the location of a member.  This should be implemented on a class
    /// which also implements IMember.
    /// Implementing this interface enables Goto Definition on the member.
    /// 
    /// New in v1.1.
    /// </summary>
    public interface ILocatedMember {
        /// <summary>
        /// Returns where the member is located or null if the location is not known.
        /// </summary>
        IEnumerable<LocationInfo> Locations {
            get;
        }
    }
}
