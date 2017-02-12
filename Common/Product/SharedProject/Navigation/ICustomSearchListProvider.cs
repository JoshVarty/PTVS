using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudioTools.Navigation {
    interface ICustomSearchListProvider {
        IVsSimpleObjectList2 GetSearchList();
    }
}
