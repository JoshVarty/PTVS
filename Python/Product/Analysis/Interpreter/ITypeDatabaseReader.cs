using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Common internal interface shared between SharedDatabaseState and PythonTypeDatabase.
    /// 
    /// This interface enables splitting of the type database into two portions.  The first is our cached
    /// type database for an interpreter, its standard library, and all of site-packages.  The second
    /// portion is per-project cached intellisense - currently only used for caching the intellisense
    /// against a referenced extension module (.pyd).
    /// 
    /// When 
    /// </summary>
    interface ITypeDatabaseReader {
        void ReadMember(string memberName, Dictionary<string, object> memberValue, Action<string, IMember> assign, IMemberContainer container);
        void LookupType(object type, Action<IPythonType> assign);
        string GetBuiltinTypeName(BuiltinTypeId id);
        void OnDatabaseCorrupt();

        bool BeginModuleLoad(IPythonModule module, int millisecondsTimeout);
        void EndModuleLoad(IPythonModule module);
    }
}
