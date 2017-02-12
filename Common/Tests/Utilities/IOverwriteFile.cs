using System;

namespace TestUtilities {
    public interface IOverwriteFile : IDisposable {
        string Text { get; }

        void No();

        bool AllItems { get; set; }

        void Yes();

        void Cancel();
    }
}
