using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.PythonTools.Profiling {
    [ComVisible(true)]
    public sealed class AutomationProfiling : IPythonProfiling {
        private readonly SessionsNode _sessions;

        internal AutomationProfiling(SessionsNode sessions) {
            _sessions = sessions;
        }

        public IPythonProfileSession GetSession(object item) {
            if (item is int) {
                int id = (int)item - 1;
                if (id >= 0 && id < _sessions.Sessions.Count) {
                    return _sessions.Sessions[id].GetAutomationObject();
                }
            } else if (item is string) {
                string name = (string)item;
                foreach (var session in _sessions.Sessions) {
                    if (session.Name == name) {
                        return session.GetAutomationObject();
                    }
                }
            }
            return null;
        }

        public IPythonProfileSession LaunchProject(EnvDTE.Project projectToProfile, bool openReport) {
            var target = new ProfilingTarget();
            target.ProjectTarget = new ProjectTarget();
            target.ProjectTarget.TargetProject = new Guid(projectToProfile.Properties.Item("Guid").Value as string);
            target.ProjectTarget.FriendlyName = projectToProfile.Name;

            return PythonProfilingPackage.Instance.ProfileTarget(target, openReport).GetAutomationObject();
        }

        public IPythonProfileSession LaunchProcess(string interpreter, string script, string workingDir, string arguments, bool openReport) {
            var target = new ProfilingTarget();
            target.StandaloneTarget = new StandaloneTarget();
            target.StandaloneTarget.WorkingDirectory = workingDir;
            target.StandaloneTarget.Script = script;
            target.StandaloneTarget.Arguments = arguments;

            if (File.Exists(interpreter)) {
                target.StandaloneTarget.InterpreterPath = interpreter;
            } else {
                target.StandaloneTarget.PythonInterpreter = new PythonInterpreter();
                target.StandaloneTarget.PythonInterpreter.Id = interpreter;
            }

            return PythonProfilingPackage.Instance.ProfileTarget(target, openReport).GetAutomationObject();
        }

        public void RemoveSession(IPythonProfileSession session, bool deleteFromDisk) {
            for (int i = 0; i < _sessions.Sessions.Count; i++) {
                if (session == _sessions.Sessions[i].GetAutomationObject()) {
                    _sessions.DeleteItem(
                        (uint)(deleteFromDisk ? __VSDELETEITEMOPERATION.DELITEMOP_DeleteFromStorage : __VSDELETEITEMOPERATION.DELITEMOP_RemoveFromProject),
                        (uint)_sessions.Sessions[i].ItemId
                    );
                    return;
                }
            }
            throw new InvalidOperationException(Strings.SessionAlreadyRemoved);
        }

        public bool IsProfiling {
            get { return PythonProfilingPackage.Instance.IsProfiling; }
        }

    }
}
