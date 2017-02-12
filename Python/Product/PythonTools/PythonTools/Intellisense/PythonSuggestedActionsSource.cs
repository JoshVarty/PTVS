using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PythonTools.Editor.Core;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Intellisense {
    class PythonSuggestedActionsSource : ISuggestedActionsSource {
        internal readonly IServiceProvider _provider;
        internal readonly ITextView _view;
        private readonly ITextBuffer _textBuffer;

        private readonly object _currentLock = new object();
        private IEnumerable<SuggestedActionSet> _current;
        private SnapshotSpan _currentSpan;
        private readonly UIThreadBase _uiThread;

        public PythonSuggestedActionsSource(
            IServiceProvider provider,
            ITextView textView,
            ITextBuffer textBuffer
        ) {
            _provider = provider;
            _view = textView;
            _textBuffer = textBuffer;
            _textBuffer.RegisterForNewAnalysis(OnNewAnalysisEntry);
            _uiThread = provider.GetUIThread();
        }

        private void OnNewAnalysisEntry(AnalysisEntry obj) {
            SuggestedActionsChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> SuggestedActionsChanged;

        public void Dispose() { }

        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken) {
            lock (_currentLock) {
                if (_currentSpan == range) {
                    return _current;
                }
            }
            return null;
        }

        public async Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken) {
            var pos = _view.Caret.Position.BufferPosition;
            if (pos.Position < pos.GetContainingLine().End.Position) {
                pos += 1;
            }
            var targetPoint = _view.BufferGraph.MapDownToFirstMatch(pos, PointTrackingMode.Positive, EditorExtensions.IsPythonContent, PositionAffinity.Successor);
            if (targetPoint == null) {
                return false;
            }
            var textBuffer = targetPoint.Value.Snapshot.TextBuffer;
            var lineStart = targetPoint.Value.GetContainingLine().Start;

            var span = targetPoint.Value.Snapshot.CreateTrackingSpan(
                lineStart,
                targetPoint.Value.Position - lineStart.Position,
                SpanTrackingMode.EdgePositive,
                TrackingFidelityMode.Forward
            );
            var imports = await _uiThread.InvokeTask(() => VsProjectAnalyzer.GetMissingImportsAsync(_provider, _view, textBuffer.CurrentSnapshot, span));

            if (imports == MissingImportAnalysis.Empty) {
                return false;
            }

            var suggestions = new List<SuggestedActionSet>();
            var availableImports = await imports.GetAvailableImportsAsync(cancellationToken);

            suggestions.Add(new SuggestedActionSet(
                availableImports.Select(s => new PythonSuggestedImportAction(this, textBuffer, s))
                    .OrderBy(k => k)
                    .Distinct()
            ));

            cancellationToken.ThrowIfCancellationRequested();

            if (!suggestions.SelectMany(s => s.Actions).Any()) {
                return false;
            }

            lock (_currentLock) {
                cancellationToken.ThrowIfCancellationRequested();
                _current = suggestions;
                _currentSpan = range;
            }

            return true;
        }

        public bool TryGetTelemetryId(out Guid telemetryId) {
            telemetryId = Guid.Empty;
            return false;
        }
    }
}
