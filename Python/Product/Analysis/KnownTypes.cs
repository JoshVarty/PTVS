using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Analysis.Values;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.PyAnalysis {
    internal interface IKnownPythonTypes {
        IPythonType this[BuiltinTypeId id] { get; }
    }

    internal interface IKnownClasses {
        BuiltinClassInfo this[BuiltinTypeId id] { get; }
    }

    internal class KnownTypes : IKnownPythonTypes, IKnownClasses {
        internal readonly IPythonType[] _types;
        internal readonly BuiltinClassInfo[] _classInfos;

        public static KnownTypes CreateDefault(PythonAnalyzer state, PythonTypeDatabase fallbackDb) {
            var res = new KnownTypes();

            var fallback = fallbackDb.BuiltinModule;

            for (int value = 0; value < res._types.Length; ++value) {
                res._types[value] = (IPythonType)fallback.GetAnyMember(
                    ((ITypeDatabaseReader)fallbackDb).GetBuiltinTypeName((BuiltinTypeId)value)
                );
            }

            res.SetClassInfo(state);
            return res;
        }

        public static KnownTypes Create(PythonAnalyzer state, PythonTypeDatabase fallbackDb) {
            var res = new KnownTypes();

            var interpreter = state.Interpreter;
            var fallback = fallbackDb.BuiltinModule;

            for (int value = 0; value < res._types.Length; ++value) {
                IPythonType type;
                try {
                    type = interpreter.GetBuiltinType((BuiltinTypeId)value);
                } catch (KeyNotFoundException) {
                    type = null;
                }
                if (type == null) {
                    type = (IPythonType)fallback.GetAnyMember(
                        ((ITypeDatabaseReader)fallbackDb).GetBuiltinTypeName((BuiltinTypeId)value)
                    );
                }
                res._types[value] = type ?? new FallbackBuiltinPythonType(state.LanguageVersion, (BuiltinTypeId)value);
            }

            res.SetClassInfo(state);
            return res;
        }

        private KnownTypes() {
            int count = (int)BuiltinTypeIdExtensions.LastTypeId + 1;
            _types = new IPythonType[count];
            _classInfos = new BuiltinClassInfo[count];
        }

        private void SetClassInfo(PythonAnalyzer state) {
            for (int value = 0; value < _types.Length; ++value) {
                if (_types[value] != null) {
                    _classInfos[value] = state.GetBuiltinType(_types[value]);
                }
            }
        }

        IPythonType IKnownPythonTypes.this[BuiltinTypeId id] {
            get {
                return _types[(int)id];
            }
        }

        BuiltinClassInfo IKnownClasses.this[BuiltinTypeId id] {
            get {
                return _classInfos[(int)id];
            }
        }
    }

    class FallbackBuiltinModule : IPythonModule {
        public static readonly IPythonModule Instance2x = new FallbackBuiltinModule(SharedDatabaseState.BuiltinName2x);
        public static readonly IPythonModule Instance3x = new FallbackBuiltinModule(SharedDatabaseState.BuiltinName3x);

        private FallbackBuiltinModule(string name) {
            Name = name;
        }

        public string Documentation => string.Empty;
        public PythonMemberType MemberType => PythonMemberType.Module;
        public string Name { get; }
        public IEnumerable<string> GetChildrenModules() => Enumerable.Empty<string>();
        public IMember GetMember(IModuleContext context, string name) => null;
        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) => Enumerable.Empty<string>();
        public void Imported(IModuleContext context) { }
    }

    class FallbackBuiltinPythonType : IPythonType {
        public FallbackBuiltinPythonType(PythonLanguageVersion version, BuiltinTypeId typeId) {
            DeclaringModule = version.Is3x() ? FallbackBuiltinModule.Instance3x : FallbackBuiltinModule.Instance2x;
            Name = SharedDatabaseState.GetBuiltinTypeName(typeId, version.ToVersion());
            TypeId = typeId;
        }

        public IPythonModule DeclaringModule { get; }
        public string Documentation => string.Empty;
        public bool IsBuiltin => true;
        public PythonMemberType MemberType => PythonMemberType.Class;
        public IList<IPythonType> Mro => new[] { (IPythonType)this };
        public string Name { get; }
        public BuiltinTypeId TypeId {get;}
        public IPythonFunction GetConstructors() => null;
        public IMember GetMember(IModuleContext context, string name) => null;
        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) => Enumerable.Empty<string>();
    }
}
