using System;
using Microsoft.PythonTools.Analysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.PythonTools.Infrastructure {
    /// <summary>
    /// Provides access to the DesignerContext and WpfEventBindingProvider.
    /// </summary>
    public interface IXamlDesignerSupport {
        Guid DesignerContextTypeGuid { get; }
        object CreateDesignerContext();
        void InitializeEventBindingProvider(object designerContext, IXamlDesignerCallback callback);
    }

    public interface IXamlDesignerCallback {
        ITextView TextView {
            get;
        }
        ITextBuffer Buffer {
            get;
        }

        InsertionPoint GetInsertionPoint(string className);

        string[] FindMethods(string className, int? paramCount);

        MethodInformation GetMethodInfo(string className, string methodName);
    }

    public sealed class InsertionPoint {
        public readonly int Location, Indentation;
        public InsertionPoint(int location, int indentation) {
            Location = location;
            Indentation = indentation;
        }
    }

    public sealed class MethodInformation {
        public readonly bool IsFound;
        public readonly int Start, End;

        public MethodInformation(int start, int end, bool found) {
            Start = start;
            End = end;
            IsFound = found;
        }
    }

}
