using System;
using System.ComponentModel.Design;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.Settings;

namespace Microsoft.PythonTools.Options {
    class PythonToolsOptionsService : IPythonToolsOptionsService {
        private const string _optionsKey = "Options";
        private readonly WritableSettingsStore _settingsStore;

        public static object CreateService(IServiceContainer container, Type serviceType) {
            if (serviceType.IsEquivalentTo(typeof(IPythonToolsOptionsService))) {
                return new PythonToolsOptionsService(container);
            }
            return null;
        }

        private PythonToolsOptionsService(IServiceProvider serviceProvider) {
            var settingsManager = SettingsManagerCreator.GetSettingsManager(serviceProvider);
            _settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
        }

        public void SaveString(string name, string category, string value) {
            var path = GetCollectionPath(category);
            if (value == null) {
                if (_settingsStore.CollectionExists(path)) {
                    _settingsStore.DeleteProperty(path, name);
                }
            } else {
                if (!_settingsStore.CollectionExists(path)) {
                    _settingsStore.CreateCollection(path);
                }
                _settingsStore.SetString(path, name, value);
            }
        }

        private static string GetCollectionPath(string category) {
            return PythonCoreConstants.BaseRegistryKey + "\\" + _optionsKey + "\\" + category;
        }

        public string LoadString(string name, string category) {
            var path = GetCollectionPath(category);
            if (!_settingsStore.CollectionExists(path)) {
                return null;
            }
            if (!_settingsStore.PropertyExists(path, name)) {
                return null;
            }
            return _settingsStore.GetString(path, name, "");
        }

        public void DeleteCategory(string category) {
            var path = GetCollectionPath(category);
            try {
                _settingsStore.DeleteCollection(path);
            } catch (ArgumentException) {
                // Documentation is a lie - raises ArgumentException if the
                // collection does not exist.
            }
        }
    }
}
