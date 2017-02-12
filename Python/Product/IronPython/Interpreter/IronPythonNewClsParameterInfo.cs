using System;
using System.Collections.Generic;
using System.Reflection;
using IronPython.Runtime;
using IronPython.Runtime.Operations;
using Microsoft.PythonTools.Interpreter;
using Microsoft.Scripting;
using Microsoft.Scripting.Generation;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonNewClsParameterInfo : IParameterInfo {
        private readonly IronPythonType _declaringType;

        public IronPythonNewClsParameterInfo(IronPythonType declaringType) {
            _declaringType = declaringType;
        }

        #region IParameterInfo Members

        public IList<IPythonType> ParameterTypes {
            get {
                return new[] { _declaringType };
            }
        }

        public string Documentation {
            get { return ""; }
        }

        public string Name {
            get {
                return "cls";
            }
        }

        public bool IsParamArray {
            get {
                return false;
            }
        }

        public bool IsKeywordDict {
            get {
                return false;
            }
        }

        public string DefaultValue {
            get {
                return null;
            }
        }

        #endregion
    }
}
