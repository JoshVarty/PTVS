using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.NavigateTo.Interfaces;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Navigation.NavigateTo {
    [Export(typeof(INavigateToItemProviderFactory))]
    internal class PythonNavigateToItemProviderFactory : INavigateToItemProviderFactory {
        private readonly IGlyphService _glyphService;

        [ImportingConstructor]
        public PythonNavigateToItemProviderFactory(IGlyphService glyphService) {
            _glyphService = glyphService;
        }

        public bool TryCreateNavigateToItemProvider(IServiceProvider serviceProvider, out INavigateToItemProvider provider) {
            var shell = serviceProvider.GetShell();
            var guid = GuidList.guidPythonToolsPackage;
            IVsPackage pkg;
            if (shell.IsPackageLoaded(ref guid, out pkg) == VSConstants.S_OK && pkg != null) {
                provider = serviceProvider.GetUIThread().Invoke(() => {
                    return new PythonNavigateToItemProvider(serviceProvider, _glyphService);
                });
                return true;
            }

            // Not loaded, so nothing to provide
            provider = null;
            return false;
        }
    }
}
