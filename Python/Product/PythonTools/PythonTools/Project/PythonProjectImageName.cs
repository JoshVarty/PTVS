namespace Microsoft.PythonTools.Project {
    enum PythonProjectImageName {
        File = 0,
        Project = 1,
        SearchPathContainer,
        SearchPath,
        MissingSearchPath,
        StartupFile,
        Interpreter,
        MissingInterpreter,
        ActiveInterpreter,
        InterpretersPackage,
        InterpretersContainer = Interpreter
    }
}
