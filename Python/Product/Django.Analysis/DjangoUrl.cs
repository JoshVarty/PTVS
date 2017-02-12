using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.PythonTools.Django.Analysis {
    class DjangoUrl {
        public readonly string Name;
        public string FullName {
            get {
                return Name;
            }
        }
        private readonly string _urlRegex;
        private static readonly Regex _regexGroupMatchingRegex = new Regex(@"\(.*?\)");
        public IList<DjangoUrlParameter> Parameters = new List<DjangoUrlParameter>();

        public IEnumerable<DjangoUrlParameter> NamedParameters {
            get {
                return Parameters.Where(p => p.IsNamed);
            }
        }

        public DjangoUrl() { }

        public DjangoUrl(string urlName, string urlRegex) {
            Name = urlName;
            _urlRegex = urlRegex;

            ParseUrlRegex();
        }

        private void ParseUrlRegex() {
            MatchCollection matches = _regexGroupMatchingRegex.Matches(_urlRegex);

            foreach (Match m in matches) {
                foreach (Group grp in m.Groups) {
                    Parameters.Add(new DjangoUrlParameter(grp.Value));
                }
            }
        }
    }

    class DjangoUrlParameter {
        private static readonly Regex _namedParameterRegex = new Regex(@"\?P<(.*)>");

        public readonly string RegexValue;
        public readonly string Name;
        public readonly bool IsNamed;

        public DjangoUrlParameter() { }

        public DjangoUrlParameter(string parameterRegex) {
            Name = parameterRegex;
            RegexValue = parameterRegex.TrimStart('(').TrimEnd(')');

            Match m = _namedParameterRegex.Match(RegexValue);
            IsNamed = m.Success && m.Groups.Count == 2;
            if (IsNamed) {
                Name = m.Groups[1].Value;
            }
        }
    }
}
