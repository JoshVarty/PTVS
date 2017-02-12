using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtilities {
    public class SessionHolder<T> : IDisposable where T : IIntellisenseSession {
        public readonly T Session;
        private readonly IEditor _owner;

        public SessionHolder(T session, IEditor owner) {
            Assert.IsNotNull(session);
            Session = session;
            _owner = owner;
        }

        void IDisposable.Dispose() {
            if (!Session.IsDismissed) {
                _owner.Invoke(() => { Session.Dismiss(); });
            }
        }
    }

}
