using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace TestUtilities.Python {
    public class MockPythonProjectEntry : IPythonProjectEntry {
        public PythonAst Tree {
            get;
            set;
        }

        public string ModuleName {
            get;
            set;
        }

        public ModuleAnalysis Analysis {
            get;
            set;
        }

        public event EventHandler<EventArgs> OnNewParseTree { add { } remove { } }

        public event EventHandler<EventArgs> OnNewAnalysis { add { } remove { } }

        public void BeginParsingTree() {
            throw new NotImplementedException();
        }

        public void UpdateTree(PythonAst ast, IAnalysisCookie fileCookie) {
            throw new NotImplementedException();
        }

        public void GetTreeAndCookie(out PythonAst ast, out IAnalysisCookie cookie) {
            throw new NotImplementedException();
        }

        public PythonAst WaitForCurrentTree(int timeout = -1) {
            throw new NotImplementedException();
        }

        public void Analyze(System.Threading.CancellationToken cancel, bool enqueueOnly) {
            throw new NotImplementedException();
        }

        public IGroupableAnalysisProject AnalysisGroup {
            get;
            set;
        }

        public bool IsAnalyzed {
            get;
            set;
        }

        public int AnalysisVersion {
            get;
            set;
        }

        public string FilePath {
            get;
            set;
        }

        public string GetLine(int lineNo) {
            throw new NotImplementedException();
        }

        public Dictionary<object, object> Properties {
            get;
            set;
        }

        public void RemovedFromProject() {
            throw new NotImplementedException();
        }

        public IModuleContext AnalysisContext {
            get;
            set;
        }

        public void Analyze(CancellationToken cancel) {
            throw new NotImplementedException();
        }
    }
}
