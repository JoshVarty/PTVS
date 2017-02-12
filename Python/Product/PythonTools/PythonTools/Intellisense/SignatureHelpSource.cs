using System.Linq;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    internal class SignatureHelpSource : ISignatureHelpSource {
        private readonly ITextBuffer _textBuffer;
        private readonly SignatureHelpSourceProvider _provider;

        public SignatureHelpSource(SignatureHelpSourceProvider provider, ITextBuffer textBuffer) {
            _textBuffer = textBuffer;
            _provider = provider;
        }

        public ISignature GetBestMatch(ISignatureHelpSession session) {
            return null;
        }

        public void AugmentSignatureHelpSession(ISignatureHelpSession session, System.Collections.Generic.IList<ISignature> signatures) {
            var span = session.GetApplicableSpan(_textBuffer);

            var sigs = _provider._serviceProvider.GetPythonToolsService().GetSignatures(
                session.TextView,
                _textBuffer.CurrentSnapshot,
                span
            );
            if (sigs != null) {
                ISignature curSig = sigs.Signatures
                     .OrderBy(s => s.Parameters.Count)
                     .FirstOrDefault(s => sigs.ParameterIndex < s.Parameters.Count);

                foreach (var sig in sigs.Signatures) {
                    signatures.Add(sig);
                }

                if (curSig != null) {
                    // save the current sig so we don't need to recalculate it (we can't set it until
                    // the signatures are added by our caller).
                    session.Properties.AddProperty(typeof(PythonSignature), curSig);
                }
            }
        }

        public void Dispose() {
        }
    }
}
