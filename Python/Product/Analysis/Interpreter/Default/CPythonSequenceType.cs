using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonSequenceType : IPythonSequenceType {
        private readonly IPythonType _type;
        private readonly List<IPythonType> _indexTypes;

        public CPythonSequenceType(IPythonType baseType, ITypeDatabaseReader typeDb, List<object> indexTypes) {
            _type = baseType;

            if (indexTypes != null) {
                _indexTypes = new List<IPythonType>();

                foreach (var indexType in indexTypes) {
                    typeDb.LookupType(indexType, type => _indexTypes.Add(type));
                }
            }
        }

        public IEnumerable<IPythonType> IndexTypes {
            get {
                return _indexTypes;
            }
        }


        public IPythonFunction GetConstructors() {
            return _type.GetConstructors();
        }

        public string Name {
            get { return _type.Name; }
        }

        public string Documentation {
            get { return _type.Documentation; }
        }

        public BuiltinTypeId TypeId {
            get { return _type.TypeId; }
        }

        public IList<IPythonType> Mro {
            get { return _type.Mro; }
        }

        public IPythonModule DeclaringModule {
            get { return _type.DeclaringModule; }
        }

        public bool IsBuiltin {
            get { return _type.IsBuiltin; }
        }

        public IMember GetMember(IModuleContext context, string name) {
            return _type.GetMember(context, name);
        }

        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) {
            return _type.GetMemberNames(moduleContext);
        }

        public PythonMemberType MemberType {
            get { return _type.MemberType; }
        }

        public override string ToString() {
            return String.Format("CPythonSequenceType('{0}', '{1}')", Name, string.Join("', '", _indexTypes.Select((t => t.Name))));
        }
    }
}
