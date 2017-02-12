namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Encapsulates all of the possible knobs which can be flipped when renaming a variable.
    /// </summary>
    class RenameVariableRequest {
        public readonly string Name;
        public readonly bool Preview, SearchInComments, SearchInStrings;

        public RenameVariableRequest(string name, bool preview, bool searchInComments, bool searchInStrings) {
            Name = name;
            Preview = preview;
            SearchInComments = searchInComments;
            SearchInStrings = searchInStrings;
        }
    }
}
