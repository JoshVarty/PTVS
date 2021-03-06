using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.PythonTools.Analysis {

    /// <summary>
    /// Represents a logical project entry which is an aggregate of multiple project entries.
    /// 
    /// The logical project entry will have its version bumped when any of one of the aggregates
    /// has their versions bumped.  This allows values which depend upon multiple project
    /// entries to exist in the system and be cleared out when any one of the project
    /// entries which contributed the dependency get updated.
    /// </summary>
    public sealed class AggregateProjectEntry : IVersioned {
        private int _version;
        /// <summary>
        /// The proejct entries which we are an aggregate of
        /// </summary>
        internal readonly HashSet<IProjectEntry> _aggregating;
        /// <summary>
        /// Transitions to the next aggregate from adding a single project entry.
        /// Provides quick lookup of the next project entry.
        /// </summary>
        private Dictionary<IProjectEntry, AggregateProjectEntry> _next;

        internal AggregateProjectEntry(HashSet<IProjectEntry> aggregating) {
            _aggregating = aggregating;
        }

        public int AnalysisVersion {
            get {
                return _version;
            }
        }

        public void BumpVersion() {
            _version++;
        }

        public void RemovedFromProject() {
            _aggregating.Clear();
            _next.Clear();
            _version = -1;
        }

        internal AggregateProjectEntry AggregateWith(ProjectEntry with) {
            if (_aggregating.Contains(with)) {
                // we're not adding any new types
                return this;
            }

            if (_next == null) {
                _next = new Dictionary<IProjectEntry, AggregateProjectEntry>();
            }

            AggregateProjectEntry entry;
            if (!_next.TryGetValue(with, out entry)) {
                // We don't yet have the next transition, create it now.
                _next[with] = entry = with.ProjectState.GetAggregate(_aggregating, with);
            }

            return entry;
        }

        internal static IVersioned GetAggregate(IVersioned from, ProjectEntry with) {
            if (from == with) {
                // No aggregation
                return from;
            }

            if (from is AggregateProjectEntry) {
                // We can use our fast check to see if we're already in the aggregate
                // or if we have a known transition.
                return ((AggregateProjectEntry)from).AggregateWith(with);
            }

            // We're aggregating two random project entries.
            Debug.Assert(from is IProjectEntry);
            return with.ProjectState.GetAggregate((IProjectEntry)from, with);
        }
    }
}
