using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies {
    internal struct CliStructProxy<TStruct> : IWritableDataProxy<TStruct> {
        public DkmProcess Process { get; private set; }
        public ulong Address { get; private set; }

        public CliStructProxy(DkmProcess process, ulong address)
            : this() {
            Debug.Assert(process != null && address != 0);
            Process = process;
            Address = address;
        }

        public long ObjectSize {
            get { return Marshal.SizeOf(typeof(TStruct)); }
        }

        public unsafe TStruct Read() {
            var buf = new byte[ObjectSize];
            Process.ReadMemory(Address, DkmReadMemoryFlags.None, buf);
            fixed (byte* p = buf) {
                return (TStruct)Marshal.PtrToStructure((IntPtr)p, typeof(TStruct));
            }
        }

        object IValueStore.Read() {
            return Read();
        }

        public unsafe void Write(TStruct value) {
            var buf = new byte[ObjectSize];
            fixed (byte* p = buf) {
                Marshal.StructureToPtr(value, (IntPtr)p, false);
            }
            Process.WriteMemory(Address, buf);
        }

        void IWritableDataProxy.Write(object value) {
            Write((TStruct)value);
        }
    }
}
