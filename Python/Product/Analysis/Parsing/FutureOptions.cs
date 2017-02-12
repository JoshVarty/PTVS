using System;

namespace Microsoft.PythonTools.Parsing {
    /// <summary>
    /// Options which have been enabled using from __future__ import 
    /// </summary>
    [Flags]
    public enum FutureOptions {
        None = 0,
        /// <summary>
        /// Enable true division (1/2 == .5)
        /// </summary>
        TrueDivision = 0x0001,
        /// <summary>
        /// Enable usage of the with statement
        /// </summary>
        WithStatement = 0x0010,
        /// <summary>
        /// Enable absolute imports
        /// </summary>
        AbsoluteImports = 0x0020,
        /// <summary>
        /// Enable usage of print as a function for better compatibility with Python 3.0.
        /// </summary>
        PrintFunction = 0x0400,
        /// <summary>
        /// String Literals should be parsed as Unicode strings
        /// </summary>
        UnicodeLiterals = 0x2000,
    }

}
