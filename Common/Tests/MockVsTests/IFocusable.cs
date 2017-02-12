
namespace Microsoft.VisualStudioTools.MockVsTests {
    /// <summary>
    /// Implemented by mock VS objects which can gain and lose focus.
    /// 
    /// Only one item in mock VS will have focus at a time, and the
    /// current item is tracked by MockVs.
    /// </summary>
    interface IFocusable {
        void GetFocus();
        void LostFocus();
    }
}
