using System;

namespace Microsoft.PythonTools.Parsing {
    [Flags]
    public enum TokenizerOptions {
        None,
        /// <summary>
        /// Tokenizer is in verbatim mode and will track extra information including white space proceeding tokens, the exact representation of constant tokens, etc...
        /// 
        /// Despite the presence of extra information no tokens will be reported which aren't normally reported.  Instead items which would be reported when 
        /// VerbatimCommentsAndLineJoins is also specified will be included in the white space tracking strings.
        /// </summary>
        Verbatim = 0x01,
        /// <summary>
        /// Tokenizer will report comment tokens and explicit line join tokens in addition to the normal tokens it produces.
        /// </summary>
        VerbatimCommentsAndLineJoins = 0x02,
        /// <summary>
        /// Tokenizer will attempt to recover from groupings which contain invalid characters
        /// by ending the grouping automatically.  This allows code like:
        /// 
        /// x = f(
        /// 
        /// def g():
        ///     pass
        ///     
        /// To successfully parse the function defintion even though we have no idea we should
        /// be looking at indents/dedents after the open grouping starts.
        /// </summary>
        GroupingRecovery = 0x04
    }
}
