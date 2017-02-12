using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IClassifierAggregatorService))]
    public class MockClassifierAggregatorService : IClassifierAggregatorService {
        [ImportMany]
        internal IEnumerable<Lazy<IClassifierProvider, IContentTypeMetadata>> _providers = null;
        
        public IClassifier GetClassifier(ITextBuffer textBuffer) {
            if (_providers == null) {
                return null;
            }

            var contentType = textBuffer.ContentType;
            return new AggregatedClassifier(
                textBuffer,
                _providers.Where(e => e.Metadata.ContentTypes.Any(c => contentType.IsOfType(c)))
                    .Select(e => e.Value)
            );
        }

        sealed class AggregatedClassifier : IClassifier, IDisposable {
            private readonly ITextBuffer _buffer;
            private readonly List<IClassifier> _classifiers;

            public AggregatedClassifier(ITextBuffer textBuffer, IEnumerable<IClassifierProvider> providers) {
                _buffer = textBuffer;
                _classifiers = providers.Select(p => p.GetClassifier(_buffer)).ToList();
            }

            public void Dispose() {
                foreach (var c in _classifiers.OfType<IDisposable>()) {
                    c.Dispose();
                }
            }

            public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged {
                add {
                    foreach (var c in _classifiers) {
                        c.ClassificationChanged += value;
                    }
                }
                remove {
                    foreach (var c in _classifiers) {
                        c.ClassificationChanged -= value;
                    }
                }
            }

            public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
                return _classifiers.SelectMany(c => c.GetClassificationSpans(span)).ToList();
            }
        }
    }
}
