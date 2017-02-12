using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Interpreter.Default;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.PyAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudioTools;
using TestUtilities;
using TestUtilities.Python;

namespace AnalysisTests {
    /// <summary>
    /// Base class w/ common infrastructure for analysis unit tests.
    /// </summary>
    public class BaseAnalysisTest {
        private readonly IPythonInterpreterFactory _defaultFactoryV2 = InterpreterFactoryCreator.CreateAnalysisInterpreterFactory(new Version(2, 7));
        private readonly IPythonInterpreterFactory _defaultFactoryV3 = InterpreterFactoryCreator.CreateAnalysisInterpreterFactory(new Version(3, 5));

        private List<IDisposable> _toDispose;

        static BaseAnalysisTest() {
            AnalysisLog.Reset();
            AnalysisLog.ResetTime();
            AssertListener.Initialize();
            PythonTestData.Deploy(includeTestData: false);
        }

        [TestInitialize]
        public void StartAnalysisLog() {
            AnalysisLog.Reset();
            AnalysisLog.Output = Console.Out;
        }

        [TestCleanup]
        public void EndAnalysisLog() {
            AnalysisLog.Flush();
            AnalysisLog.Output = null;
            foreach (var d in _toDispose.MaybeEnumerate()) {
                d.Dispose();
            }
            _toDispose = null;
        }

        protected virtual IPythonInterpreterFactory DefaultFactoryV2 => _defaultFactoryV2;
        protected virtual IPythonInterpreterFactory DefaultFactoryV3 => _defaultFactoryV3;
        protected virtual bool SupportsPython3 => _defaultFactoryV3 != null;
        protected virtual IModuleContext DefaultContext => null;
        protected virtual AnalysisLimits GetLimits() => AnalysisLimits.GetDefaultLimits();

        protected virtual PythonAnalysis CreateAnalyzerInternal(IPythonInterpreterFactory factory) {
            return new PythonAnalysis(factory);
        }

        public PythonAnalysis CreateAnalyzer(IPythonInterpreterFactory factory = null, bool allowParseErrors = false) {
            var analysis = CreateAnalyzerInternal(factory ?? DefaultFactoryV2);
            analysis.AssertOnParseErrors = !allowParseErrors;
            analysis.ModuleContext = DefaultContext;
            analysis.SetLimits(GetLimits());

            if (_toDispose == null) {
                _toDispose = new List<IDisposable>();
            }
            _toDispose.Add(analysis);

            return analysis;
        }

        public PythonAnalysis ProcessTextV2(string text, bool allowParseErrors = false) {
            var analysis = CreateAnalyzer(DefaultFactoryV2, allowParseErrors);
            analysis.AddModule("test-module", text);
            analysis.WaitForAnalysis();
            return analysis;
        }

        public PythonAnalysis ProcessTextV3(string text, bool allowParseErrors = false) {
            var analysis = CreateAnalyzer(DefaultFactoryV3, allowParseErrors);
            analysis.AddModule("test-module", text);
            analysis.WaitForAnalysis();
            return analysis;
        }

        public PythonAnalysis ProcessText(
            string text,
            PythonLanguageVersion version = PythonLanguageVersion.None,
            bool allowParseErrors = false
        ) {
            // TODO: Analyze against multiple versions when the version is None
            if (version == PythonLanguageVersion.None) {
                return ProcessTextV2(text, allowParseErrors);
            }

            var analysis = CreateAnalyzer(InterpreterFactoryCreator.CreateAnalysisInterpreterFactory(version.ToVersion()), allowParseErrors);
            analysis.AddModule("test-module", text);
            analysis.WaitForAnalysis();
            return analysis;
        }
    }
}
