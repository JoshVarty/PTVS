using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Interpreter {
    class NoPackageManager : IPackageManager {
        public static readonly IPackageManager Instance = new NoPackageManager();

        private NoPackageManager() { }

        public bool IsReady => true;
        public IPythonInterpreterFactory Factory => null;

        public event EventHandler InstalledFilesChanged { add { } remove { } }
        public event EventHandler InstalledPackagesChanged { add { } remove { } }

        public event EventHandler IsReadyChanged { add { } remove { } }

        public Task<PackageSpec> GetInstalledPackageAsync(string name, CancellationToken cancellationToken) {
            return Task.FromResult(new PackageSpec());
        }

        public Task<IList<PackageSpec>> GetInstalledPackagesAsync(CancellationToken cancellationToken) {
            return Task.FromResult<IList<PackageSpec>>(Array.Empty<PackageSpec>());
        }

        public void NotifyPackagesChanged() { }

        public sealed class NoSuppression : IDisposable {
            public static readonly IDisposable Instance = new NoSuppression();
            private NoSuppression() { }
            void IDisposable.Dispose() { }
        }

        public IDisposable SuppressNotifications() => NoSuppression.Instance;

        public void SetInterpreterFactory(IPythonInterpreterFactory factory) { }

        public Task PrepareAsync(IPackageManagerUI ui, CancellationToken cancellationToken) {
            return Task.FromResult<object>(null);
        }

        public Task<bool> ExecuteAsync(string arguments, IPackageManagerUI ui, CancellationToken cancellationToken) {
            throw new NotSupportedException();
        }

        public Task<bool> InstallAsync(PackageSpec package, IPackageManagerUI ui, CancellationToken cancellationToken) {
            throw new NotSupportedException();
        }

        public Task<bool> UninstallAsync(PackageSpec package, IPackageManagerUI ui, CancellationToken cancellationToken) {
            throw new NotSupportedException();
        }

        public Task<PackageSpec> GetInstalledPackageAsync(PackageSpec package, CancellationToken cancellationToken) {
            return Task.FromResult(new PackageSpec());
        }

        public Task<IList<PackageSpec>> GetInstallablePackagesAsync(CancellationToken cancellationToken) {
            return Task.FromResult<IList<PackageSpec>>(Array.Empty<PackageSpec>());
        }

        public Task<PackageSpec> GetInstallablePackageAsync(PackageSpec package, CancellationToken cancellationToken) {
            return Task.FromResult(new PackageSpec());
        }
    }
}
