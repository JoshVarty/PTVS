using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Tracks version information for a parsed project entry along with
    /// the individual ASTs which represent each unique buffer which forms
    /// an overall project entry.
    /// </summary>
    sealed class VersionCookie : IAnalysisCookie {
        /// <summary>
        /// Dictionary from buffer ID to VersionInfo.
        /// </summary>
        public readonly Dictionary<int, BufferVersion> Buffers;

        public VersionCookie(Dictionary<int, BufferVersion> versions) {
            Buffers = versions;
        }
    }

    /// <summary>
    /// Stores a snapshot of a specific buffer at a specific version.
    /// </summary>
    sealed class BufferVersion {
        /// <summary>
        /// The version this buffer was last parsed at
        /// </summary>
        public readonly int Version;
        /// <summary>
        /// The ASP that was produced from the last version parsed
        /// </summary>
        public readonly PythonAst Ast;

        public BufferVersion(int version, PythonAst ast) {
            Version = version;
            Ast = ast;
        }
    }
}
