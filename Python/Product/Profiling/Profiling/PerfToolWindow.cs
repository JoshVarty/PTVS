using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.PythonTools.Profiling {
    [Guid(WindowGuid)]
    class PerfToolWindow : ToolWindowPane {
        internal const string WindowGuid = "328AF5EC-350F-4A96-B847-90F38B18E9BF";
        private SessionsNode _sessions;

        public PerfToolWindow() {
            ToolClsid = GuidList.VsUIHierarchyWindow_guid;
            Caption = Strings.PerformanceToolWindowTitle;
        }
        
        public override void OnToolWindowCreated() {
            base.OnToolWindowCreated();

            var frame = (IVsWindowFrame)Frame;
            object ouhw;
            ErrorHandler.ThrowOnFailure(frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView, out ouhw));

            // initialie w/ our hierarchy
            var hw = ouhw as IVsUIHierarchyWindow;
            _sessions = new SessionsNode((IServiceProvider)Package, hw);
            object punk;
            ErrorHandler.ThrowOnFailure(hw.Init(
                _sessions,
                (uint)(__UIHWINFLAGS.UIHWF_SupportToolWindowToolbars |
                __UIHWINFLAGS.UIHWF_InitWithHiddenParentRoot |
                __UIHWINFLAGS.UIHWF_HandlesCmdsAsActiveHierarchy),
                out punk
            ));

            // add our toolbar which  is defined in our VSCT file
            object otbh;
            ErrorHandler.ThrowOnFailure(frame.GetProperty((int)__VSFPROPID.VSFPROPID_ToolbarHost, out otbh));
            IVsToolWindowToolbarHost tbh = otbh as IVsToolWindowToolbarHost;
            Guid guidPerfMenuGroup = GuidList.guidPythonProfilingCmdSet;
            ErrorHandler.ThrowOnFailure(tbh.AddToolbar(VSTWT_LOCATION.VSTWT_TOP, ref guidPerfMenuGroup, PkgCmdIDList.menuIdPerfToolbar));
        }

        public SessionsNode Sessions {
            get {
                return _sessions;
            }
        }
    }
}
