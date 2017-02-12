using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools {
    static class PythonInterpreterFactoryRunnableExtensions {
        /// <summary>
        /// Returns true if the factory can be run. This checks whether the
        /// configured InterpreterPath value is an actual file.
        /// </summary>
        internal static bool IsRunnable(this IPythonInterpreterFactory factory) {
            return factory != null && factory.Configuration.IsRunnable();
        }

        /// <summary>
        /// Returns true if the configuration can be run. This checks whether
        /// the configured InterpreterPath value is an actual file.
        /// </summary>
        internal static bool IsRunnable(this InterpreterConfiguration config) {
            return config != null &&
                !InterpreterRegistryConstants.IsNoInterpretersFactory(config.Id) &&
                File.Exists(config.InterpreterPath);
        }

        /// <summary>
        /// Checks whether the factory can be run and throws the appropriate
        /// exception if it cannot.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// factory is null and parameterName is provided.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// factory is null and parameterName is not provided, or the factory
        /// has no configuration.
        /// </exception>
        /// <exception cref="NoInterpretersException">
        /// factory is the sentinel used when no environments are installed.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// factory's InterpreterPath does not exist on disk.
        /// </exception>
        internal static void ThrowIfNotRunnable(this IPythonInterpreterFactory factory, string parameterName = null) {
            if (factory == null) {
                if (string.IsNullOrEmpty(parameterName)) {
                    throw new NullReferenceException();
                } else {
                    throw new ArgumentNullException(parameterName);
                }
            }
            factory.Configuration.ThrowIfNotRunnable();
        }

        /// <summary>
        /// Checks whether the configuration can be run and throws the
        /// appropriate exception if it cannot.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// config is null and parameterName is provided.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// config is null and parameterName is not provided.
        /// </exception>
        /// <exception cref="NoInterpretersException">
        /// config is the sentinel used when no environments are installed.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// config's InterpreterPath does not exist on disk.
        /// </exception>
        internal static void ThrowIfNotRunnable(this InterpreterConfiguration config, string parameterName = null) {
            if (config == null) {
                if (string.IsNullOrEmpty(parameterName)) {
                    throw new NullReferenceException();
                } else {
                    throw new ArgumentNullException(parameterName);
                }
            } else if (InterpreterRegistryConstants.IsNoInterpretersFactory(config.Id)) {
                throw new NoInterpretersException();
            } else if (!File.Exists(config.InterpreterPath)) {
                throw new FileNotFoundException(config.InterpreterPath ?? "(null)");
            }
        }
    }
}
