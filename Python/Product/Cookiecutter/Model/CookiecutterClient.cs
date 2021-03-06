using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json.Linq;

namespace Microsoft.CookiecutterTools.Model {
    class CookiecutterClient : ICookiecutterClient {
        private readonly IServiceProvider _provider;
        private readonly CookiecutterPythonInterpreter _interpreter;
        private readonly string _envFolderPath;
        private readonly string _envInterpreterPath;
        private readonly Redirector _redirector;

        internal string DefaultBasePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public bool CookiecutterInstalled {
            get {
                if (!File.Exists(_envInterpreterPath)) {
                    return false;
                }

                return true;
            }
        }

        public CookiecutterClient(IServiceProvider provider, CookiecutterPythonInterpreter interpreter, Redirector redirector) {
            _provider = provider;
            _interpreter = interpreter;
            var localAppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _envFolderPath = Path.Combine(localAppDataFolderPath, "Microsoft", "CookiecutterTools", "env");
            _envInterpreterPath = Path.Combine(_envFolderPath, "scripts", "python.exe");
            _redirector = redirector;
        }

        public async Task<bool> IsCookiecutterInstalled() {
            if (!File.Exists(_envInterpreterPath)) {
                return false;
            }

            try {
                var result = await RunCheckScript(_envInterpreterPath);
                return result.StandardOutputLines.FirstOrDefault() == "ok";
            } catch (Exception e) {
                Trace.WriteLine(e.Message);
                return false;
            }
        }

        public async Task CreateCookiecutterEnv() {
            // Create a virtual environment using the global interpreter
            var interpreterPath = _interpreter.InterpreterExecutablePath;
            var output = ProcessOutput.RunHiddenAndCapture(interpreterPath, "-m", "venv", _envFolderPath);

            await WaitForOutput(interpreterPath, output);
        }

        public async Task InstallPackage() {
            // Install the package into the virtual environment
            var output = ProcessOutput.RunHiddenAndCapture(_envInterpreterPath, "-m", "pip", "install", "cookiecutter<1.5");

            await WaitForOutput(_envInterpreterPath, output);
        }

        public async Task<TemplateContext> LoadUnrenderedContextAsync(string localTemplateFolder, string userConfigFilePath) {
            if (localTemplateFolder == null) {
                throw new ArgumentNullException(nameof(localTemplateFolder));
            }

            var unrenderedContext = new TemplateContext();

            var result = await RunGenerateContextScript(_redirector, _envInterpreterPath, localTemplateFolder, userConfigFilePath);
            var contextJson = string.Join(Environment.NewLine, result.StandardOutputLines);
            var context = (JToken)JObject.Parse(contextJson).SelectToken("cookiecutter");
            if (context != null) {
                JProperty vsExtrasProp = null;

                foreach (JProperty prop in context) {
                    // Properties that start with underscore are for internal use,
                    // and cookiecutter doesn't prompt for them.
                    if (!prop.Name.StartsWith("_")) {
                        if (prop.Value.Type == JTokenType.String ||
                            prop.Value.Type == JTokenType.Integer ||
                            prop.Value.Type == JTokenType.Float) {
                            unrenderedContext.Items.Add(new ContextItem(prop.Name, Selectors.String, prop.Value.ToString()));
                        } else if (prop.Value.Type == JTokenType.Array) {
                            var elements = new List<string>();
                            JArray ar = prop.Value as JArray;
                            foreach (JToken element in ar) {
                                elements.Add(element.ToString());
                            }
                            unrenderedContext.Items.Add(new ContextItem(prop.Name, Selectors.List, elements[0], elements.ToArray()));
                        } else {
                            throw new InvalidOperationException(Strings.CookiecutterClient_UnsupportedJsonElementTypeForPorperty.FormatUI(prop.Name));
                        }
                    } else if (prop.Name == "_visual_studio") {
                        vsExtrasProp = prop;
                    } else if (prop.Name == "_visual_studio_post_cmds") {
                        ReadCommands(unrenderedContext, prop);
                    }
                }

                if (vsExtrasProp != null) {
                    LoadVisualStudioSpecificContext(unrenderedContext.Items, vsExtrasProp);
                }
            }

            return unrenderedContext;
        }

