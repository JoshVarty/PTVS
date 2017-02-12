using System;

namespace TestUtilities {
    public interface IAddExistingItem : IDisposable {
        void OK();
        void Add();
        void AddLink();
        string FileName {
            get;
            set;
        }
    }
}
