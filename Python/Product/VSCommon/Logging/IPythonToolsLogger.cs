namespace Microsoft.PythonTools.Logging {
    /// <summary>
    /// Provides an interface for logging events and statistics inside of PTVS.
    /// 
    /// Multiple loggers can be created which send stats to different locations.
    /// 
    /// By default there is one logger which shows the stats in 
    /// Tools->Python Tools->Diagnostic Info.
    /// </summary>
    public interface IPythonToolsLogger {
        /// <summary>
        /// Informs the logger of an event.  Unknown events should be ignored.
        /// </summary>
        void LogEvent(PythonLogEvent logEvent, object argument);
    }
}
