using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Microsoft.PythonTools.EnvironmentsList {
    [ValueConversion(typeof(string), typeof(string))]
    sealed class FileNameEllipsisConverter : IValueConverter {
        private static readonly char[] PathSeparators = new [] {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        public bool IncludeHead { get; set; }
        public bool IncludeBody { get; set; }
        public bool IncludeTail { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var path = value as string;
            if (string.IsNullOrEmpty(path) || IncludeHead && IncludeBody && IncludeTail) {
                return path;
            }

            var headSplit = path.IndexOfAny(PathSeparators) + 1;
            if (headSplit == path.Length) {
                headSplit = -1;
            }

            var tailSplit = path.LastIndexOfAny(PathSeparators);
            if (tailSplit == path.Length - 1) {
                if (path.Length > 1) {
                    tailSplit = path.LastIndexOfAny(PathSeparators, tailSplit - 1);
                } else {
                    tailSplit = 0;
                }
            }

            if (headSplit == tailSplit + 1) {
                headSplit -= 1;
            }

            var head = (headSplit > 0) ? path.Remove(headSplit) : string.Empty;
            var tail = (tailSplit > 0) ? path.Substring(tailSplit) : path;

            var body = string.Empty;
            if (tailSplit > headSplit) {
                if (headSplit > 0) {
                    body = path.Substring(headSplit, tailSplit - headSplit);
                } else {
                    body = path.Remove(tailSplit);
                }
            } else if (tailSplit < headSplit) {
                Debug.Assert(headSplit > 0);
                body = path.Substring(Math.Max(headSplit, 0));
            }

            var result = string.Empty;
            if (IncludeHead) {
                result += head;
            }
            if (IncludeBody) {
                result += body;
            }
            if (IncludeTail) {
                result += tail;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
