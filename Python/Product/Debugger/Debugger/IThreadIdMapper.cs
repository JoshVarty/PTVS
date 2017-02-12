namespace Microsoft.PythonTools.Debugger {
    /// <summary>
    /// Provides mapping between Python thread IDs (which can be 64-bit if running on a 64-bit Linux system), and
    /// VS 32-bit thread IDs (which are 32-bit, and are faked if a 64-bit Python ID does not fit into 32 bits).
    /// </summary>
    internal interface IThreadIdMapper {
        long? GetPythonThreadId(uint vsThreadId);
    }
}
