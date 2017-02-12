using System.Runtime.InteropServices;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Options {
    [ComVisible(true)]
    public class PythonFormattingWrappingOptionsPage : PythonFormattingOptionsPage {
        public PythonFormattingWrappingOptionsPage()
            : base( 
            new OptionCategory(Strings.FormattingOptionsCategoryWrapping, OptionCategory.GetOptions(CodeFormattingCategory.Wrapping))
            ) {
        }
    }
}