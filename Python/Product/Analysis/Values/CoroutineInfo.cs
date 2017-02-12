using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    /// <summary>
    /// Represents a coroutine instance
    /// </summary>
    internal class CoroutineInfo : BuiltinInstanceInfo, IHasRichDescription {
        private readonly IPythonProjectEntry _declaringModule;
        private readonly int _declaringVersion;
        public readonly VariableDef Returns;

        public CoroutineInfo(PythonAnalyzer projectState, IPythonProjectEntry entry)
            : base(projectState.ClassInfos[BuiltinTypeId.Generator]) {
            // Internally, coroutines are represented by generators with a CO_*
            // flag on the code object. Here we represent it as a separate info,
            // but reuse the underlying class info.

            _declaringModule = entry;
            _declaringVersion = entry.AnalysisVersion;
            Returns = new VariableDef();
        }

        public override IPythonProjectEntry DeclaringModule { get { return _declaringModule; } }
        public override int DeclaringVersion { get { return _declaringVersion; } }

        public IEnumerable<KeyValuePair<string, string>> GetRichDescription() {
            yield return new KeyValuePair<string, string>(WellKnownRichDescriptionKinds.Misc, "coroutine");
            foreach (var kv in FunctionInfo.GetReturnTypeString(Returns.TypesNoCopy.AsUnion)) {
                yield return kv;
            }
        }

        public override string Description {
            get {
                // Generator lies about its name when it represents a coroutine
                return string.Join("", GetRichDescription().Select(kv => kv.Value));
            }
        }

        public override IAnalysisSet Await(Node node, AnalysisUnit unit) {
            Returns.AddDependency(unit);
            return Returns.GetTypesNoCopy(unit, DeclaringModule);
        }

        public void AddReturn(Node node, AnalysisUnit unit, IAnalysisSet returnValue, bool enqueue = true) {
            Returns.MakeUnionStrongerIfMoreThan(ProjectState.Limits.ReturnTypes, returnValue);
            Returns.AddTypes(unit, returnValue, enqueue, DeclaringModule);
        }
    }
}
