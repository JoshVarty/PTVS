using System;

namespace Microsoft.PythonTools.Parsing {
    public sealed class ParserOptions {
        internal static ParserOptions Default = new ParserOptions();
        public ParserOptions() {
            ErrorSink = ErrorSink.Null;
        }

        public ErrorSink ErrorSink { get; set; }

        public Severity IndentationInconsistencySeverity { set; get; }

        public bool Verbatim { get; set; }

        /// <summary>
        /// True if references to variables should be bound in the AST.  The root node must be
        /// held onto to access the references via GetReference/GetReferences APIs on various 
        /// nodes which reference variables.
        /// </summary>
        public bool BindReferences { get; set; }

        /// <summary>
        /// Specifies the class name the parser starts off with for name mangling name expressions.
        /// 
        /// For example __fob would turn into _C__fob if PrivatePrefix is set to C.
        /// </summary>
        public string PrivatePrefix { get; set; }

        /// <summary>
        /// An event that is raised for every comment in the source as it is parsed.
        /// </summary>
        public event EventHandler<CommentEventArgs> ProcessComment;

        internal void RaiseProcessComment(object sender, CommentEventArgs e) {
            var handler = ProcessComment;
            if (handler != null) {
                handler(sender, e);
            }
        }
    }

    public class CommentEventArgs : EventArgs {
        public SourceSpan Span { get; private set; }
        public string Text { get; private set; }

        public CommentEventArgs(SourceSpan span, string text) {
            Span = span;
            Text = text;
        }
    }
}
