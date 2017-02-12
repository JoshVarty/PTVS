using System;
using System.ComponentModel.Composition;

namespace Microsoft.PythonTools.Interpreter {
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]

    public sealed class InterpreterFactoryIdAttribute : Attribute {
        private readonly string _id;

        public InterpreterFactoryIdAttribute(string interpreterFactoryId) {
            _id = interpreterFactoryId;
        }

        public string InterpreterFactoryId => _id;
    }

}
