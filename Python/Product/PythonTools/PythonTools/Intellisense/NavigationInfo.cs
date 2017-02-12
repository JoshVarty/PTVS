using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    class NavigationInfo {
        public readonly string Name;
        public readonly SnapshotSpan Span;
        public readonly NavigationInfo[] Children;
        public readonly NavigationKind Kind;        

        public NavigationInfo(string name, NavigationKind kind, SnapshotSpan span, NavigationInfo[] children) {
            Name = name;
            Kind = kind;
            Span = span;
            Children = children;
        }
    }

    public enum NavigationKind {
        None,
        Class,
        Function,
        StaticMethod,
        ClassMethod,
        Property
    }
}
