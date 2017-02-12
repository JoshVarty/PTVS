using System;
using Microsoft.PythonTools.Debugger.DebugEngine;

namespace Microsoft.PythonTools.Debugger {
    enum FrameKind {
        None,
        Python,
        Django
    }

    internal static class FrameKindExtensions {
        internal static void GetLanguageInfo(this FrameKind self, ref string pbstrLanguage, ref Guid pguidLanguage) {
            switch (self) {
                case FrameKind.Django:
                    pbstrLanguage = DebuggerLanguageNames.DjangoTemplates;
                    pguidLanguage = Guid.Empty;
                    break;
                case FrameKind.Python:
                    pbstrLanguage = DebuggerLanguageNames.Python;
                    pguidLanguage = DebuggerConstants.guidLanguagePython;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}
