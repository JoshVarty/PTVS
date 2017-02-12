using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(IUIElementProvider<CompletionSet, ICompletionSession>))]
    [Name("Python Completion UI Provider")]
    [Order(Before="Default Completion Presenter")]
    [ContentType(PythonCoreConstants.ContentType)]
    internal class CompletionUIElementProvider : IUIElementProvider<CompletionSet, ICompletionSession> {
        [ImportMany]
        internal List<Lazy<IUIElementProvider<CompletionSet, ICompletionSession>, IOrderableContentTypeMetadata>> UnOrderedCompletionSetUIElementProviders { get; set; }
        private static readonly bool _isPreSp1 = CheckPreSp1();
        private bool _gettingUIElement;
        private readonly IServiceProvider _serviceProvider;

        private static bool CheckPreSp1() {
            var attrs = typeof(VSConstants).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            if (attrs.Length > 0 && ((AssemblyFileVersionAttribute)attrs[0]).Version == "10.0.30319.1") {
                // http://pytools.codeplex.com/workitem/537
                // http://connect.microsoft.com/VisualStudio/feedback/details/550886/visual-studio-2010-crash-when-the-source-file-contains-non-unicode-characters
                // pre-SP1 cannot handle us wrapping this up, so just don't offer this functionality pre-SP1.
                return true;
            }
            return false;
        }

        [ImportingConstructor]
        public CompletionUIElementProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public UIElement GetUIElement(CompletionSet itemToRender, ICompletionSession context, UIElementType elementType) {
            if (_gettingUIElement) {
                return null;
            }
            _gettingUIElement = true;
            try {
                var orderedProviders = Orderer.Order(UnOrderedCompletionSetUIElementProviders);
                var pyContentType = _serviceProvider.GetPythonContentType();
                foreach (var presenterProviderExport in orderedProviders) {

                    foreach (var contentType in presenterProviderExport.Metadata.ContentTypes) {
                        if (pyContentType.IsOfType(contentType)) {
                            if (presenterProviderExport.Value.GetType() == typeof(CompletionUIElementProvider)) {
                                // don't forward to ourselves...
                                continue;
                            }

                            var res = presenterProviderExport.Value.GetUIElement(itemToRender, context, elementType);
                            if (res != null) {
                                if (_isPreSp1) {
                                    return res;
                                }

                                return new CompletionControl(_serviceProvider, res, context);
                            }
                        }
                    }
                }

                return null;
            } finally {
                _gettingUIElement = false;
            }
        }
    }
}
