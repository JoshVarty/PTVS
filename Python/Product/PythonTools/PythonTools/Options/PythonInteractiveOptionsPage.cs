namespace Microsoft.PythonTools.Options {
    class PythonInteractiveOptionsPage : PythonDialogPage {
        private PythonInteractiveOptionsControl _window;

        // replace the default UI of the dialog page w/ our own UI.
        protected override System.Windows.Forms.IWin32Window Window {
            get {
                if (_window == null) {
                    _window = new PythonInteractiveOptionsControl(Site);
                }
                return _window;
            }
        }

        public override void ResetSettings() {
            var opts = PyService.InteractiveOptions;
            opts.Reset();
            _window?.UpdateSettings();
        }

        public override void LoadSettingsFromStorage() {
            var opts = PyService.InteractiveOptions;
            opts.Load();
            _window?.UpdateSettings();
        }

        public override void SaveSettingsToStorage() {
            var opts = PyService.InteractiveOptions;
            opts.Save();
            _window?.UpdateSettings();
        }
    }
}
