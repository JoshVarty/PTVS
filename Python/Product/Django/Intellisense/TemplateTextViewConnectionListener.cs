using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.ContainedLanguage;
using Microsoft.Web.Editor.Controller;
using Microsoft.Web.Editor.Host;
using Microsoft.Web.Editor.Services;

namespace Microsoft.PythonTools.Django.Intellisense {
    [Export(typeof(IWpfTextViewConnectionListener))]
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType(TemplateTagContentType.ContentTypeName)]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    [Name("Django Template Text View Connection Listener")]
    [Order(Before = "default")]
    internal class TemplateTextViewConnectionListener : TextViewConnectionListener {
        protected override void OnTextViewConnected(ITextView textView, ITextBuffer textBuffer) {
            var mainController = ServiceManager.GetService<TemplateMainController>(textView) ??
                new TemplateMainController(textView, textBuffer);

            if (textBuffer != textView.TextBuffer) {
                var containedLanguageHost = ContainedLanguageHost.GetHost(textView, textBuffer);
                if (containedLanguageHost != null) {
                    object nextFilter = containedLanguageHost.SetContainedCommandTarget(textView, mainController);
                    mainController.ChainedController = WebEditor.TranslateCommandTarget(textView, nextFilter);
                }
            }

            base.OnTextViewConnected(textView, textBuffer);
        }

        protected override void OnTextViewDisconnected(ITextView textView, ITextBuffer textBuffer) {
            if (textBuffer != textView.TextBuffer) {
                var containedLanguageHost = ContainedLanguageHost.GetHost(textView, textBuffer);
                if (containedLanguageHost != null) {
                    containedLanguageHost.RemoveContainedCommandTarget(textView);
                }
            }

            base.OnTextViewDisconnected(textView, textBuffer);
        }
    }
}