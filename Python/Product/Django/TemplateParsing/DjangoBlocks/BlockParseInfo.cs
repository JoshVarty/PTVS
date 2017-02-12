namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class BlockParseInfo {
        public readonly string Command;
        public readonly string Args;
        public readonly int Start;

        public BlockParseInfo(string command, string text, int start) {
            Command = command;
            Args = text;
            Start = start;
        }
    }
}
