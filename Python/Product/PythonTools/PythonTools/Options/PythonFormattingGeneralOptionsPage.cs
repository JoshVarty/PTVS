using System;
using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Options {
    [ComVisible(true)]
    public class PythonFormattingGeneralOptionsPage : PythonDialogPage {
        private PythonFormattingGeneralOptionsControl _window;

        // replace the default UI of the dialog page w/ our own UI.
        protected override System.Windows.Forms.IWin32Window Window {
            get {
                if (_window == null) {
                    _window = new PythonFormattingGeneralOptionsControl();
                    LoadSettingsFromStorage();
                }
                return _window;
            }
        }

        public override void ResetSettings() {
            PyService.AdvancedOptions.Reset();
        }

        public override void LoadSettingsFromStorage() {
            // Load settings from storage.
            PyService.AdvancedOptions.Load();
            
            // Synchronize UI with backing properties.
            if (_window != null) {
                _window.SyncControlWithPageSettings(PyService);
            }
        }

        public override void SaveSettingsToStorage() {
            // Synchronize backing properties with UI.
            if (_window != null) {
                _window.SyncPageWithControlSettings(PyService);
            }

            // Save settings.
            PyService.AdvancedOptions.Save();
        }
    }
}
