using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.BuildTasks {
    /// <summary>
    /// Resolves a project's active environment from the contents of the project
    /// file.
    /// </summary>
    public sealed class ResolveEnvironment : ITask {
        private readonly string _projectPath;
        internal readonly TaskLoggingHelper _log;

        internal ResolveEnvironment(string projectPath, IBuildEngine buildEngine) {
            BuildEngine = buildEngine;
            _projectPath = projectPath;
            _log = new TaskLoggingHelper(this);
        }

        class CatalogLog : ICatalogLog {
            private readonly TaskLoggingHelper _helper;
            public CatalogLog(TaskLoggingHelper helper) {
                _helper = helper;
            }

            public void Log(string msg) {
                _helper.LogWarning(msg);
            }
        }

        /// <summary>
        /// The interpreter ID to resolve.
        /// </summary>
        public string InterpreterId { get; set; }

        [Output]
        public string PrefixPath { get; private set; }

        [Output]
        public string ProjectRelativePrefixPath { get; private set; }

        [Output]
        public string InterpreterPath { get; private set; }

        [Output]
        public string WindowsInterpreterPath { get; private set; }

        [Output]
        public string Architecture { get; private set; }

        [Output]
        public string PathEnvironmentVariable { get; private set; }

        [Output]
        public string Description { get; private set; }

        [Output]
        public string MajorVersion { get; private set; }

        [Output]
        public string MinorVersion { get; private set; }

        internal string[] SearchPaths { get; private set; }

        public bool Execute() {
            string id = InterpreterId;

            ProjectCollection collection = null;
            Project project = null;

            var exports = GetExportProvider();
            if (exports == null) {
                _log.LogError("Unable to obtain interpreter service.");
                return false;
            }

            try {
                try {
                    project = ProjectCollection.GlobalProjectCollection.GetLoadedProjects(_projectPath).Single();
                } catch (InvalidOperationException) {
                    // Could not get exactly one project matching the path.
                }

                if (project == null) {
                    collection = new ProjectCollection();
                    project = collection.LoadProject(_projectPath);
                }

                if (id == null) {
                    id = project.GetPropertyValue("InterpreterId");
                    if (String.IsNullOrWhiteSpace(id)) {
                        var options = exports.GetExportedValueOrDefault<IInterpreterOptionsService>();
                        if (options != null) {
                            id = options.DefaultInterpreterId;
                        }
                    }
                }

                var projectHome = PathUtils.GetAbsoluteDirectoryPath(
                    project.DirectoryPath,
                    project.GetPropertyValue("ProjectHome")
                );

                var searchPath = project.GetPropertyValue("SearchPath");
                if (!string.IsNullOrEmpty(searchPath)) {
                    SearchPaths = searchPath.Split(';')
                        .Select(p => PathUtils.GetAbsoluteFilePath(projectHome, p))
                        .ToArray();
                } else {
                    SearchPaths = new string[0];
                }

                // MsBuildProjectContextProvider isn't available in-proc, instead we rely upon the
                // already loaded VsProjectContextProvider which is loaded in proc and already
                // aware of the projects loaded in Solution Explorer.
                var projectContext = exports.GetExportedValueOrDefault<MsBuildProjectContextProvider>();
                if (projectContext != null) {
                    projectContext.AddContext(project);
                }
                try {
                    var config = exports.GetExportedValue<IInterpreterRegistryService>().FindConfiguration(id);

                    if (config == null) {
                        _log.LogError(
                            "The environment '{0}' is not available. Check your project configuration and try again.",
                            id
                        );
                        return false;
                    } else {
                        PrefixPath = PathUtils.EnsureEndSeparator(config.PrefixPath);
                        if (PathUtils.IsSubpathOf(projectHome, PrefixPath)) {
                            ProjectRelativePrefixPath = PathUtils.GetRelativeDirectoryPath(projectHome, PrefixPath);
                        } else {
                            ProjectRelativePrefixPath = string.Empty;
                        }
                        InterpreterPath = config.InterpreterPath;
                        WindowsInterpreterPath = config.WindowsInterpreterPath;
                        Architecture = config.Architecture.ToString("X");
                        PathEnvironmentVariable = config.PathEnvironmentVariable;
                        Description = config.Description;
                        MajorVersion = config.Version.Major.ToString();
                        MinorVersion = config.Version.Minor.ToString();

                        return true;
                    }
                } finally {
                    if (projectContext != null) {
                        projectContext.RemoveContext(project);
                    }
                }

            } catch (Exception ex) {
                _log.LogErrorFromException(ex);
            } finally {
                if (collection != null) {
                    collection.UnloadAllProjects();
                    collection.Dispose();
                }
            }

            _log.LogError("Unable to resolve environment");
            return false;
        }

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }


        private ExportProvider GetExportProvider() {
            return InterpreterCatalog.CreateContainer(
                new CatalogLog(_log), 
                typeof(MsBuildProjectContextProvider), 
                typeof(IInterpreterRegistryService), 
                typeof(IInterpreterOptionsService)
            );
        }
    }

    /// <summary>
    /// Constructs ResolveEnvironment task objects.
    /// </summary>
    public sealed class ResolveEnvironmentFactory : TaskFactory<ResolveEnvironment> {
        public override ITask CreateTask(IBuildEngine taskFactoryLoggingHost) {
            return new ResolveEnvironment(Properties["ProjectPath"], taskFactoryLoggingHost);
        }
    }
}
