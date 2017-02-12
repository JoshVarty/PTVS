using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.BuildTasks {
    /// <summary>
    /// Converts filenames to Python module names.
    /// </summary>
    public class ConvertPathToModuleName : Task {
        /// <summary>
        /// The filenames to convert.
        /// </summary>
        [Required]
        public ITaskItem[] Paths { get; set; }

        /// <summary>
        /// The path representing the top-level module. Even if there are more
        /// __init__.py files above this path, they will not become part of the
        /// module name.
        /// </summary>
        public string PathLimit { get; set; }

        /// <summary>
        /// If true, does not log or fail because of invalid module names.
        /// </summary>
        public bool IgnoreErrors { get; set; }

        /// <summary>
        /// The converted module names.
        /// </summary>
        [Output]
        public ITaskItem[] ModuleNames { get; private set; }

        public override bool Execute() {
            var modules = new List<ITaskItem>();

            foreach (var path in Paths) {
                try {
                    modules.Add(new TaskItem(ModulePath.FromFullPath(path.ItemSpec, PathLimit).ModuleName));
                } catch (ArgumentException ex) {
                    modules.Add(new TaskItem(string.Empty));
                    if (!IgnoreErrors) {
                        Log.LogErrorFromException(ex);
                    }
                }
            }

            ModuleNames = modules.ToArray();
            return !Log.HasLoggedErrors;
        }
    }
}
