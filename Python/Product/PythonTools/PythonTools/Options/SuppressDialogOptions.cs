using System;

namespace Microsoft.PythonTools.Options {
    public static class SuppressDialog {
        public const string Category = "SuppressDialog";

        public const string SwitchEvaluatorSetting = "SwitchEvaluator";
        public const string PublishToAzure30Setting = "PublishToAzure30";
    }

    sealed class SuppressDialogOptions {
        private readonly PythonToolsService _service;

        internal SuppressDialogOptions(PythonToolsService service) {
            _service = service;
            Load();
        }

        public void Load() {
            SwitchEvaluator = _service.LoadString(SuppressDialog.SwitchEvaluatorSetting, SuppressDialog.Category);
            PublishToAzure30 = _service.LoadString(SuppressDialog.PublishToAzure30Setting, SuppressDialog.Category);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void Save() {
            _service.SaveString(SuppressDialog.SwitchEvaluatorSetting, SuppressDialog.Category, SwitchEvaluator);
            _service.SaveString(SuppressDialog.PublishToAzure30Setting, SuppressDialog.Category, PublishToAzure30);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void Reset() {
            SwitchEvaluator = null;
            PublishToAzure30 = null;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Changed;

        public string SwitchEvaluator { get; set; }
        public string PublishToAzure30 { get; set; }
    }
}
