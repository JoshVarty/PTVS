using Microsoft.VisualStudio.Language.NavigateTo.Interfaces;

namespace Microsoft.PythonTools.Navigation.NavigateTo {
    internal class PythonNavigateToItemDisplayFactory : INavigateToItemDisplayFactory {
        public static readonly PythonNavigateToItemDisplayFactory Instance = new PythonNavigateToItemDisplayFactory();

        public INavigateToItemDisplay CreateItemDisplay(NavigateToItem item) {
            return new PythonNavigateToItemDisplay(item);
        }
    }
}
