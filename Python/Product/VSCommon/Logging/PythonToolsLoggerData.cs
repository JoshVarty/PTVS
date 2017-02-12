using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.Telemetry;

namespace Microsoft.PythonTools.Logging {
    /// <summary>
    /// Provides a base class for logging complicated event data.
    /// </summary>
    public abstract class PythonToolsLoggerData {
        public static IDictionary<string, object> AsDictionary(object obj) {
            IDictionary<string, object> res;

            if (obj == null) {
                return null;
            }

            if ((res = obj as IDictionary<string, object>) != null) {
                return res;
            }

            if (!(obj is PythonToolsLoggerData)) {
                return null;
            }

            res = new Dictionary<string, object>();
            foreach (var propInfo in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                try {
                    var value = propInfo.GetValue(obj);
                    if (propInfo.GetCustomAttributes().OfType<PiiPropertyAttribute>().Any()) {
                        value = new TelemetryPiiProperty(value);
                    }
                    res[propInfo.Name] = value;
                } catch (Exception ex) {
                    Debug.Fail(ex.ToUnhandledExceptionMessage(typeof(PythonToolsLoggerData)));
                }
            }
            return res;
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PiiPropertyAttribute : Attribute {
        public PiiPropertyAttribute() { }
    }

    internal sealed class PackageInfo : PythonToolsLoggerData {
        [PiiProperty]
        public string Name { get; set; }
    }

    internal sealed class AnalysisInfo : PythonToolsLoggerData {
        [PiiProperty]
        public string InterpreterId { get; set; }
        public int AnalysisSeconds { get; set; }
    }

    internal sealed class LaunchInfo : PythonToolsLoggerData {
        public bool IsDebug { get; set; }
        public bool IsWeb { get; set; }
        public string Version { get; set; }
    }
}
