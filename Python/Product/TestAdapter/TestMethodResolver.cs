using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Projects;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.TestAdapter {
    [Export(typeof(ITestMethodResolver))]
    class TestMethodResolver : ITestMethodResolver {
        private readonly IServiceProvider _serviceProvider;
        private readonly TestContainerDiscoverer _discoverer;

        #region ITestMethodResolver Members

        [ImportingConstructor]
        public TestMethodResolver([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider,
            [Import]TestContainerDiscoverer discoverer) {
            _serviceProvider = serviceProvider;
            _discoverer = discoverer;
        }

        public Uri ExecutorUri {
            get { return TestContainerDiscoverer._ExecutorUri; }
        }

        public string GetCurrentTest(string filePath, int line, int lineCharOffset) {
            var provider = PathToProject(filePath) as IPythonProjectProvider;
            if (provider != null) {
                var container = _discoverer.GetTestContainer(provider.Project, filePath);
                if (container != null) {
                    foreach (var testCase in container.TestCases) {
                        if (testCase.StartLine >= line && line <= testCase.EndLine) {
                            var moduleName = CommonUtils.CreateFriendlyFilePath(provider.Project.ProjectHome, testCase.Filename);
                            return moduleName + "::" + testCase.ClassName + "::" + testCase.MethodName;
                        }
                    }
                }
            }

            return null;
        }

        private IVsProject PathToProject(string filePath) {
            var rdt = (IVsRunningDocumentTable)_serviceProvider.GetService(typeof(SVsRunningDocumentTable));
            IVsHierarchy hierarchy;
            uint itemId;
            IntPtr docData = IntPtr.Zero;
            uint cookie;
            try {
                var hr = rdt.FindAndLockDocument(
                    (uint)_VSRDTFLAGS.RDT_NoLock,
                    filePath,
                    out hierarchy,
                    out itemId,
                    out docData,
                    out cookie);
                ErrorHandler.ThrowOnFailure(hr);
            } finally {
                if (docData != IntPtr.Zero) {
                    Marshal.Release(docData);
                    docData = IntPtr.Zero;
                }
            }

            return hierarchy as IVsProject;
        }

        #endregion
    }
}
