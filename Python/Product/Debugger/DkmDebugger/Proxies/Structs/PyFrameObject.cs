using System;
using System.Diagnostics;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.CallStack;
using Microsoft.VisualStudio.Debugger.Evaluation;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyFrameObject : PyVarObject {
        public class Fields_27_35 {
            public StructField<PointerProxy<PyCodeObject>> f_code;
            public StructField<PointerProxy<PyDictObject>> f_globals;
            public StructField<PointerProxy<PyDictObject>> f_locals;
            public StructField<Int32Proxy> f_lineno;
            public StructField<ArrayProxy<PointerProxy<PyObject>>> f_localsplus;
        }

        public class Fields_36 {
            public StructField<PointerProxy<PyFrameObject>> f_back;
            public StructField<PointerProxy<PyCodeObject>> f_code;
            public StructField<PointerProxy<PyDictObject>> f_globals;
            public StructField<PointerProxy<PyDictObject>> f_locals;
            public StructField<Int32Proxy> f_lineno;
            public StructField<ArrayProxy<PointerProxy<PyObject>>> f_localsplus;
        }

        private readonly object _fields;

        public PyFrameObject(DkmProcess process, ulong address)
            : base(process, address) {
            var pythonInfo = process.GetPythonRuntimeInfo();
            if (pythonInfo.LanguageVersion <= PythonLanguageVersion.V35) {
                Fields_27_35 fields;
                InitializeStruct(this, out fields);
                _fields = fields;
            } else {
                Fields_36 fields;
                InitializeStruct(this, out fields);
                _fields = fields;
            }
            CheckPyType<PyFrameObject>();
        }

        private static bool IsInEvalFrame(DkmStackWalkFrame frame) {
            var process = frame.Process;
            var pythonInfo = process.GetPythonRuntimeInfo();
            ulong addr = 0;
            if (pythonInfo.LanguageVersion <= PythonLanguageVersion.V35) {
                if (frame.ModuleInstance == pythonInfo.DLLs.Python) {
                    addr = pythonInfo.DLLs.Python.GetFunctionAddress("PyEval_EvalFrameEx");
                }
            } else {
                if (frame.ModuleInstance == pythonInfo.DLLs.DebuggerHelper) {
                    addr = pythonInfo.DLLs.DebuggerHelper.GetFunctionAddress("EvalFrameFunc");
                }
            }

            if (addr == 0) {
                return false;
            }

            return frame.InstructionAddress.IsInSameFunction(process.CreateNativeInstructionAddress(addr));
        }

        public static unsafe PyFrameObject TryCreate(DkmStackWalkFrame frame) {
            var process = frame.Process;

            if (frame.InstructionAddress == null) {
                return null;
            } 
            if (frame.RuntimeInstance.Id.RuntimeType != Guids.PythonRuntimeTypeGuid && !IsInEvalFrame(frame)) {
                return null;
            }

            var cppLanguage = DkmLanguage.Create("C++", new DkmCompilerId(Guids.MicrosoftVendorGuid, Guids.CppLanguageGuid));
            var inspectionSession = DkmInspectionSession.Create(process, null);
            var inspectionContext = DkmInspectionContext.Create(inspectionSession, process.GetNativeRuntimeInstance(), frame.Thread, 0,
                DkmEvaluationFlags.TreatAsExpression | DkmEvaluationFlags.NoSideEffects, DkmFuncEvalFlags.None, 10, cppLanguage, null);

            CppExpressionEvaluator cppEval;
            try {
                cppEval = new CppExpressionEvaluator(inspectionContext, frame);
            } catch (ArgumentException) {
                Debug.Fail("Failed to create C++ expression evaluator while obtaining PyFrameObject from a native frame.");
                return null;
            }

            ulong framePtr;
            try {
                framePtr = cppEval.EvaluateUInt64("f");
            } catch (CppEvaluationException) {
                Debug.Fail("Failed to evaluate the 'f' parameter to PyEval_EvalFrameEx while obtaining PyFrameObject from a native frame.");
                return null;
            }

            return new PyFrameObject(frame.Process, framePtr);
        }

        public PointerProxy<PyFrameObject> f_back {
            get { return GetFieldProxy((_fields as Fields_36)?.f_back); }
        }

        public PointerProxy<PyCodeObject> f_code {
            get { return GetFieldProxy((_fields as Fields_36)?.f_code ?? (_fields as Fields_27_35)?.f_code); }
        }

        public PointerProxy<PyDictObject> f_globals {
            get { return GetFieldProxy((_fields as Fields_36)?.f_globals ?? (_fields as Fields_27_35)?.f_globals); }
        }

        public PointerProxy<PyDictObject> f_locals {
            get { return GetFieldProxy((_fields as Fields_36)?.f_locals ?? (_fields as Fields_27_35)?.f_locals); }
        }

        public Int32Proxy f_lineno {
            get { return GetFieldProxy((_fields as Fields_36)?.f_lineno ?? (_fields as Fields_27_35)?.f_lineno); }
        }

        public ArrayProxy<PointerProxy<PyObject>> f_localsplus {
            get { return GetFieldProxy((_fields as Fields_36)?.f_localsplus ?? (_fields as Fields_27_35)?.f_localsplus); }
        }

    }
}
