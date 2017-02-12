using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Intellisense {
    sealed class ProjectEntryMap : IEnumerable<KeyValuePair<string, IProjectEntry>> {
        private readonly List<IProjectEntry> _ids = new List<IProjectEntry>();
        private readonly Stack<int> _freedIds = new Stack<int>();
        private readonly ConcurrentDictionary<string, IProjectEntry> _projectFiles = new ConcurrentDictionary<string, IProjectEntry>(StringComparer.OrdinalIgnoreCase);
        private static object _idKey = new object();

        /// <summary>
        /// Must be called from the UI thread
        /// </summary>
        public int Add(string filename, IProjectEntry node) {
#if DEBUG
            foreach (var item in _ids) {
                Debug.Assert(node != item);
            }
#endif
            int id;
            if (_freedIds.Count > 0) {
                var i = _freedIds.Pop();
                _ids[i] = node;
                id = i + 1;
            } else {
                _ids.Add(node);
                // ids are 1 based
                id = _ids.Count;
            }
            _projectFiles[filename] = node;
            node.Properties[_idKey] = id;
            return id;
        }

        /// <summary>
        /// Must be called from the UI thread
        /// </summary>
        public void Remove(IProjectEntry node) {
            int i = GetId(node) - 1;
            if (i < 0 ||
                i >= _ids.Count ||
                !object.ReferenceEquals(node, _ids[i])) {
                throw new InvalidOperationException("Removing node with invalid ID or map is corrupted");
            }

            _ids[i] = null;
            _freedIds.Push(i);
            IProjectEntry removed;
            _projectFiles.TryRemove(node.FilePath, out removed);
        }

        public static int GetId(IProjectEntry node) {
            return (int)node.Properties[_idKey];
        }

        public IEnumerator<KeyValuePair<string, IProjectEntry>> GetEnumerator() {
            return _projectFiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _projectFiles.GetEnumerator();
        }

        /// <summary>
        /// Must be called from the UI thread
        /// </summary>
        public IProjectEntry this[int itemId] {
            get {
                int i = (int)itemId - 1;
                if (0 <= i && i < _ids.Count) {
                    return _ids[i];
                }
                return null;
            }
        }

        public bool TryGetValue(string path, out IProjectEntry item) {
            return _projectFiles.TryGetValue(path, out item);
        }
    }
}
