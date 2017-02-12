using System.Collections.Generic;
using Microsoft.CookiecutterTools.Infrastructure;

namespace CookiecutterTests {
    class MockRedirector : Redirector {
        private List<string> ErrorLines { get; } = new List<string>();
        private List<string> Lines { get; } = new List<string>();

        public override void WriteErrorLine(string line) {
            ErrorLines.Add(line);
        }

        public override void WriteLine(string line) {
            Lines.Add(line);
        }
    }
}
