using System.Collections.Generic;
using IronPython.Runtime;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonBuiltinModule : IronPythonModule, IBuiltinPythonModule {

        public IronPythonBuiltinModule(IronPythonInterpreter interpreter, ObjectIdentityHandle mod, string name)
            : base(interpreter, mod, name) {
        }

        public IMember GetAnyMember(string name) {
            switch (name) {
                case "NoneType": return Interpreter.GetBuiltinType(BuiltinTypeId.NoneType);
                case "generator": return Interpreter.GetBuiltinType(BuiltinTypeId.Generator);
                case "builtin_function": return Interpreter.GetBuiltinType(BuiltinTypeId.BuiltinFunction);
                case "builtin_method_descriptor": return Interpreter.GetBuiltinType(BuiltinTypeId.BuiltinMethodDescriptor);
                case "dict_keys": return Interpreter.GetBuiltinType(BuiltinTypeId.DictKeys);
                case "dict_values": return Interpreter.GetBuiltinType(BuiltinTypeId.DictValues);
                case "function": return Interpreter.GetBuiltinType(BuiltinTypeId.Function);
                case "ellipsis": return Interpreter.GetBuiltinType(BuiltinTypeId.Ellipsis);
            }

            return base.GetMember(null, name);
        }
    }
}
