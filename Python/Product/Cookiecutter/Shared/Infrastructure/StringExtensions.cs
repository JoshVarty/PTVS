using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.CookiecutterTools.Infrastructure {
    public static class StringExtensions {
#if DEBUG
        private static readonly Regex SubstitutionRegex = new Regex(
            @"\{(\d+)",
            RegexOptions.IgnorePatternWhitespace,
            TimeSpan.FromSeconds(1)
        );

        private static void ValidateFormatString(string str, int argCount) {
            foreach (Match m in SubstitutionRegex.Matches(str)) {
                int index = int.Parse(m.Groups[1].Value);
                if (index >= argCount) {
                    Debug.Fail(string.Format("Format string expects more than {0} args.\n\n{1}", argCount, str));
                }
            }
        }
#else
        [Conditional("DEBUG")]
        private static void ValidateFormatString(string str, int argCount) { }
#endif

        public static string FormatUI(this string str, object arg0) {
            ValidateFormatString(str, 1);
            return string.Format(CultureInfo.CurrentUICulture, str, arg0);
        }

        public static string FormatUI(this string str, object arg0, object arg1) {
            ValidateFormatString(str, 2);
            return string.Format(CultureInfo.CurrentUICulture, str, arg0, arg1);
        }

        public static string FormatUI(this string str, params object[] args) {
            ValidateFormatString(str, args.Length);
            return string.Format(CultureInfo.CurrentUICulture, str, args);
        }

        public static string FormatInvariant(this string str, object arg0) {
            ValidateFormatString(str, 1);
            return string.Format(CultureInfo.InvariantCulture, str, arg0);
        }

        public static string FormatInvariant(this string str, object arg0, object arg1) {
            ValidateFormatString(str, 2);
            return string.Format(CultureInfo.InvariantCulture, str, arg0, arg1);
        }

        public static string FormatInvariant(this string str, params object[] args) {
            ValidateFormatString(str, args.Length);
            return string.Format(CultureInfo.InvariantCulture, str, args);
        }

        public static bool IsTrue(this string str) {
            bool asBool;
            return !string.IsNullOrWhiteSpace(str) && (
                str.Equals("1") ||
                str.Equals("yes", StringComparison.InvariantCultureIgnoreCase) ||
                (bool.TryParse(str, out asBool) && asBool)
            );
        }

        public static string Truncate(this string str, int length) {
            if (string.IsNullOrEmpty(str)) {
                return str;
            }

            if (str.Length < length) {
                return str;
            }

            return str.Substring(0, length);
        }
    }
}
