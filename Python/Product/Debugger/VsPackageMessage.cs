namespace Microsoft.PythonTools.DkmDebugger {
    public enum VsPackageMessage {
        None = 0,
        WarnAboutPythonSymbols = 1,
        // TODO: Evaluate if this is still necessary
        //WarnAboutPGO = 2,
        SetDebugOptions = 3,
    }
}
