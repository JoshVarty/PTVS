using System;

namespace Microsoft.CookiecutterTools.Model {
    class SearchUtils {
        internal static string[] ParseKeywords(string filter) {
            if (filter != null) {
                return filter.Split(new char[] { ' ', '\t', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            } else {
                return null;
            }
        }

        internal static bool SearchMatches(string[] keywords, Template template) {
            return SearchMatches(keywords, template.Name) || SearchMatches(keywords, template.Description);
        }

        private static bool SearchMatches(string[] keywords, string text) {
            if (text == null) {
                return false;
            }

            if (keywords == null || keywords.Length == 0) {
                return true;
            }

            foreach (var keyword in keywords) {
                if (text.Contains(keyword)) {
                    return true;
                }
            }

            return false;
        }

    }
}