        public async Task<TemplateContext> LoadRenderedContextAsync(string localTemplateFolder, string userConfigFilePath, string contextPath, string outputFolderPath) {
            if (localTemplateFolder == null) {
                throw new ArgumentNullException(nameof(localTemplateFolder));
            }

            if (contextPath == null) {
                throw new ArgumentNullException(nameof(contextPath));
            }

            if (outputFolderPath == null) {
                throw new ArgumentNullException(nameof(outputFolderPath));
            }

            var renderedContext = new TemplateContext();

            var result = await RunRenderContextScript(_redirector, _envInterpreterPath, localTemplateFolder, userConfigFilePath, outputFolderPath, contextPath);
            var contextJson = string.Join(Environment.NewLine, result.StandardOutputLines);
            var context = (JToken)JObject.Parse(contextJson).SelectToken("cookiecutter");
            if (context != null) {
                foreach (JProperty prop in context) {
                    // Properties that start with underscore are for internal use,
                    // and cookiecutter doesn't prompt for them.
                    if (!prop.Name.StartsWith("_")) {
                        if (prop.Value.Type == JTokenType.String ||
                            prop.Value.Type == JTokenType.Integer ||
                            prop.Value.Type == JTokenType.Float) {
                            renderedContext.Items.Add(new ContextItem(prop.Name, Selectors.String, prop.Value.ToString()));
                        } else if (prop.Value.Type == JTokenType.Array) {
                            var elements = new List<string>();
                            JArray ar = prop.Value as JArray;
                            foreach (JToken element in ar) {
                                elements.Add(element.ToString());
                            }
                            renderedContext.Items.Add(new ContextItem(prop.Name, Selectors.List, elements[0], elements.ToArray()));
                        } else {
                            throw new InvalidOperationException(Strings.CookiecutterClient_UnsupportedJsonElementTypeForPorperty.FormatUI(prop.Name));
                        }
                    } else if (prop.Name == "_visual_studio_post_cmds") {
                        // List of commands to run after the folder is opened,
                        // or the files are added to the project.
                        // name and args are the values passed to DTE.ExecuteCommand
                        // args type:
                        //   - it should be of type array if the command is passed multiple arguments
                        //   - it can be of type string for the common single argument scenario,
                        //     this is equivalent to an array of a single element
                        // args value passed to DTE.ExecuteCommand will
                        // be concatenation of all values in the array, each one quoted as necessary
                        // (for example when a value contains a space char)
                        //
                        // cookiecutter._output_folder_path can be used to files inside generated project
                        //
                        // Examples:
                        // "_visual_studio_post_cmds": [
                        // {
                        //   "name": "Cookiecutter.ExternalWebBrowser",
                        //   "args": "https://docs.microsoft.com"
                        // },
                        // {
                        //   "name": "View.WebBrowser",
                        //   "args": "https://docs.microsoft.com"
                        // },
                        // {
                        //   "name": "View.WebBrowser",
                        //   "args": "{{cookiecutter._output_folder_path}}\\readme.html"
                        // },
                        // {
                        //   "name": "File.OpenFile",
                        //   "args": [ "{{cookiecutter._output_folder_path}}\\readme.txt" ]
                        // }
                        // ]
                        //
                        // Special accommodations for switch arguments:
                        // If the switch takes a value, the switch/value pair should be split in 2 array elements
                        // so the value can be properly quoted independently of the switch, whose name isn't quoted
                        // since space is not allowed in the name. The switch name should end with a colon.
                        //
                        // {
                        //   "name": "File.OpenFile",
                        //   "args": ["c:\\my folder\\my file.txt", "/e:", "Source Code (text) Editor"]
                        // }
                        //

                        ReadCommands(renderedContext, prop);
                    }
                }
            }

            return renderedContext;
        }

