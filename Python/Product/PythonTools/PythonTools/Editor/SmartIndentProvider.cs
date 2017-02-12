using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Editor {
    [Export(typeof(ISmartIndentProvider))]
    [ContentType(PythonCoreConstants.ContentType)]
    public sealed class SmartIndentProvider : ISmartIndentProvider {
        private readonly PythonToolsService _pyService;

        [ImportingConstructor]
        internal SmartIndentProvider([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider) {
            _pyService = serviceProvider.GetPythonToolsService();
        }

        private sealed class Indent : ISmartIndent {
            private readonly ITextView _textView;
            private readonly SmartIndentProvider _provider;

            public Indent(SmartIndentProvider provider, ITextView view) {
                _provider = provider;
                _textView = view;
            }

            public int? GetDesiredIndentation(ITextSnapshotLine line) {
                if (_provider._pyService.LangPrefs.IndentMode == vsIndentStyle.vsIndentStyleSmart) {
                    return AutoIndent.GetLineIndentation(line, _textView);
                } else {
                    return null;
                }
            }

            public void Dispose() {
            }
        }

        public ISmartIndent CreateSmartIndent(ITextView textView) {
            return new Indent(this, textView);
        }
    }
}
