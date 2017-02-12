namespace Microsoft.PythonTools.Interpreter {
    class PythonInterpreterInformation {
        IPythonInterpreterFactory Factory;
        public readonly InterpreterConfiguration Configuration;
        public readonly string Vendor;
        public readonly string VendorUrl;
        public readonly string SupportUrl;

        public PythonInterpreterInformation(
            InterpreterConfiguration configuration,
            string vendor,
            string vendorUrl,
            string supportUrl
        ) {
            Configuration = configuration;
            Vendor = vendor;
            VendorUrl = vendorUrl;
            SupportUrl = supportUrl;
        }

        public IPythonInterpreterFactory EnsureFactory() {
            if (Factory == null) {
                lock (this) {
                    if (Factory == null) {
                        Factory = InterpreterFactoryCreator.CreateInterpreterFactory(
                            Configuration,
                            new InterpreterFactoryCreationOptions {
                                PackageManager = new PipPackageManager(),
                                WatchFileSystem = true
                            }
                        );
                    }
                }
            }
            return Factory;
        }
    }
}
