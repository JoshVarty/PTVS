namespace Microsoft.PythonTools.Interpreter {
    public static class InterpreterRegistryConstants {
        public const string NoInterpretersFactoryId = "NoInterpreters";

        public static bool IsNoInterpretersFactory(string id) {
            return id == NoInterpretersFactoryId;
        }
    }
}
