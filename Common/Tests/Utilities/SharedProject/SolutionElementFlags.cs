using System;

namespace TestUtilities.SharedProject {
    [Flags]
    public enum SolutionElementFlags {
        None,
        ExcludeFromSolution = 0x01,
        ExcludeFromConfiguration = 0x02
    }
}
