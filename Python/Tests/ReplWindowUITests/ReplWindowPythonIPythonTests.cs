using System.Threading;
using Microsoft.PythonTools;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.UI;
using TestUtilities.UI.Python;
using Keyboard = TestUtilities.UI.Keyboard;

namespace ReplWindowUITests {
    /// <summary>
    /// These tests are run for important versions of Python that use or support
    /// IPython mode in the REPL.
    /// </summary>
    [TestClass, Ignore]
    public abstract class ReplWindowPythonIPythonTests : ReplWindowPythonTests {
        internal virtual ReplWindowProxy PrepareIPython(
            bool addNewLineAtEndOfFullyTypedWord = false
        ) {
            return Prepare(useIPython: true, addNewLineAtEndOfFullyTypedWord: addNewLineAtEndOfFullyTypedWord);
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonMode() {
            using (var interactive = PrepareIPython()) {
                interactive.SubmitCode("x = 42\n?x");

                interactive.WaitForText(
                    ">x = 42",
                    ">?x",
                    ((PythonReplWindowProxySettings)interactive.Settings).IPythonIntDocumentation,
                    ">"
                );
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonCtrlBreakAborts() {
            using (var interactive = PrepareIPython()) {
                var code = "while True: pass";
                interactive.Type(code + "\n", waitForLastLine: false);
                interactive.WaitForText(">" + code, ".", "");

                Thread.Sleep(2000);

                interactive.CancelExecution();

                // we can potentially get different output depending on where the Ctrl-C gets caught.
                try {
                    interactive.WaitForTextStart(">while True: pass", ".", "KeyboardInterrupt caught in kernel");
                } catch {
                    interactive.WaitForTextStart(">while True: pass", ".",
                        "---------------------------------------------------------------------------",
                        "KeyboardInterrupt                         Traceback (most recent call last)");
                }
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonSimpleCompletion() {
            using (var interactive = PrepareIPython(addNewLineAtEndOfFullyTypedWord: false)) {
                interactive.SubmitCode("x = 42");
                interactive.WaitForText(">x = 42", ">");
                interactive.ClearScreen();

                Keyboard.Type("x.");

                using (var sh = interactive.WaitForSession<ICompletionSession>()) {
                    // commit entry
                    sh.Commit();
                    sh.WaitForSessionDismissed();
                }

                interactive.WaitForText(">x." + ((PythonReplWindowProxySettings)interactive.Settings).IntFirstMember);

                // clear input at repl
                interactive.ClearInput();

                // try it again, and dismiss the session
                Keyboard.Type("x.");
                using (interactive.WaitForSession<ICompletionSession>()) { }
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonSimpleSignatureHelp() {
            using (var interactive = PrepareIPython()) {
                Assert.IsNotNull(interactive);

                interactive.SubmitCode("def f(): pass");
                interactive.WaitForText(">def f(): pass", ">");

                Keyboard.Type("f(");

                using (var sh = interactive.WaitForSession<ISignatureHelpSession>()) {
                    Assert.AreEqual("<no docstring>", sh.Session.SelectedSignature.Documentation);
                }
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonInlineGraph() {
            using (var interactive = PrepareIPython()) {
                interactive.SubmitCode(@"from pylab import *
x = linspace(0, 4*pi)
plot(x, x)");
                interactive.WaitForTextStart(
                    ">from pylab import *",
                    ">x = linspace(0, 4*pi)",
                    ">plot(x, x)",
                    "Out["
                );

                Thread.Sleep(2000);

                var tags = WaitForTags(interactive);
                Assert.AreEqual(1, tags.Length);
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void IPythonStartInInteractive() {
            using (var interactive = PrepareIPython())
            using (new DefaultInterpreterSetter(interactive.GetAnalyzer().InterpreterFactory)) {
                var project = interactive.App.OpenProject(@"TestData\InteractiveFile.sln");

                interactive.App.ExecuteCommand("Python.ExecuteInInteractive");
                interactive.WaitForTextEnd("Program.pyabcdef", ">");
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void ExecuteInIPythonReplSysArgv() {
            using (var interactive = PrepareIPython())
            using (new DefaultInterpreterSetter(interactive.GetAnalyzer().InterpreterFactory)) {
                var project = interactive.App.OpenProject(@"TestData\SysArgvRepl.sln");

                interactive.App.ExecuteCommand("Python.ExecuteInInteractive");
                interactive.WaitForTextEnd("Program.py']", ">");
            }
        }

        [TestMethod, Priority(1)]
        [HostType("VSTestHost"), TestCategory("Installed")]
        public virtual void ExecuteInIPythonReplSysArgvScriptArgs() {
            using (var interactive = PrepareIPython())
            using (new DefaultInterpreterSetter(interactive.GetAnalyzer().InterpreterFactory)) {
                var project = interactive.App.OpenProject(@"TestData\SysArgvScriptArgsRepl.sln");

                interactive.App.ExecuteCommand("Python.ExecuteInInteractive");
                interactive.WaitForTextEnd(@"Program.py', '-source', 'C:\\Projects\\BuildSuite', '-destination', 'C:\\Projects\\TestOut', '-pattern', '*.txt', '-recurse', 'true']", ">");
            }
        }
    }
}
