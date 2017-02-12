using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis {
    sealed class XamlProjectEntry : IXamlProjectEntry {
        private XamlAnalysis _analysis;
        private readonly string _filename;
        private int _version;
        private string _content;
        private Dictionary<object, object> _properties;
        private readonly HashSet<IProjectEntry> _dependencies = new HashSet<IProjectEntry>();

        public XamlProjectEntry(string filename) {
            _filename = filename;
        }

        public void ParseContent(TextReader content, IAnalysisCookie fileCookie) {
            _content = content.ReadToEnd();
        }

        public void AddDependency(IProjectEntry projectEntry) {
            lock (_dependencies) {
                _dependencies.Add(projectEntry);
            }
        }

        #region IProjectEntry Members

        public bool IsAnalyzed {
            get { return _analysis != null; }
        }

        public void Analyze(CancellationToken cancel) {
            if (cancel.IsCancellationRequested) {
                return;
            }

            lock (this) {
                _analysis = new XamlAnalysis(new StringReader(_content));

                _version++;

                // update any .py files which depend upon us.
                for (var deps = GetNewDependencies(null); deps.Any(); deps = GetNewDependencies(deps)) {
                    foreach (var dep in deps) {
                        dep.Analyze(cancel);
                    }
                }
            }
        }

        private HashSet<IProjectEntry> GetNewDependencies(HashSet<IProjectEntry> oldDependencies) {
            HashSet<IProjectEntry> deps;
            lock (_dependencies) {
                deps = new HashSet<IProjectEntry>(_dependencies);
            }

            if (oldDependencies != null) {
                deps.ExceptWith(oldDependencies);
            }

            return deps;
        }

        public string FilePath { get { return _filename; } }

        public int AnalysisVersion {
            get {
                return _version;
            }
        }

        public Dictionary<object, object> Properties {
            get {
                if (_properties == null) {
                    _properties = new Dictionary<object, object>();
                }
                return _properties;
            }
        }

        public IModuleContext AnalysisContext {
            get { return null; }
        }

        public void RemovedFromProject() { }

        #endregion

        #region IXamlProjectEntry Members

        public XamlAnalysis Analysis {
            get { return _analysis; }
        }

        #endregion

    }

    public interface IXamlProjectEntry : IExternalProjectEntry {
        XamlAnalysis Analysis {
            get;
        }
    }
}
