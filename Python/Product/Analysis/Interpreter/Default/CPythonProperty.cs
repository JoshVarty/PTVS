using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonProperty : IBuiltinProperty, ILocatedMember {
        private readonly string _doc;
        private IPythonType _type;
        private readonly CPythonModule _declaringModule;
        private readonly bool _hasLocation;
        private readonly int _line, _column;
        
        public CPythonProperty(ITypeDatabaseReader typeDb, Dictionary<string, object> valueDict, IMemberContainer container) {
            _declaringModule = CPythonModule.GetDeclaringModuleFromContainer(container);

            object value;
            if (valueDict.TryGetValue("doc", out value)) {
                _doc = value as string;
            }

            object type;
            if (!valueDict.TryGetValue("type", out type) || type == null) {
                type = new[] { null, "object" };
            }

            _hasLocation = PythonTypeDatabase.TryGetLocation(valueDict, ref _line, ref _column);

            typeDb.LookupType(type, typeValue => _type = typeValue);
        }

        #region IBuiltinProperty Members

        public IPythonType Type {
            get { return _type; }
        }

        public bool IsStatic {
            get { return false; }
        }

        public string Documentation {
            get { return _doc; }
        }

        public string Description {
            get {
                if (Type == null) {
                    return "property of unknown type";
                } else {
                    return "property of type " + Type.Name;
                }
            }
        }

        #endregion

        #region IMember Members

        public PythonMemberType MemberType {
            get { return PythonMemberType.Property; }
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
