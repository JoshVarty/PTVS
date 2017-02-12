namespace Microsoft.PythonTools.Interpreter {
    public static class BuiltInPackageManagers {
        /// <summary>
        /// Gets an instance of a package manager based on pip.
        /// </summary>
        public static IPackageManager Pip => new PipPackageManager(extraInterpreterArgs: new[] { "-E" });

        /// <summary>
        /// Gets an instance of a package manager based on pip that runs with
        /// the -X:Frames option
        /// </summary>
        public static IPackageManager PipXFrames => new PipPackageManager(extraInterpreterArgs: new[] { "-E", "-X:Frames" });
    }
}
