using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Interpreter.Ast {
    class AstPythonConstant : IPythonConstant, ILocatedMember {
        public AstPythonConstant(IPythonType type, params LocationInfo[] locations) {
            Type = type;
            Locations = locations.ToArray();
        }

        public IEnumerable<LocationInfo> Locations { get; }

        public PythonMemberType MemberType => PythonMemberType.Constant;
        public IPythonType Type { get; }
    }
}
