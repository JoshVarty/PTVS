using System.Collections.Generic;

namespace Microsoft.PythonTools.Debugger {
    class IdDispenser {
        private readonly List<int> _freedInts = new List<int>();
        private int _curValue;

        public int Allocate() {
            lock (this) {
                if (_freedInts.Count > 0) {
                    int res = _freedInts[_freedInts.Count - 1];
                    _freedInts.RemoveAt(_freedInts.Count - 1);
                    return res;
                } else {
                    int res = _curValue++;
                    return res;
                }
            }
        }

        public void Free(int id) {
            lock (this) {
                if (id + 1 == _curValue) {
                    _curValue--;
                } else {
                    _freedInts.Add(id);
                }
            }
        }
    }
}
