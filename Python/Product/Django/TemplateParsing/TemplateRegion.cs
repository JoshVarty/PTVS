namespace Microsoft.PythonTools.Django.TemplateParsing {
    internal class TemplateRegion {
        public readonly string Text;
        public readonly TemplateTokenKind Kind;
        public readonly DjangoBlock Block;
        public readonly int Start;

        public TemplateRegion(string text, TemplateTokenKind kind, DjangoBlock block, int start) {
            Text = text;
            Kind = kind;
            Start = start;
            Block = block;
        }
    }
}
