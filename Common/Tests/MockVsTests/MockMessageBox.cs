namespace Microsoft.VisualStudioTools.MockVsTests {
    class MockMessageBox : MockDialog {
        public readonly string Text;

        public MockMessageBox(MockVs vs, string title, string text) : base(vs, title) {
            Text = text;
        }
    }
}