        private void ReadCommands(TemplateContext renderedContext, JProperty prop) {
            if (prop.Value.Type != JTokenType.Array) {
                WrongJsonType("_visual_studio_post_cmds", JTokenType.Array, prop.Type);
                return;
            }

            foreach (JToken element in (JArray)prop.Value) {
                if (element.Type != JTokenType.Object) {
                    WrongJsonType("_visual_studio_post_cmds element", JTokenType.Object, element.Type);
                    continue;
                }

                var cmd = ReadCommand((JObject)element);
                if (cmd != null) {
                    renderedContext.Commands.Add(cmd);
                }
            }
        }

        private DteCommand ReadCommand(JObject itemObj) {
            string name = null;
            string args = null;

            var nameToken = itemObj.SelectToken("name");
            if (nameToken != null) {
                if (nameToken.Type == JTokenType.String) {
                    name = nameToken.Value<string>();
                } else {
                    WrongJsonType("name", JTokenType.String, nameToken.Type);
                    return null;
                }
            } else {
                MissingProperty("_visual_studio_post_cmds", "name");
                return null;
            }

            var argsToken = itemObj.SelectToken("args");
            if (argsToken != null) {
                if (argsToken.Type == JTokenType.String) {
                    var argValues = new[] { argsToken.Value<string>() };
                    args = BuildArguments(argValues);
                } else if (argsToken.Type == JTokenType.Array) {
                    var argValues = ((JArray)argsToken).Values().Where(t => t.Type == JTokenType.String).Select(t => t.Value<string>());
                    args = BuildArguments(argValues);
                } else {
                    WrongJsonType("args", JTokenType.Array, argsToken.Type);
                    return null;
                }
            }

            return new DteCommand(name, args);
        }

        public async Task<CreateFilesOperationResult> CreateFilesAsync(string localTemplateFolder, string userConfigFilePath, string contextFilePath, string outputFolderPath) {
            if (localTemplateFolder == null) {
                throw new ArgumentNullException(nameof(localTemplateFolder));
            }

            if (contextFilePath == null) {
                throw new ArgumentNullException(nameof(contextFilePath));
            }

            if (outputFolderPath == null) {
                throw new ArgumentNullException(nameof(outputFolderPath));
            }

            string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            var result = await RunRunScript(_redirector, _envInterpreterPath, localTemplateFolder, userConfigFilePath, tempFolder, contextFilePath);
            return await MoveToDesiredFolderAsync(outputFolderPath, tempFolder);
        }

        public Task<string> GetDefaultOutputFolderAsync(string shortName) {
            var shell = _provider?.GetService(typeof(SVsShell)) as IVsShell;
            object o;
            string vspp, baseName;
            if (shell != null &&
                ErrorHandler.Succeeded(shell.GetProperty((int)__VSSPROPID.VSSPROPID_VisualStudioProjDir, out o)) &&
                PathUtils.IsValidPath((vspp = o as string))) {
                baseName = vspp;
            } else {
                baseName = DefaultBasePath;
            }

            var candidate = PathUtils.GetAbsoluteDirectoryPath(baseName, shortName);
            int counter = 1;
            while (Directory.Exists(candidate) || File.Exists(PathUtils.TrimEndSeparator(candidate))) {
                candidate = PathUtils.GetAbsoluteDirectoryPath(baseName, "{0}{1}".FormatInvariant(shortName, ++counter));
            }
            return Task.FromResult(candidate);
        }

        private static string BuildArguments(IEnumerable<string> values) {
            // Examples of valid results:
            // "C:\My Folder\"
            // C:\MyFolder\MyFile.txt /e:"Source Code (text) Editor"
            //
            // Examples of invalid results:
            // C:\My Folder
            // C:\MyFolder\MyFile.txt "/e:Source Code (text) Editor"
            // C:\MyFolder\MyFile.txt /e: "Source Code (text) Editor"
            // C:\MyFolder\MyFile.txt /e:Source Code (text) Editor
            StringBuilder args = new StringBuilder();
            bool insertSpace = false;
            foreach (var val in values) {
                if (insertSpace) {
                    args.Append(" ");
                }

                if (val.EndsWith(":")) {
                    args.Append(val);
                    // no space after a switch that takes a value
                    insertSpace = false;
                } else {
                    args.Append(ProcessOutput.QuoteSingleArgument(val));
                    insertSpace = true;
                }
            }

            return args.ToString();
        }

