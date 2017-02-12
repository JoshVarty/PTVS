using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtilities {
    public sealed class ProcessScope : IDisposable {
        private readonly string[] _names;
        private readonly HashSet<int> _alreadyRunning;
        private readonly HashSet<int> _alreadyWaited;

        public ProcessScope(params string[] names) {
            _names = names;

            _alreadyRunning = new HashSet<int>(
                _names.SelectMany(n => Process.GetProcessesByName(n).Select(p => p.Id))
            );
            _alreadyWaited = new HashSet<int>(_alreadyRunning);
        }

        public IEnumerable<Process> WaitForNewProcess(TimeSpan timeout) {
            var end = DateTime.Now + timeout;
            while (DateTime.Now < end) {
                var nowRunning = _names
                    .SelectMany(n => Process.GetProcessesByName(n))
                    .Where(p => !_alreadyWaited.Contains(p.Id))
                    .ToList();
                if (nowRunning.Any()) {
                    _alreadyWaited.UnionWith(nowRunning.Select(p => p.Id));
                    return nowRunning;
                }

                Thread.Sleep(100);
            }

            return Enumerable.Empty<Process>();
        }
        
        public void Dispose() {
            var end = DateTime.Now + TimeSpan.FromSeconds(30.0);
            while (DateTime.Now < end) {
                if (ExitNewProcesses()) {
                    return;
                }
                Thread.Sleep(100);
            }
            Assert.Fail("Failed to close all processes");
        }

        public bool ExitNewProcesses() {
            var newProcesses = _names
                .SelectMany(n => Process.GetProcessesByName(n))
                .Where(p => !_alreadyRunning.Contains(p.Id));
            bool allGone = true;
            foreach (var p in newProcesses) {
                if (!p.HasExited) {
                    try {
                        p.Kill();
                    } catch (Exception ex) {
                        allGone = false;
                        Trace.TraceWarning("Failed to kill {0} ({1}).{2}{3}",
                            p.ProcessName,
                            p.Id,
                            Environment.NewLine,
                            ex.ToString()
                        );
                    }
                }
            }
            return allGone;
        }
    }
}
