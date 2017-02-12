using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Web.Editor.Controller;
using Microsoft.Web.Editor.Services;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class TemplateMainController : ViewController {
        public TemplateMainController(ITextView textView, ITextBuffer textBuffer)
            : base(textView, textBuffer) {
            ServiceManager.AddService<TemplateMainController>(this, textView);
        }

        protected override void Dispose(bool disposing) {
            ServiceManager.RemoveService<TemplateMainController>(TextView);
            base.Dispose(disposing);
        }
    }
}
