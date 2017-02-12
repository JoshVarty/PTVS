using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.PythonTools.CodeCoverage;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for importing code coverage information from a coverage.py XML file.
    /// </summary>
    internal sealed class ImportCoverageCommand : Command {
        private readonly IServiceProvider _serviceProvider;

        public ImportCoverageCommand(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            
        }

        public override void DoCommand(object sender, EventArgs args) {
            var oe = args as OleMenuCmdEventArgs;
            string file = oe.InValue as string;
            PythonLanguageVersion? version = null;
            if (file == null) {
                object[] inp = oe.InValue as object[];
                if (inp != null && inp.Length == 2) {
                    file = inp[0] as string;
                    if (inp[1] is PythonLanguageVersion) {
                        version = (PythonLanguageVersion)inp[1];
                    }
                }
            }

            if (file == null) {
                file = _serviceProvider.BrowseForFileOpen(
                    IntPtr.Zero,
                    Strings.ImportCoverageCommandFileFilter
                );
            }

            if (file != null) {
                var outFilename = Path.ChangeExtension(file, ".coveragexml");

                try {
                    ConvertCoveragePy(file, outFilename, version);
                } catch (IOException ioex) {
                    MessageBox.Show(String.Format(Strings.FailedToConvertCoverageFile, ioex.Message));
                }

                _serviceProvider.GetDTE().ItemOperations.OpenFile(outFilename);
            }
        }

        private void ConvertCoveragePy(string inputFile, string outputFile, PythonLanguageVersion? version) {
            var baseDir = Path.GetDirectoryName(inputFile);
            using (FileStream tmp = new FileStream(inputFile, FileMode.Open))
            using (FileStream outp = new FileStream(outputFile, FileMode.Create)) {
                // Read in the data from coverage.py's XML file
                CoverageFileInfo[] fileInfo = new CoveragePyConverter(baseDir, tmp).Parse();

                // Discover what version we should use for this if one hasn't been provided...
                if (version == null) {
                    foreach (var file in fileInfo) {
                        var project = _serviceProvider.GetProjectFromFile(file.Filename);
                        if (project != null) {
                            version = project.ActiveInterpreter.Configuration.Version.ToLanguageVersion();
                            break;
                        }
                    }
                }

                if (version == null) {
                    var interpreters = _serviceProvider.GetComponentModel().GetService<IInterpreterOptionsService>();
                    version = interpreters?.DefaultInterpreter.Configuration.Version.ToLanguageVersion()
                        ?? PythonLanguageVersion.None;
                }

                // Convert that into offsets within the actual code
                var covInfo = Import(fileInfo, version.Value);

                // Then export as .coveragexml
                new CoverageExporter(outp, covInfo).Export();
            }
        }

        internal static Dictionary<CoverageFileInfo, CoverageMapper>  Import(CoverageFileInfo[] fileInfo, PythonLanguageVersion version = PythonLanguageVersion.V27) {
            Dictionary<CoverageFileInfo, CoverageMapper> files = new Dictionary<CoverageFileInfo, CoverageMapper>();
            foreach (var file in fileInfo) {
                using (var parser = Parser.CreateParser(
                   new FileStream(file.Filename, FileMode.Open),
                   version
                )) {
                    var ast = parser.ParseFile();

                    var collector = new CoverageMapper(ast, file.Filename, file.Hits);
                    ast.Walk(collector);

                    files[file] = collector;
                }
            }
            return files;
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidImportCoverage; }
        }
    }
}
