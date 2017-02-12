using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.CookiecutterTools.Model;

namespace Microsoft.CookiecutterTools.ViewModel {
    class ContextItemViewModel : INotifyPropertyChanged {
        private string _name;
        private string _selector;
        private string _label;
        private string _description;
        private string _url;
        private string _val;
        private string _default;
        private readonly List<string> _items;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor for design view.
        /// </summary>
        public ContextItemViewModel() :
            this(null, Selectors.String, null, null, null, null, null) {
        }

        public ContextItemViewModel(string name, string selector, string label, string description, string url, string defaultValue, string[] items = null) {
            _name = name;
            _selector = selector;
            _label = !string.IsNullOrEmpty(label) ? label : name;
            _description = !string.IsNullOrEmpty(description) ? description : defaultValue;
            _url = url;
            _val = string.Empty;
            _default = defaultValue;
            _items = new List<string>();
            if (items != null && items.Length > 0) {
                _items.AddRange(items);
            }

            // These selectors don't have a way of showing the default value (watermark)
            // when no value is set (and there's no way to unset the value once it is set).
            // So we'll always start with the value set to default.
            if (selector == Selectors.YesNo || selector == Selectors.List) {
                _val = _default;
            }
        }

        public string Name {
            get {
                return _name;
            }

            set {
                _name = value;
            }
        }

        public string Selector {
            get {
                return _selector;
            }

            set {
                if (value != _selector) {
                    _selector = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selector)));
                }
            }
        }

        public string Label {
            get {
                return _label;
            }

            set {
                if (value != _label) {
                    _label = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
                }
            }
        }

        public string Description {
            get {
                return _description;
            }

            set {
                if (value != _description) {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        public string Url {
            get {
                return _url ?? string.Empty;
            }

            set {
                if (value != _url) {
                    _url = value ?? string.Empty;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Url)));
                }
            }
        }

        public string Val {
            get {
                return _val;
            }

            set {
                if (value != _val) {
                    _val = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Val)));
                }
            }
        }

        public string Default {
            get {
                return _default;
            }

            set {
                if (value != _default) {
                    _default = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Default)));
                }
            }
        }

        public List<string> Items {
            get {
                return _items;
            }
        }
    }
}
