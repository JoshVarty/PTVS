using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.ComponentModelHost;

namespace TestUtilities.Mocks {
    public class MockServiceProvider : IServiceProvider, IServiceContainer {
        public readonly Dictionary<Guid, object> Services = new Dictionary<Guid, object>();
        public readonly MockComponentModel ComponentModel = new MockComponentModel();

        public MockServiceProvider() {
            Services[typeof(SComponentModel).GUID] = ComponentModel;
        }

        public object GetService(Type serviceType) {
            object service;
            Console.WriteLine("MockServiceProvider.GetService({0})", serviceType.Name);
            Services.TryGetValue(serviceType.GUID, out service);
            return service;
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote) {
            Services[serviceType.GUID] = callback != null ? callback(this, serviceType) : null;
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback) {
            Services[serviceType.GUID] = callback != null ? callback(this, serviceType) : null;
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote) {
            Services[serviceType.GUID] = serviceInstance;
        }

        public void AddService(Type serviceType, object serviceInstance) {
            Services[serviceType.GUID] = serviceInstance;
        }

        public void RemoveService(Type serviceType, bool promote) {
            Services.Remove(serviceType.GUID);
        }

        public void RemoveService(Type serviceType) {
            Services.Remove(serviceType.GUID);
        }
    }
}
