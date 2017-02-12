using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.CookiecutterTools.ViewModel {
    class CategorizedViewModel : INotifyPropertyChanged {
        private string _displayName;

        /// <summary>
        /// Constructor for design view.
        /// </summary>
        public CategorizedViewModel() :
            this(null) {
        }

        public CategorizedViewModel(string displayName) {
            _displayName = displayName;
        }

        public bool Selectable => false;

        public string DisplayName {
            get {
                return _displayName;
            }

            set {
                if (value != _displayName) {
                    _displayName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
                }
            }
        }

        public ObservableCollection<object> Templates { get; } = new ObservableCollection<object>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
