using System.Collections.Generic;

namespace Microsoft.PythonTools.Analysis {
    public interface IHasRichDescription {
        /// <summary>
        /// Returns a sequence of Kind,Text pairs that make up the description.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetRichDescription();
    }

    public static class WellKnownRichDescriptionKinds {
        public const string Name = "name";
        public const string Type = "type";
        public const string Misc = "misc";
        public const string Comma = "comma";
        public const string Parameter = "param";
        public const string EndOfDeclaration = "enddecl";
    }
}
