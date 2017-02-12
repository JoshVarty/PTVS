
namespace Microsoft.PythonTools.Parsing {
    public class ErrorSink {
        public static readonly ErrorSink Null = new ErrorSink();
        
        public virtual void Add(string message, NewLineLocation[] lineLocations, int startIndex, int endIndex, int errorCode, Severity severity) {
        }
    }
}