        private void LoadVisualStudioSpecificContext(List<ContextItem> items, JProperty vsExtrasProp) {
            // This section reads additional metadata for Visual Studio.
            // All fields are optional, but if they are specified, they are validated.
            //
            // Example:
            // "_visual_studio" : {
            //   "var1" : {
            //     "label" : "Variable 1",
            //     "description" : "Description for variable 1",
            //     "selector" : "yesno"
            //   },
            //   "var2" : {
            //     "label" : "Variable 2",
            //     "description" : "Description for variable 2",
            //     "url" : "http://azure.microsoft.com",
            //     "selector" : "odbcConnection"
            //   }
            // }
            //
            // Valid values for selector:
            // - string
            // - list
            // - yesno: generates 'y' or 'n'
            // - odbcConnection
            if (vsExtrasProp.Value.Type == JTokenType.Object) {
                var vsExtrasObj = (JObject)vsExtrasProp.Value;
                foreach (JProperty prop in vsExtrasObj.Properties()) {
                    var item = items.SingleOrDefault(ctx => ctx.Name == prop.Name);
                    if (item != null) {
                        if (prop.Value.Type == JTokenType.Object) {
                            var itemObj = (JObject)prop.Value;
                            ReadLabel(item, itemObj);
                            ReadDescription(item, itemObj);
                            ReadUrl(item, itemObj);
                            ReadSelector(item, itemObj);
                        } else {
                            WrongJsonType(prop.Name, JTokenType.Object, prop.Value.Type);
                        }
                    } else {
                        ReferenceNotFound(prop.Name);
                    }
                }
            } else {
                WrongJsonType("_visual_studio", JTokenType.Object, vsExtrasProp.Value.Type);
            }
        }

        private JToken ReadLabel(ContextItem item, JObject itemObj) {
            var labelToken = itemObj.SelectToken("label");
            if (labelToken != null) {
                if (labelToken.Type == JTokenType.String) {
                    item.Label = labelToken.Value<string>();
                } else {
                    WrongJsonType("label", JTokenType.String, labelToken.Type);
                }
            }

            return labelToken;
        }

        private JToken ReadDescription(ContextItem item, JObject itemObj) {
            var descriptionToken = itemObj.SelectToken("description");
            if (descriptionToken != null) {
                if (descriptionToken.Type == JTokenType.String) {
                    item.Description = descriptionToken.Value<string>();
                } else {
                    WrongJsonType("description", JTokenType.String, descriptionToken.Type);
                }
            }

            return descriptionToken;
        }

        private JToken ReadUrl(ContextItem item, JObject itemObj) {
            var urlToken = itemObj.SelectToken("url");
            if (urlToken != null) {
                if (urlToken.Type == JTokenType.String) {
                    var val = urlToken.Value<string>();
                    Uri uri;
                    if (Uri.TryCreate(val, UriKind.Absolute, out uri)) {
                        if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) {
                            item.Url = val;
                        } else {
                            InvalidUrl(val);
                        }
                    } else {
                        InvalidUrl(val);
                    }
                } else {
                    WrongJsonType("url", JTokenType.String, urlToken.Type);
                }
            }

            return urlToken;
        }

        private void ReadSelector(ContextItem item, JObject itemObj) {
            var selectorToken = itemObj.SelectToken("selector");
            if (selectorToken != null) {
                if (selectorToken.Type == JTokenType.String) {
                    item.Selector = selectorToken.Value<string>();
                } else {
                    WrongJsonType("selector", JTokenType.String, selectorToken.Type);
                }
            }
        }

