namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Specifies creation options for an interpreter factory.
    /// </summary>
    public sealed class InterpreterFactoryCreationOptions {
        public InterpreterFactoryCreationOptions() {
        }

        public InterpreterFactoryCreationOptions Clone() {
            return (InterpreterFactoryCreationOptions)MemberwiseClone();
        }

        public bool WatchFileSystem { get; set; }

        public string DatabasePath { get; set; }

        public IPackageManager PackageManager { get; set; }
    }
}
