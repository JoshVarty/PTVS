// Guids.cs
// MUST match guids.h
using System;

namespace Microsoft.PythonTools.Profiling
{
    static class GuidList
    {
        public const string guidPythonToolsPkgString = "6dbd7c1e-1f1b-496d-ac7c-c55dae66c783";
        public static readonly Guid guidPythonToolsPackage = new Guid(guidPythonToolsPkgString);

        public const string guidPythonProfilingPkgString = "81da0100-e6db-4783-91ea-c38c3fa1b81e";
        public const string guidPythonProfilingCmdSetString = "c6e7bafe-d4b6-4e04-85af-9c83f18d8c78";
        public const string guidEditorFactoryString = "3585dc22-81a0-409e-85ae-cae5d02d99cd";

        public static readonly Guid guidPythonProfilingCmdSet = new Guid(guidPythonProfilingCmdSetString);

        public static readonly Guid VsUIHierarchyWindow_guid = new Guid("{7D960B07-7AF8-11D0-8E5E-00A0C911005A}");
        public static readonly Guid guidEditorFactory = new Guid(guidEditorFactoryString);

        public static readonly Guid GuidPerfPkg = new Guid("{F4A63B2A-49AB-4b2d-AA59-A10F01026C89}");
    };
}