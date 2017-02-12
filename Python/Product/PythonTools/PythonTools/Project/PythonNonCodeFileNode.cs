using System;
using System.IO;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    class PythonNonCodeFileNode : CommonNonCodeFileNode {
        private object _designerContext;

        public PythonNonCodeFileNode(CommonProjectNode root, ProjectElement e)
            : base(root, e) {
        }

        class XamlCallback : IXamlDesignerCallback {
            private readonly PythonFileNode _node;

            public XamlCallback(PythonFileNode node) {
                _node = node;
            }

            public ITextBuffer Buffer {
                get {
                    return _node.GetTextBuffer();
                }
            }

            public ITextView TextView {
                get {
                    return _node.GetTextView();
                }
            }

            public string[] FindMethods(string className, int? paramCount) {
                var fileInfo = _node.GetAnalysisEntry();
                return fileInfo.Analyzer.FindMethodsAsync(
                    fileInfo,
                    _node.GetTextBuffer(),
                    className,
                    paramCount
                ).WaitOrDefault(1000);
            }

            public InsertionPoint GetInsertionPoint(string className) {
                var fileInfo = _node.GetAnalysisEntry();
                return fileInfo.Analyzer.GetInsertionPointAsync(
                    fileInfo,
                    _node.GetTextBuffer(),
                    className
                ).WaitOrDefault(1000);
            }

            public MethodInformation GetMethodInfo(string className, string methodName) {
                var fileInfo = _node.GetAnalysisEntry();
                var info = fileInfo.Analyzer.GetMethodInfoAsync(fileInfo, _node.GetTextBuffer(), className, methodName).WaitOrDefault(1000);
                if (info != null) {
                    return new MethodInformation(
                        info.start,
                        info.end,
                        info.found
                    );
                }
                return null;
            }
        }

        public override int QueryService(ref Guid guidService, out object result) {
            var model = ProjectMgr.GetService(typeof(SComponentModel)) as IComponentModel;
            var designerSupport = model?.GetService<IXamlDesignerSupport>();
            if (designerSupport != null &&
                guidService == designerSupport.DesignerContextTypeGuid &&
                Path.GetExtension(Url).Equals(".xaml", StringComparison.OrdinalIgnoreCase)) {
                // Create a DesignerContext for the XAML designer for this file
                if (_designerContext == null) {
                    _designerContext = designerSupport.CreateDesignerContext();
                    var child = (
                        // look for spam.py
                        ProjectMgr.FindNodeByFullPath(Path.ChangeExtension(Url, PythonConstants.FileExtension)) ??
                        // then look for spam.pyw
                        ProjectMgr.FindNodeByFullPath(Path.ChangeExtension(Url, PythonConstants.WindowsFileExtension))
                    ) as CommonFileNode;

                    if (child != null) {
                        PythonFileNode pythonNode = child as PythonFileNode;
                        if (pythonNode != null) {
                            designerSupport.InitializeEventBindingProvider(
                                _designerContext,
                                new XamlCallback(pythonNode)
                            );
                        }
                    }
                }
                result = _designerContext;
                return VSConstants.S_OK;
            }

            return base.QueryService(ref guidService, out result);
        }
    }
}
