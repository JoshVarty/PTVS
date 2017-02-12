using System;
using System.Globalization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Microsoft.VisualStudioTools.TestAdapter {
    internal static class ServiceProviderExtensions {
        public static T GetService<T>(this IServiceProvider serviceProvider)
            where T : class {
            return serviceProvider.GetService<T>(typeof(T));
        }

        public static T GetService<T>(this IServiceProvider serviceProvider, Type serviceType)
            where T : class {
            ValidateArg.NotNull(serviceProvider, "serviceProvider");
            ValidateArg.NotNull(serviceType, "serviceType");

            var serviceInstance = serviceProvider.GetService(serviceType) as T;
            if (serviceInstance == null) {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, serviceType.Name));
            }

            return serviceInstance;
        }
    }
}
