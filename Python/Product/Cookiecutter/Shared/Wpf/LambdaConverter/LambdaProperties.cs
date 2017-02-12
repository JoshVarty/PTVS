using System;
using System.Windows;

namespace Microsoft.VisualStudioTools.Wpf {
    public static class LambdaProperties {
        public static readonly DependencyProperty ImportedNamespacesProperty = DependencyProperty.RegisterAttached(
            "ImportedNamespaces", typeof(string), typeof(LambdaProperties));

        public static string GetImportedNamespaces(object obj) {
            return null;
        }

        public static void SetImportedNamespaces(object obj, string value) {
        }
    }
}
