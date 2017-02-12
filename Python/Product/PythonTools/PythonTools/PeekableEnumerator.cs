using System.Collections.Generic;

namespace Microsoft.PythonTools {
    /// <summary>
    /// An enumerator that allows one item either side of the current element to
    /// be previewed.
    /// </summary>
    sealed class PeekableEnumerator<T> : IEnumerator<T> {
        private readonly IEnumerable<T> _enumerable;
        private IEnumerator<T> _enumerator;

        public PeekableEnumerator(IEnumerable<T> enumerable) {
            _enumerable = enumerable;
            Reset();
        }

        public void Reset() {
            _enumerator = _enumerable.GetEnumerator();
            
            HasPrevious = false;
            Previous = default(T);

            HasCurrent = false;
            Current = default(T);

            HasNext = _enumerator.MoveNext();
            Next = HasNext ? _enumerator.Current : default(T);
        }

        public T Previous { get; private set; }
        public T Current { get; private set; }
        public T Next { get; private set; }

        public bool HasPrevious { get; private set; }
        public bool HasCurrent { get; private set; }
        public bool HasNext { get; private set; }

        public void Dispose() {
            _enumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current {
            get { return (object)Current; }
        }

        public bool MoveNext() {
            HasPrevious = HasCurrent;
            Previous = Current;

            HasCurrent = HasNext;
            Current = Next;

            HasNext = HasCurrent && _enumerator.MoveNext();
            Next = HasNext ? _enumerator.Current : default(T);

            return HasCurrent;
        }
    }
}
