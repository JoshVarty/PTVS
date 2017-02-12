using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.PythonTools.Analysis {
    static class LockedEnumerable {
        public static IEnumerable<T> AsLockedEnumerable<T>(this IEnumerable<T> source) {
            return new LockedEnumerableObject<T>(source, source);
        }

        public static IEnumerable<T> AsLockedEnumerable<T>(this IEnumerable<T> source, object lockObject) {
            return new LockedEnumerableObject<T>(source, lockObject);
        }

        class LockedEnumerableObject<T> : IEnumerable<T> {
            private readonly IEnumerable<T> _source;
            private readonly object _lockObject;

            public LockedEnumerableObject(IEnumerable<T> source, object lockObject) {
                _source = source;
                _lockObject = lockObject;
            }

            public IEnumerator<T> GetEnumerator() {
                bool lockTaken = false;
                bool enumeratorReturned = false;

                try {
                    Monitor.Enter(_lockObject, ref lockTaken);

                    var enumerator = new LockedEnumeratorObject<T>(_source.GetEnumerator(), _lockObject);
                    enumeratorReturned = true;
                    return enumerator;
                } finally {
                    if (lockTaken && !enumeratorReturned) {
                        Monitor.Exit(_lockObject);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }
        }

        struct LockedEnumeratorObject<T> : IEnumerator<T> {
            private readonly IEnumerator<T> _source;
            private readonly object _lockObject;

            public LockedEnumeratorObject(IEnumerator<T> source, object lockObject) {
                _source = source;
                _lockObject = lockObject;
            }

            public T Current {
                get {
                    return _source.Current;
                }
            }

            object IEnumerator.Current {
                get {
                    return ((IEnumerator)_source).Current;
                }
            }

            public void Dispose() {
                try {
                    _source.Dispose();
                } finally {
                    Monitor.Exit(_lockObject);
                }
            }

            public bool MoveNext() {
                return _source.MoveNext();
            }

            public void Reset() {
                _source.Reset();
            }
        }
    }
}
