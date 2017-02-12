using System;

namespace TestUtilities {
    public interface IAddNewItem : IDisposable {
        string FileName { get; set; }

        void OK();
    }
}
