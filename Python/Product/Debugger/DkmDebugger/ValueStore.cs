namespace Microsoft.PythonTools.DkmDebugger {

    /// <summary>
    ///  Represents a stored value, with a potentially non-imdepotent (if the backing store changes) and potentially expensive retrieval operation.
    /// </summary>
    internal interface IValueStore {
        /// <summary>
        /// Read the stored value.
        /// </summary>
        /// <remarks>
        /// This operation is not imdepotent, and can be expensive - don't repeatedly call on the same proxy unless deliberately trying to obtain a fresh value.
        /// </remarks>
        object Read();
    }

    /// <summary>
    /// Represents a stored typed value.
    /// </summary>
    internal interface IValueStore<out T> : IValueStore {
        new T Read();
    }

    /// <summary>
    /// A simple implementation of <see cref="IValueStore"/> which simply wraps the provided value.
    /// </summary>
    internal class ValueStore<T> : IValueStore<T> {
        private readonly T _value;

        public ValueStore(T value) {
            _value = value;
        }

        public T Read() {
            return _value;
        }

        object IValueStore.Read() {
            return Read();
        }
    }
}
