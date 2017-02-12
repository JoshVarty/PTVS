using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.PythonTools.Analysis.Browser {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static bool ExportDiffable, ExportTree, ExportPerPackage;
        public static string ExportPath;
        public static string ExportFilter;
        public static string InitialPath;
        public static string InitialVersion;

        private static Predicate<string> Is(string switchName) {
            var lowerS = switchName.ToLowerInvariant();
            return a => {
                if (a.StartsWith("/")) {
                    return string.Compare(a, 1, switchName, 0, switchName.Length, true) == 0;
                } else if (a.StartsWith("--")) {
                    return string.Compare(a, 2, switchName, 0, switchName.Length, true) == 0;
                } else {
                    return false;
                }
            };
        }

        private static IEnumerable<string> GetArgValues(IEnumerable<string> source, string switchName) {
            return source
                .Where(Is(switchName).Invoke)
                .Select(a => {
                    if (a.StartsWith("/")) {
                        return a.Substring(switchName.Length + 2);
                    } else if (a.StartsWith("--")) {
                        return a.Substring(switchName.Length + 3);
                    }
                    return null;
                })
                .Where(s => !string.IsNullOrEmpty(s));
        }

        [STAThread]
        public static void Main() {
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();

            ExportTree = args.RemoveAll(Is("tree")) > 0;
            ExportDiffable = args.RemoveAll(Is("diff")) > 0;
            ExportPerPackage = args.RemoveAll(Is("perpackage")) > 0;

            ExportFilter = GetArgValues(args, "filter:").FirstOrDefault();
            args.RemoveAll(Is("filter:"));

            InitialPath = args.ElementAtOrDefault(0);
            InitialVersion = args.ElementAtOrDefault(1);
            ExportPath = args.ElementAtOrDefault(2);

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
