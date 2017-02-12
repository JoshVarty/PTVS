using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Html.Package.Package.Common;

namespace Microsoft.PythonTools.Django {
    [Guid(GuidList.guidDjangoEditorFactoryString)]
    public class DjangoEditorFactory : WebEditorFactory {
        public DjangoEditorFactory(DjangoPackage package)
            : base(package, GuidList.guidDjangoEditorFactory, typeof(DjangoLanguageInfo).GUID) {
        }
    }
}
