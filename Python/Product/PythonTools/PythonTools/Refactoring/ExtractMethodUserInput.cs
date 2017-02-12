using System;
using System.Windows;

namespace Microsoft.PythonTools.Refactoring {
    class ExtractMethodUserInput : IExtractMethodInput {
        private readonly IServiceProvider _serviceProvider;

        public ExtractMethodUserInput(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public bool ShouldExpandSelection() {
            var res = MessageBox.Show(Strings.ExtractMethod_ShouldExpandSelection,
                        Strings.ExtractMethod_ShouldExpandSelectionTitle,
                        MessageBoxButton.YesNo
                    );

            return res == MessageBoxResult.Yes;
        }


        public ExtractMethodRequest GetExtractionInfo(ExtractedMethodCreator previewer) {
            var requestView = new ExtractMethodRequestView(_serviceProvider, previewer);
            var dialog = new ExtractMethodDialog(requestView);

            bool res = dialog.ShowModal() ?? false;
            if (res) {
                return requestView.GetRequest();
            }

            return null;
        }

        public void CannotExtract(string reason) {
            MessageBox.Show(reason, Strings.ExtractMethod_CannotExtractMethod, MessageBoxButton.OK);
        }
    }
}
