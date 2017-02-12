using System;
using System.Threading;
using System.Windows;
using Microsoft.PythonTools.Intellisense;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Navigation {
    /// <summary>
    /// Interaction logic for WaitForCompleteAnalysisDialog.xaml
    /// </summary>
    partial class WaitForCompleteAnalysisDialog : DialogWindowVersioningWorkaround {
        private VsProjectAnalyzer _analyzer;

        public WaitForCompleteAnalysisDialog(VsProjectAnalyzer analyzer) {
            _analyzer = analyzer;
            InitializeComponent();

            new Thread(AnalysisComplete).Start();
        }

        private void _cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }

        private void AnalysisComplete() {
            _analyzer.WaitForCompleteAnalysis(UpdateItemsRemaining);
        }

        private bool UpdateItemsRemaining(int itemsLeft) {
            if (itemsLeft == 0) {
                Dispatcher.Invoke((Action)(() => {
                    this.DialogResult = true;
                    this.Close();
                }));
                return false;
            }
            
            bool? dialogResult = null;
            Dispatcher.Invoke((Action)(() => {
                dialogResult = DialogResult;
                if (dialogResult == null) {
                    _progress.Maximum = itemsLeft;
                }
            }));
            

            return dialogResult == null;
        }
    }
}
