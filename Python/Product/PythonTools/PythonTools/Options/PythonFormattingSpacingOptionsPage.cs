using System.Runtime.InteropServices;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Options {
    [ComVisible(true)]
    public class PythonFormattingSpacingOptionsPage : PythonFormattingOptionsPage {
        public PythonFormattingSpacingOptionsPage()
            : base( 
            new OptionCategory(Strings.FormattingOptionsCategoryClassDefinitions, OptionCategory.GetOptions(CodeFormattingCategory.Classes)),
            new OptionCategory(Strings.FormattingOptionsCategoryFunctionDefinitions, OptionCategory.GetOptions(CodeFormattingCategory.Functions)),
            new OptionCategory(Strings.FormattingOptionsCategoryOperators, OptionCategory.GetOptions(CodeFormattingCategory.Operators)),
            new OptionCategory(Strings.FormattingOptionsCategoryExpressionSpacing, OptionCategory.GetOptions(CodeFormattingCategory.Spacing))
            )
        
        {
        }
    }
}