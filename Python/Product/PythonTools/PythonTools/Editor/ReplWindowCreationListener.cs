using System;
using System.ComponentModel.Composition;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.Language;
using Microsoft.PythonTools.Repl;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.PythonTools.InteractiveWindow;
using Microsoft.PythonTools.InteractiveWindow.Shell;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using IOleCommandTarget = Microsoft.VisualStudio.OLE.Interop.IOleCommandTarget;

namespace Microsoft.PythonTools.Editor {
    [Export(typeof(IVsInteractiveWindowOleCommandTargetProvider))]
    [ContentType(PythonCoreConstants.ContentType)]
    public class ReplWindowCreationListener : IVsInteractiveWindowOleCommandTargetProvider {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentModel _componentModel;

        [ImportingConstructor]
        public ReplWindowCreationListener([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _componentModel = _serviceProvider.GetComponentModel();
        }

        public IOleCommandTarget GetCommandTarget(IWpfTextView textView, IOleCommandTarget nextTarget) {
            var window = textView.TextBuffer.GetInteractiveWindow();

            var controller = IntellisenseControllerProvider.GetOrCreateController(
                _serviceProvider,
                _componentModel,
                textView
            );
            controller._oldTarget = nextTarget;

            var editFilter = EditFilter.GetOrCreate(_serviceProvider, _componentModel, textView, controller);

            if (window == null) {
                return editFilter;
            }

            textView.Properties[IntellisenseController.SuppressErrorLists] = IntellisenseController.SuppressErrorLists;
            return ReplEditFilter.GetOrCreate(_serviceProvider, _componentModel, textView, editFilter);
        }
    }
}
