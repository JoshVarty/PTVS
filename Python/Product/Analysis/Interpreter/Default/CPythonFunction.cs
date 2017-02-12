using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonFunction : IPythonFunction, ILocatedMember {
        private readonly string _name;
        private readonly string _doc;
        private readonly bool _hasLocation;
        private readonly int _line, _column;
        private readonly IPythonType _declaringType;
        private readonly CPythonModule _declaringModule;
        private readonly List<IPythonFunctionOverload> _overloads;
        private readonly bool _isBuiltin, _isStatic, _isClassMethod;
        private static readonly List<IPythonFunctionOverload> EmptyOverloads = new List<IPythonFunctionOverload>();

        internal CPythonFunction(string name, string doc, bool isBuiltin, bool isStatic, IMemberContainer declaringType) {
            _name = name;
            _doc = doc;
            _isBuiltin = isBuiltin;
            _isStatic = isStatic;
            _declaringModule = CPythonModule.GetDeclaringModuleFromContainer(declaringType);
            _declaringType = declaringType as IPythonType;
            _overloads = new List<IPythonFunctionOverload>();
        }
        
        public CPythonFunction(ITypeDatabaseReader typeDb, string name, Dictionary<string, object> functionTable, IMemberContainer declaringType, bool isMethod = false) {
            _name = name;
            
            object doc;
            if (functionTable.TryGetValue("doc", out doc)) {
                _doc = doc as string;
            }

            object value;
            if (functionTable.TryGetValue("builtin", out value)) {
                _isBuiltin = Convert.ToBoolean(value);
            } else {
                _isBuiltin = true;
            }

            if (functionTable.TryGetValue("static", out value)) {
                _isStatic = Convert.ToBoolean(value);
            } else {
                _isStatic = false;
            }

            if (functionTable.TryGetValue("classmethod", out value)) {
                _isClassMethod = Convert.ToBoolean(value);
            } else {
                _isClassMethod = false;
            }

            _hasLocation = PythonTypeDatabase.TryGetLocation(functionTable, ref _line, ref _column);

            _declaringModule = CPythonModule.GetDeclaringModuleFromContainer(declaringType);
            _declaringType = declaringType as IPythonType;

            if (functionTable.TryGetValue("overloads", out value)) {
                _overloads = LoadOverloads(typeDb, value, isMethod);
            }
        }

        private List<IPythonFunctionOverload> LoadOverloads(ITypeDatabaseReader typeDb, object data, bool isMethod) {
            var overloads = data as List<object>;
            if (overloads != null) {
                return overloads
                    .OfType<Dictionary<string, object>>()
                    .Select(o => new CPythonFunctionOverload(typeDb, this, o, isMethod))
                    .ToList<IPythonFunctionOverload>();
            }
            return EmptyOverloads;
        }

        #region IBuiltinFunction Members

        public string Name {
            get { return _name; }
        }

        public string Documentation {
            get { return _doc; }
        }

        public IList<IPythonFunctionOverload> Overloads {
            get { return _overloads; }
        }

        public IPythonType DeclaringType {
            get { return _declaringType; }
        }

        public IPythonModule DeclaringModule {
            get { return _declaringModule; }
        }

        public bool IsBuiltin {
            get {
                return _isBuiltin;
            }
        }

        public bool IsStatic {
            get {
                return _isStatic;
            }
        }

        public bool IsClassMethod {
            get {
                return _isClassMethod;
            }
        }


        #endregion

        #region IMember Members

        public PythonMemberType MemberType {
            get { return PythonMemberType.Function; }
        }

        #endregion

        #region ILocatedMember Members

        public IEnumerable<LocationInfo> Locations {
            get {
                if (_hasLocation) {
                    yield return new LocationInfo(_declaringModule.FilePath, _line, _column);
                }
            }
        }

        #endregion
    }
}
