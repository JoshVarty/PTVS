using System.ComponentModel;

namespace Microsoft.CookiecutterTools.ViewModel {
    class ErrorViewModel : INotifyPropertyChanged {
        private string _errorDescription;
        private string _errorDetails;

        public event PropertyChangedEventHandler PropertyChanged;

        public ErrorViewModel() {
        }

        public bool Selectable => false;

        public string ErrorDescription {
            get {
                return _errorDescription;
            }

            set {
                if (value != _errorDescription) {
                    _errorDescription = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorDescription)));
                }
            }
        }

        public string ErrorDetails {
            get {
                return _errorDetails;
            }

            set {
                if (value != _errorDetails) {
                    _errorDetails = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorDetails)));
                }
            }
        }
    }
}
