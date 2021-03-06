using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtilities {
    public static class FileUtils {
        /// <summary>
        /// Safely enumerates all subdirectories under a given root. If a
        /// subdirectory is inaccessible, it will not be returned (compare and
        /// contrast with Directory.GetDirectories, which will crash without
        /// returning any subdirectories at all).
        /// </summary>
        /// <param name="root">
        /// Directory to enumerate under. This is not returned from this
        /// function.
        /// </param>
        /// <param name="recurse">
        /// <c>true</c> to return subdirectories of subdirectories.
        /// </param>
        /// <param name="fullPaths">
        /// <c>true</c> to return full paths for all subdirectories. Otherwise,
        /// the relative path from <paramref name="root"/> is returned.
        /// </param>
        public static IEnumerable<string> EnumerateDirectories(
            string root,
            bool recurse = true,
            bool fullPaths = true
        ) {
            var queue = new Queue<string>();
            if (!root.EndsWith("\\")) {
                root += "\\";
            }
            queue.Enqueue(root);

            while (queue.Any()) {
                var path = queue.Dequeue();
                if (!path.EndsWith("\\")) {
                    path += "\\";
                }

                IEnumerable<string> dirs = null;
                try {
                    dirs = Directory.GetDirectories(path);
                } catch (UnauthorizedAccessException) {
                } catch (IOException) {
                }
                if (dirs == null) {
                    continue;
                }

                foreach (var d in dirs) {
                    if (!fullPaths && !d.StartsWith(root, StringComparison.OrdinalIgnoreCase)) {
                        continue;
                    }
                    if (recurse) {
                        queue.Enqueue(d);
                    }
                    yield return fullPaths ? d : d.Substring(root.Length);
                }
            }
        }

        /// <summary>
        /// Safely enumerates all files under a given root. If a subdirectory is
        /// inaccessible, its files will not be returned (compare and contrast
        /// with Directory.GetFiles, which will crash without returning any
        /// files at all).
        /// </summary>
        /// <param name="root">
        /// Directory to enumerate.
        /// </param>
        /// <param name="pattern">
        /// File pattern to return. You may use wildcards * and ?.
        /// </param>
        /// <param name="recurse">
        /// <c>true</c> to return files within subdirectories.
        /// </param>
        /// <param name="fullPaths">
        /// <c>true</c> to return full paths for all subdirectories. Otherwise,
        /// the relative path from <paramref name="root"/> is returned.
        /// </param>
        public static IEnumerable<string> EnumerateFiles(
            string root,
            string pattern = "*",
            bool recurse = true,
            bool fullPaths = true
        ) {
            if (!root.EndsWith("\\")) {
                root += "\\";
            }

            var dirs = Enumerable.Repeat(root, 1);
            if (recurse) {
                dirs = dirs.Concat(EnumerateDirectories(root, true, false));
            }

            foreach (var dir in dirs) {
                var fullDir = Path.IsPathRooted(dir) ? dir : (root + dir);
                var dirPrefix = Path.IsPathRooted(dir) ? "" : (dir.EndsWith("\\") ? dir : (dir + "\\"));

                IEnumerable<string> files = null;
                try {
                    files = Directory.GetFiles(fullDir, pattern);
                } catch (UnauthorizedAccessException) {
                } catch (IOException) {
                }
                if (files == null) {
                    continue;
                }

                foreach (var f in files) {
                    if (fullPaths) {
                        yield return f;
                    } else {
                        var relPath = dirPrefix + Path.GetFileName(f);
                        if (File.Exists(root + relPath)) {
                            yield return relPath;
                        }
                    }
                }
            }
        }

        public static void CopyDirectory(string sourceDir, string destDir) {
            sourceDir = sourceDir.TrimEnd('\\');
            destDir = destDir.TrimEnd('\\');
            try {
                Directory.CreateDirectory(destDir);
            } catch (IOException) {
            }

            var newDirectories = new HashSet<string>(EnumerateDirectories(sourceDir, fullPaths: false), StringComparer.OrdinalIgnoreCase);
            newDirectories.ExceptWith(EnumerateDirectories(destDir, fullPaths: false));

            foreach (var newDir in newDirectories.OrderBy(i => i.Length).Select(i => Path.Combine(destDir, i))) {
                try {
                    Directory.CreateDirectory(newDir);
                } catch {
                    Debug.WriteLine("Failed to create directory " + newDir);
                }
            }

            var newFiles = new HashSet<string>(EnumerateFiles(sourceDir, fullPaths: false), StringComparer.OrdinalIgnoreCase);
            newFiles.ExceptWith(EnumerateFiles(destDir, fullPaths: false));

            foreach (var newFile in newFiles) {
                var copyFrom = Path.Combine(sourceDir, newFile);
                var copyTo = Path.Combine(destDir, newFile);
                try {
                    File.Copy(copyFrom, copyTo);
                    File.SetAttributes(copyTo, FileAttributes.Normal);
                } catch {
                    Debug.WriteLine("Failed to copy " + copyFrom + " to " + copyTo);
                }
            }
        }

        public static void DeleteDirectory(string path) {
            Trace.TraceInformation("Removing directory: {0}", path);
            NativeMethods.RecursivelyDeleteDirectory(path, silent: true);
        }

        public static IDisposable Backup(string path) {
            var backup = Path.GetTempFileName();
            File.Delete(backup);
            File.Copy(path, backup);
            return new FileRestorer(path, backup);
        }

        public static IDisposable TemporaryTextFile(out string path, string content) {
            var tempPath = TestData.GetTempPath();
            for (int retries = 100; retries > 0; --retries) {
                path = Path.Combine(tempPath, Path.GetRandomFileName());
                try {
                    using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                    using (var writer = new StreamWriter(stream, Encoding.Default, 128, true)) {
                        writer.Write(content);
                        return new FileDeleter(path);
                    }
                } catch (IOException) {
                } catch (UnauthorizedAccessException) {
                }
            }
            Assert.Fail("Failed to create temporary file.");
            throw new InvalidOperationException();
        }

        private sealed class FileDeleter : IDisposable {
            private readonly string _path;

            public FileDeleter(string path) {
                _path = path;
            }
            
            public void Dispose() {
                for (int retries = 10; retries > 0; --retries) {
                    try {
                        File.Delete(_path);
                        return;
                    } catch (IOException) {
                    } catch (UnauthorizedAccessException) {
                        try {
                            File.SetAttributes(_path, FileAttributes.Normal);
                        } catch (IOException) {
                        } catch (UnauthorizedAccessException) {
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }


        private sealed class FileRestorer : IDisposable {
            private readonly string _original, _backup;

            public FileRestorer(string original, string backup) {
                _original = original;
                _backup = backup;
            }

            public void Dispose() {
                for (int retries = 10; retries > 0; --retries) {
                    try {
                        File.Delete(_original);
                        File.Move(_backup, _original);
                        return;
                    } catch (IOException) {
                    } catch (UnauthorizedAccessException) {
                        try {
                            File.SetAttributes(_original, FileAttributes.Normal);
                        } catch (IOException) {
                        } catch (UnauthorizedAccessException) {
                        }
                    }
                    Thread.Sleep(100);
                }

                Assert.Fail("Failed to restore {0} from {1}", _original, _backup);
            }
        }
    }
}
