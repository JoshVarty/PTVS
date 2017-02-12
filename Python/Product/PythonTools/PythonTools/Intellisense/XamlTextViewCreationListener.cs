using System;
using System.ComponentModel.Composition;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Watches for text views to be created for xaml code.  Then wires up to support analysis so that
    /// we can use the analysis for completion in .py code.
    /// </summary>
    [Export(typeof(IVsTextViewCreationListener))]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    [ContentType("xaml")]
    class XamlTextViewCreationListener : IVsTextViewCreationListener {
        internal readonly IVsEditorAdaptersFactoryService AdapterService;
        private readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public XamlTextViewCreationListener(
            [Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider,
            IVsEditorAdaptersFactoryService adapterService
        ) {
            _serviceProvider = serviceProvider;
            AdapterService = adapterService;
        }

        public void VsTextViewCreated(VisualStudio.TextManager.Interop.IVsTextView textViewAdapter) {
            // TODO: We should probably only track text views in Python projects or loose files.
            ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            
            if (textView != null) {
                var analyzer = _serviceProvider.GetProjectFromFile(textView.GetFilePath())?.GetAnalyzer();
                if (analyzer != null) {
                    var monitorResult = analyzer.MonitorTextBufferAsync(textView.TextBuffer)
                        .ContinueWith(
                            task => {
                                textView.Closed += TextView_Closed;
                                lock(task.Result) {
                                    task.Result.AttachedViews++;
                                }
                            }
                        );
                }
            }
        }

        private void TextView_Closed(object sender, EventArgs e) {
            var textView = (ITextView)sender;

            var analysis = textView.GetAnalysisEntry(textView.TextBuffer, _serviceProvider);
            if (analysis != null) {
                analysis.Analyzer.BufferDetached(analysis, textView.TextBuffer);
            }
            
            textView.Closed -= TextView_Closed;
        }
    }
}

