using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.PythonTools.Django.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = DjangoPredefinedClassificationTypeNames.TemplateTag)]
    [Name(DjangoPredefinedClassificationTypeNames.TemplateTag)] // TODO: Localization - this string appears in fonts page in tools options
    [DisplayName(DjangoPredefinedClassificationTypeNames.TemplateTag)] // TODO: Localization - not sure if this is used, does not affect fonts page
    [UserVisible(true)]
    [Order(After = LanguagePriority.NaturalLanguage, Before = LanguagePriority.FormalLanguage)]
    internal sealed class OperatorFormat : ClassificationFormatDefinition {
        public OperatorFormat() {
            ForegroundColor = Color.FromRgb(0x00, 0x80, 0x80);
        }
    }
}
