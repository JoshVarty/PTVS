using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Tracks our quick info response.  We kick off an async request
    /// to get the info and then attach it to the buffer.  We then
    /// trigger the session and this instance is retrieved.
    /// </summary>
    internal sealed class QuickInfo {
        public readonly string Text;
        public readonly ITrackingSpan Span;

        public QuickInfo(string text, ITrackingSpan span) {
            Text = text;
            Span = span;
        }
    }
}