        private void InvalidUrl(string url) {
            _redirector.WriteErrorLine(Strings.CookiecutterClient_Invalidurl.FormatUI(url));
        }

        private void WrongJsonType(string name, JTokenType expected, JTokenType actual) {
            _redirector.WriteErrorLine(Strings.CookiecutterClient_WrongJsonType.FormatUI(name, expected, actual));
        }

        private void ReferenceNotFound(string name) {
            _redirector.WriteErrorLine(Strings.CookiecutterClient_ReferenceNotFound.FormatUI(name));
        }

        private void MissingProperty(string objectName, string propertyName) {
            _redirector.WriteErrorLine(Strings.CookiecutterClient_MissingProperty.FormatUI(propertyName, objectName));
        }

        private static async Task<ProcessOutputResult> RunGenerateContextScript(Redirector redirector, string interpreterPath, string templateFolderPath, string userConfigFilePath) {
            var scriptPath = PythonToolsInstallPath.GetFile("cookiecutter_load.py");
            return await RunPythonScript(redirector, interpreterPath, scriptPath, "\"{0}\" \"{1}\"".FormatInvariant(templateFolderPath, userConfigFilePath));
        }

        private static async Task<ProcessOutputResult> RunRenderContextScript(Redirector redirector, string interpreterPath, string templateFolderPath, string userConfigFilePath, string outputFolderPath, string contextPath) {
            var scriptPath = PythonToolsInstallPath.GetFile("cookiecutter_render.py");
            return await RunPythonScript(redirector, interpreterPath, scriptPath, "\"{0}\" \"{1}\" \"{2}\" \"{3}\"".FormatInvariant(templateFolderPath, userConfigFilePath, PathUtils.TrimEndSeparator(outputFolderPath), contextPath));
        }

        private static async Task<ProcessOutputResult> RunCheckScript(string interpreterPath) {
            var scriptPath = PythonToolsInstallPath.GetFile("cookiecutter_check.py");
            var output = ProcessOutput.RunHiddenAndCapture(interpreterPath, scriptPath);
            return await WaitForOutput(interpreterPath, output);
        }

        private static async Task<ProcessOutputResult> RunRunScript(Redirector redirector, string interpreterPath, string templateFolderPath, string userConfigFilePath, string outputFolderPath, string contextPath) {
            var scriptPath = PythonToolsInstallPath.GetFile("cookiecutter_run.py");
            return await RunPythonScript(redirector, interpreterPath, scriptPath, GetRunArguments(templateFolderPath, userConfigFilePath, outputFolderPath, contextPath));
        }

        private static string GetRunArguments(string templateFolderPath, string userConfigFilePath, string outputFolderPath, string contextFilePath) {
            return "\"{0}\" \"{1}\" \"{2}\" \"{3}\"".FormatInvariant(contextFilePath, templateFolderPath, outputFolderPath, userConfigFilePath);
        }

        private static async Task<ProcessOutputResult> RunPythonScript(Redirector redirector, string interpreterPath, string script, string parameters) {
            var outputLines = new List<string>();
            var errorLines = new List<string>();

            ProcessOutput output = null;
            var arguments = "\"{0}\" {1}".FormatInvariant(script, parameters);
            var listRedirector = new ListRedirector(outputLines, errorLines);
            var outerRedirector = new TeeRedirector(redirector, listRedirector);

            output = ProcessOutput.Run(interpreterPath, new string[] { arguments }, null, null, false, outerRedirector);

            var result = await WaitForOutput(interpreterPath, output);
            result.StandardOutputLines = outputLines.ToArray();
            result.StandardErrorLines = errorLines.ToArray();

            return result;
        }

        private static async Task<ProcessOutputResult> WaitForOutput(string interpreterPath, ProcessOutput output) {
            using (output) {
                await output;

                var r = new ProcessOutputResult() {
                    ExeFileName = interpreterPath,
                    ExitCode = output.ExitCode,
                    StandardOutputLines = output.StandardOutputLines?.ToArray(),
                    StandardErrorLines = output.StandardErrorLines?.ToArray(),
                };

                // All our python scripts will return 0 if successful
                if (r.ExitCode != 0) {
                    throw new ProcessException(r);
                }

                return r;
            }
        }

