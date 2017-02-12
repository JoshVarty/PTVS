namespace Microsoft.CookiecutterTools.Interpreters {
    class PythonInterpreterInformation {
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
    }
}
