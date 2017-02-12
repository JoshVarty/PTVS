namespace Microsoft.CookiecutterTools.Model {
    struct ProcessOutputResult {
        public string ExeFileName;
        public int? ExitCode;
        public string[] StandardOutputLines;
        public string[] StandardErrorLines;
    }
}
