using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Windows.Design.Host;

namespace Microsoft.PythonTools.XamlDesignerSupport {
    /// <summary>
    /// Provides access to the DesignerContext and WpfEventBindingProvider assuming that functionality
    /// is installed into VS.  If it's not installed then this becomes a nop and DesignerContextType
    /// returns null;
    /// </summary>
    [Export(typeof(IXamlDesignerSupport))]
    class XamlDesignerSupport : IXamlDesignerSupport {
        private readonly Lazy<Guid> _DesignerContextTypeGuid = new Lazy<Guid>(() => {
            try {
                return typeof(DesignerContext).GUID;
            } catch {
                return Guid.Empty;
            }
        });

        public Guid DesignerContextTypeGuid => _DesignerContextTypeGuid.Value;

        public object CreateDesignerContext() {
            var context = new DesignerContext();
            //Set the RuntimeNameProvider so the XAML designer will call it when items are added to
            //a design surface. Since the provider does not depend on an item context, we provide it at 
            //the project level.
            // This is currently disabled because we don't successfully serialize to the remote domain
            // and the default name provider seems to work fine.  Likely installing our assembly into
            // the GAC or implementing an IsolationProvider would solve this.
            //context.RuntimeNameProvider = new PythonRuntimeNameProvider();
            return context;
        }

        public void InitializeEventBindingProvider(object designerContext, IXamlDesignerCallback callback) {
            Debug.Assert(designerContext is DesignerContext);
            ((DesignerContext)designerContext).EventBindingProvider = new WpfEventBindingProvider(callback);
        }
    }
}
