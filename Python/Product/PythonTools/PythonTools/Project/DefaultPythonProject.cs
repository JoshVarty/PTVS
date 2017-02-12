using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Execution;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    class DefaultPythonProject : IPythonProject {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _filePath;

        public event EventHandler<AnalyzerChangingEventArgs> ProjectAnalyzerChanging { add { } remove { } }

        public DefaultPythonProject(IServiceProvider serviceProvider, string filePath) {
            Utilities.ArgumentNotNullOrEmpty("filePath", filePath);
            _filePath = filePath;
            _serviceProvider = serviceProvider;
        }

        public void SetProperty(string name, string value) {
            Debug.Fail("Unexpected DefaultPythonProject.SetProperty() call");
        }

        public VsProjectAnalyzer GetProjectAnalyzer() {
            return _serviceProvider.GetPythonToolsService().DefaultAnalyzer;
        }

        public IPythonInterpreterFactory GetInterpreterFactory() {
            return _serviceProvider.GetComponentModel().GetService<IInterpreterOptionsService>().DefaultInterpreter;
        }

        public bool Publish(PublishProjectOptions options) {
            Debug.Fail("Unexpected DefaultPythonProject.Publish() call");
            return false;
        }

        private string FullPath => Path.GetFullPath(_filePath);
        public string GetProperty(string name) => null;
        public string GetWorkingDirectory() => PathUtils.GetParent(FullPath);
        public string GetStartupFile() => FullPath;
        public string ProjectDirectory => PathUtils.GetParent(_filePath);
        public string ProjectName => Path.GetFileNameWithoutExtension(_filePath);
        public string ProjectHome => ProjectDirectory;
        public string ProjectFile => FullPath;
        public IServiceProvider Site => _serviceProvider;
        public string GetUnevaluatedProperty(string name) => null;
        public IAsyncCommand FindCommand(string canonicalName) => null;
        public ProjectInstance GetMSBuildProjectInstance() => null;
        public void AddActionOnClose(object key, Action<object> action) { }
        public IPythonInterpreterFactory GetInterpreterFactoryOrThrow() => GetInterpreterFactory();
        public LaunchConfiguration GetLaunchConfigurationOrThrow() => new LaunchConfiguration(GetInterpreterFactory().Configuration);

        public event EventHandler ProjectAnalyzerChanged { add { } remove { } }
    }
}
