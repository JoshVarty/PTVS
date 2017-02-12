using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Infrastructure {
    public static class EnumerableExtensions {
        public static IEnumerable<T> MaybeEnumerate<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        private static T Identity<T>(T source) {
            return source;
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source) {
            return source.SelectMany(Identity);
        }

        public static IEnumerable<T> Ordered<T>(this IEnumerable<T> source) {
            return source.OrderBy(Identity);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T value) {
            return source.Where(v => {
                try {
                    return !v.Equals(value);
                } catch (NullReferenceException) {
                    return false;
                }
            });
        }

        private static TKey GetKey<TKey, TValue>(KeyValuePair<TKey, TValue> source) {
            return source.Key;
        }

        public static IEnumerable<TKey> Keys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) {
            return source.Select(GetKey);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable<TKey, TValue>(
            this System.Collections.IDictionary source
        ) {
            foreach (System.Collections.DictionaryEntry entry in source) {
                yield return new KeyValuePair<TKey, TValue>((TKey)entry.Key, (TValue)entry.Value);
            }
        }

        private class TakeWhileCounter<T> {
            private ulong _remaining;

            public TakeWhileCounter(ulong count) {
                _remaining = count;
            }

            public bool ShouldTake(T value) {
                if (_remaining == 0) {
                    return false;
                }
                _remaining -= 1;
                return true;
            }
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, ulong count) {
            return source.TakeWhile(new TakeWhileCounter<T>(count).ShouldTake);
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, long count) {
            if (count > 0) {
                return source.TakeWhile(new TakeWhileCounter<T>((ulong)count).ShouldTake);
            }
            return Enumerable.Empty<T>();
        }
    }
}
