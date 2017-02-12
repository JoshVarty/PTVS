using System.Runtime.InteropServices;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Options {
    [ComVisible(true)]
    public class PythonFormattingStatementsOptionsPage : PythonFormattingOptionsPage {
        public PythonFormattingStatementsOptionsPage()
            : base(
            new OptionCategory(Strings.FormattingOptionsCategoryStatements, OptionCategory.GetOptions(CodeFormattingCategory.Statements))
            ) {
        }
    }
}