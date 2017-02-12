using System.Windows;

namespace Microsoft.PythonTools.EnvironmentsList {
    public interface IEnvironmentViewExtension {
        int SortPriority { get; }

        string LocalizedDisplayName { get; }

        FrameworkElement WpfObject { get; }

        object HelpContent { get; }
    }
}