        private async Task<CreateFilesOperationResult> MoveToDesiredFolderAsync(string desiredFolder, string tempFolder) {
            if (!Directory.Exists(desiredFolder)) {
                Directory.CreateDirectory(desiredFolder);
            }

            // Cookiecutter templates generate into a single subfolder that doesn't have a fixed name
            string generatedFolder = tempFolder;
            var subfolders = Directory.GetDirectories(tempFolder);
            if (subfolders.Length == 1) {
                generatedFolder = subfolders[0];
            } else {
                throw new InvalidOperationException(Strings.CookiecutterClient_MoveToDesiredFolderTemplatedFolderNotFound);
            }

            var res = await MoveFilesAndFoldersAsync(generatedFolder, desiredFolder);

            try {
                Directory.Delete(tempFolder, true);
            } catch (IOException) {
            }

            return res;
        }

        private async Task<CreateFilesOperationResult> MoveFilesAndFoldersAsync(string generatedFolder, string targetFolderPath) {
            List<string> createdFolders = new List<string>();
            List<string> createdFiles = new List<string>();
            List<ReplacedFile> replacedFiles = new List<ReplacedFile>();

            Directory.CreateDirectory(targetFolderPath);

            foreach (var folderPath in PathUtils.EnumerateDirectories(generatedFolder, recurse: true, fullPaths: false)) {
                createdFolders.Add(folderPath);

                Directory.CreateDirectory(Path.Combine(targetFolderPath, folderPath));
            }

            foreach (var filePath in PathUtils.EnumerateFiles(generatedFolder, recurse: true, fullPaths: false)) {
                createdFiles.Add(filePath);

                string targetFilePath = Path.Combine(targetFolderPath, filePath);
                string generatedFilePath = Path.Combine(generatedFolder, filePath);

                if (File.Exists(targetFilePath)) {
                    if (!await AreFilesSameAsync(generatedFilePath, targetFilePath)) {
                        // Need to backup the user's file before overwriting it
                        string backupFilePath = GetBackupFilePath(targetFilePath);
                        File.Move(targetFilePath, backupFilePath);
                        File.Move(generatedFilePath, targetFilePath);
                        replacedFiles.Add(new ReplacedFile(filePath, PathUtils.GetRelativeFilePath(targetFolderPath, backupFilePath)));
                    }
                } else {
                    File.Move(generatedFilePath, targetFilePath);
                }
            }

            return new Model.CreateFilesOperationResult(createdFolders.ToArray(), createdFiles.ToArray(), replacedFiles.ToArray());
        }

        private string GetBackupFilePath(string filePath) {
            return PathUtils.GetAvailableFilename(
                Path.GetDirectoryName(filePath),
                Path.GetFileNameWithoutExtension(filePath) + ".bak",
                Path.GetExtension(filePath)
            );
        }

        internal static async Task<bool> AreFilesSameAsync(string file1Path, string file2Path) {
            var length = new FileInfo(file1Path).Length;
            if (length != new FileInfo(file2Path).Length) {
                return false;
            }

            int bufferSize = 32768;
            var buffer1 = new byte[bufferSize];
            var buffer2 = new byte[bufferSize];
            using (var stream1 = new FileStream(file1Path, FileMode.Open, FileAccess.Read))
            using (var stream2 = new FileStream(file2Path, FileMode.Open, FileAccess.Read)) {
                while (length > 0) {
                    var actual1 = await stream1.ReadAsync(buffer1, 0, bufferSize);
                    var actual2 = await stream2.ReadAsync(buffer2, 0, bufferSize);
                    if (actual1 != actual2) {
                        return false;
                    }

                    length -= actual1;

                    if (!buffer1.SequenceEqual(buffer2)) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
