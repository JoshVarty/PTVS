using System.Collections.Generic;
using Microsoft.PythonTools.Django.Analysis;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    /// <summary>
    /// Provides context for returning the available variables/filters in a template file.
    /// 
    /// This is implemented as an interface so we can mock it out for the purposes of our tests
    /// and not need to do a fully analysis of the Django library.
    /// </summary>
    interface IDjangoCompletionContext {
        string[] Variables {
            get;
        }

        Dictionary<string, TagInfo> Filters {
            get;
        }

        DjangoUrl[] Urls
        {
            get;
        }

        Dictionary<string, PythonMemberType> GetMembers(string name);
    }
}
