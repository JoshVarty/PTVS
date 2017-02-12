namespace Microsoft.PythonTools.Logging {
    /// <summary>
    /// Main entry point for logging events.  A single instance of this logger is created
    /// by our package and can be used to dispatch log events to all installed loggers.
    /// </summary>
    class PythonToolsLogger : IPythonToolsLogger {
        private readonly IPythonToolsLogger[] _loggers;

        public PythonToolsLogger(IPythonToolsLogger[] loggers) {
            _loggers = loggers;
        }
        
        public void LogEvent(PythonLogEvent logEvent, object data = null) {
            foreach (var logger in _loggers) {
                logger.LogEvent(logEvent, data);
            }
        }
    }
}
