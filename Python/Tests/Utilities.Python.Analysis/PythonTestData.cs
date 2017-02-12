using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudioTools;

namespace TestUtilities.Python {
    public class PythonTestData {
#if DEBUG
        const string Configuration = "Debug";
#else
        const string Configuration = "Release";
#endif

        const string BinariesInSourceTree = "BuildOutput\\" + Configuration + AssemblyVersionInfo.VSVersion + "\\raw\\binaries";
        const string BinariesInTestDrop = "binaries";
        const string BinariesInReleaseDrop = "raw\\binaries";
        const string BinariesLandmark = "Microsoft.PythonTools.Analysis.dll";

        const string TestDataInSourceTree = "Python\\Tests\\TestData";
        const string TestDataInTestDrop = "binaries\\TestData";
        const string TestDataInReleaseDrop = "raw\\binaries\\TestData";
        const string TestDataLandmark = "testdata.root";

        private static string FindDirectoryFromLandmark(string root, string directory, string landmark = null) {
            if (string.IsNullOrEmpty(root) || string.IsNullOrEmpty(directory)) {
                return null;
            }

            var path = CommonUtils.GetAbsoluteDirectoryPath(root, directory);
            if (landmark != null) {
                return File.Exists(CommonUtils.GetAbsoluteFilePath(path, landmark)) ? path : null;
            }
            return Directory.Exists(path) ? path : null;
        }

        private static string GetRootDir() {
            var dir = CommonUtils.GetParent((typeof(TestData)).Assembly.Location);
            while (!string.IsNullOrEmpty(dir) &&
                Directory.Exists(dir) &&
                !File.Exists(CommonUtils.GetAbsoluteFilePath(dir, "build.root"))) {
                dir = CommonUtils.GetParent(dir);
            }
            return dir ?? "";
        }

        public static void Deploy(bool includeTestData = true) {
            var binSource = Environment.GetEnvironmentVariable("PTVS_BINARIES_SOURCE");
            var testDataSource = Environment.GetEnvironmentVariable("PTVS_TESTDATA_SOURCE");

            var drop = Environment.GetEnvironmentVariable("PTVS_DROP") ??
                CommonUtils.GetParent(CommonUtils.GetParent(typeof(TestData).Assembly.Location));
            string buildRoot = null;

            if (string.IsNullOrEmpty(binSource)) {
                buildRoot = buildRoot ?? GetRootDir();
                binSource = FindDirectoryFromLandmark(buildRoot, BinariesInSourceTree, BinariesLandmark)
                    ?? FindDirectoryFromLandmark(drop, BinariesInTestDrop, BinariesLandmark)
                    ?? FindDirectoryFromLandmark(drop, BinariesInReleaseDrop, BinariesLandmark);
            }

            if (string.IsNullOrEmpty(testDataSource) && includeTestData) {
                buildRoot = buildRoot ?? GetRootDir();
                testDataSource = FindDirectoryFromLandmark(buildRoot, TestDataInSourceTree, TestDataLandmark)
                    ?? FindDirectoryFromLandmark(drop, TestDataInTestDrop, TestDataLandmark)
                    ?? FindDirectoryFromLandmark(drop, TestDataInReleaseDrop, TestDataLandmark);
            }

            Debug.Assert(Directory.Exists(binSource), "Unable to find binaries at " + (binSource ?? "(null)"));

            Trace.TraceInformation("Copying binaries from {0}", binSource);

            FileUtils.CopyDirectory(binSource, TestData.GetPath());

            if (includeTestData) {
                Debug.Assert(Directory.Exists(testDataSource), "Unable to find test data at " + (testDataSource ?? "(null)"));
                FileUtils.CopyDirectory(testDataSource, TestData.GetPath("TestData"));
            }
        }

        public static string GetUnicodePathSolution() {
            var tempProject = TestData.GetTempPath("UnicodePath");
            var tempSln = Path.Combine(tempProject, "UnicodePath�.sln");
            if (!File.Exists(tempSln)) {
                FileUtils.CopyDirectory(TestData.GetPath("TestData\\UnicodePath"), Path.Combine(tempProject, "UnicodePath�"));
                File.Copy(TestData.GetPath("TestData\\UnicodePath.sln"), tempSln);
            }
            return tempSln;
        }
    }
}
