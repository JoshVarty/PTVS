using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Projection;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal static class TextBufferExtensions {
        public static string GetFileName(this ITextBuffer textBuffer) {
            string path = string.Empty;
            IEnumerable<ITextBuffer> searchBuffers = GetContributingBuffers(textBuffer);

            foreach (ITextBuffer buffer in searchBuffers) {
                ITextDocument document = null;
                if (buffer.Properties.TryGetProperty(typeof(ITextDocument), out document)) {
                    path = document.FilePath ?? string.Empty;
                    if (!string.IsNullOrEmpty(path)) {
                        break;
                    }
                }
            }

            return path;
        }

        public static IEnumerable<ITextBuffer> GetContributingBuffers(this ITextBuffer textBuffer) {
            var allBuffers = new List<ITextBuffer>();

            allBuffers.Add(textBuffer);
            for (int i = 0; i < allBuffers.Count; i++) {
                IProjectionBuffer currentBuffer = allBuffers[i] as IProjectionBuffer;
                if (currentBuffer != null) {
                    foreach (ITextBuffer sourceBuffer in currentBuffer.SourceBuffers) {
                        if (!allBuffers.Contains(sourceBuffer))
                            allBuffers.Add(sourceBuffer);
                    }
                }
            }

            return allBuffers;
        }

    }
}
