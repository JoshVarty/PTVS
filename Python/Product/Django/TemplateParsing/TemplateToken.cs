using System;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    struct TemplateToken : IEquatable<TemplateToken> {
        internal readonly TemplateTokenKind Kind;
        internal readonly int Start, End;
        internal readonly bool IsClosed;

        public TemplateToken(TemplateTokenKind kind, int start, int end, bool isClosed = true) {
            Kind = kind;
            Start = start;
            End = end;
            IsClosed = isClosed;
        }

        public override bool Equals(object obj) {
            if (obj is TemplateToken) {
                return Equals((TemplateToken)obj);
            }
            return false;
        }

        public override int GetHashCode() {
            return Kind.GetHashCode() ^ Start ^ End ^ IsClosed.GetHashCode();
        }

        #region IEquatable<TemplateToken> Members

        public bool Equals(TemplateToken other) {
            return Kind == other.Kind &&
                Start == other.Start &&
                End == other.End &&
                IsClosed == other.IsClosed;
        }

        #endregion
    }
}
