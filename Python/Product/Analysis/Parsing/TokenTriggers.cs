using System;

namespace Microsoft.PythonTools.Parsing {

    /// <summary>
    /// See also <c>Microsoft.VisualStudio.Package.TokenTriggers</c>.
    /// </summary>
    [Flags]
    public enum TokenTriggers {
        None = 0,
        MemberSelect = 1,
        MatchBraces = 2,
        ParameterStart = 16,
        ParameterNext = 32,
        ParameterEnd = 64,
        Parameter = 128,
        MethodTip = 240,
    }

}
