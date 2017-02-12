using System.Diagnostics;
using System.Text;

namespace TestUtilities {
    public class OutputReceiver {
        public readonly StringBuilder Output = new StringBuilder();

        public void OutputDataReceived(object sender, DataReceivedEventArgs e) {
            if (e.Data != null) {
                Output.AppendLine(e.Data);
            }
        }
    }
}
