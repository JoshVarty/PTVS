using System.Text.RegularExpressions;

namespace Microsoft.CookiecutterTools.Model {
    class ParseUtils {
        public static bool ParseGitHubRepoOwnerAndName(string repoUrl, out string owner, out string name) {
            var m = Regex.Match(repoUrl, @"http(s)?://github\.com/(?<owner>.+?)/(?<name>.+?)(/|#|\?|$)");
            owner = m.Groups["owner"].Value;
            name = m.Groups["name"].Value;
            return m.Groups["owner"].Success && m.Groups["name"].Success;
        }
    }
}
