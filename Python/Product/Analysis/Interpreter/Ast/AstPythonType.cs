using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Interpreter.Ast {
    class AstPythonType : IPythonType, IMemberContainer, ILocatedMember {
        private readonly Dictionary<string, IMember> _members;

        private static readonly IPythonModule NoDeclModule = new AstPythonModule();

        public AstPythonType(string name) {
            _members = new Dictionary<string, IMember>();
            Name = name;
            DeclaringModule = NoDeclModule;
            Mro = Array.Empty<IPythonType>();
            Locations = Array.Empty<LocationInfo>();
        }

        public AstPythonType(
            PythonAst ast,
            IPythonModule declModule,
            ClassDefinition def,
            string doc,
            LocationInfo loc,
            IEnumerable<IPythonType> mro
        ) {
            _members = new Dictionary<string, IMember>();

            Name = def.Name;
            Documentation = doc;
            DeclaringModule = declModule;
            Mro = mro.MaybeEnumerate().ToArray();
            Locations = new[] { loc };
        }

        internal void AddMembers(IEnumerable<KeyValuePair<string, IMember>> members, bool overwrite) {
            lock (_members) {
                foreach (var kv in members) {
                    if (!overwrite) {
                        IMember existing;
                        if (_members.TryGetValue(kv.Key, out existing)) {
                            continue;
                        }
                    }
                    _members[kv.Key] = kv.Value;
                }
            }
        }

        public string Name { get; }
        public string Documentation { get; }
        public IPythonModule DeclaringModule {get;}
        public IList<IPythonType> Mro { get; }
        public bool IsBuiltin => true;
        public PythonMemberType MemberType => PythonMemberType.Class;
        public BuiltinTypeId TypeId => BuiltinTypeId.Type;

        public IEnumerable<LocationInfo> Locations { get; }

        public IMember GetMember(IModuleContext context, string name) {
            IMember member;
            lock (_members) {
                _members.TryGetValue(name, out member);
            }
            return member;
        }

        public IPythonFunction GetConstructors() => GetMember(null, "__init__") as IPythonFunction;

        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) {
            lock (_members) {
                return _members.Keys.ToArray();
            }
        }
    }
}
