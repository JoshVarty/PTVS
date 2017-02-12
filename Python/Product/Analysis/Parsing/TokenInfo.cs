using System;

namespace Microsoft.PythonTools.Parsing {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")] // TODO: fix
    [Serializable]
    public struct TokenInfo : IEquatable<TokenInfo> {
        private TokenCategory _category;
        private TokenTriggers _trigger;
        private SourceSpan _span;
        
        public TokenCategory Category {
            get { return _category; }
            set { _category = value; }
        }

        public TokenTriggers Trigger {
            get { return _trigger; }
            set { _trigger = value; }
        }

        public SourceSpan SourceSpan {
            get { return _span; }
            set { _span = value; }
        }

        internal TokenInfo(SourceSpan span, TokenCategory category, TokenTriggers trigger) {
            _category = category;
            _trigger = trigger;
            _span = span;
        }

        #region IEquatable<TokenInfo> Members

        public bool Equals(TokenInfo other) {
            return _category == other._category && _trigger == other._trigger && _span == other._span;
        }

        #endregion

        public override string ToString() {
            return String.Format("TokenInfo: {0}, {1}, {2}", _span, _category, _trigger);
        }
    }
}
