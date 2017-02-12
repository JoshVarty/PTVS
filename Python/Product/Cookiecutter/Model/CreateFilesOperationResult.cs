namespace Microsoft.CookiecutterTools.Model {
    class ReplacedFile {
        public string OriginalFilePath { get; }
        public string BackupFilePath { get; }
        public ReplacedFile(string original, string backup) {
            OriginalFilePath = original;
            BackupFilePath = backup;
        }
    }

    class CreateFilesOperationResult {
        public string[] FoldersCreated { get; }
        public string[] FilesCreated { get; }
        public ReplacedFile[] FilesReplaced { get; }
        public CreateFilesOperationResult(string[] foldersCreated, string[] filesCreated, ReplacedFile[] filesReplaced) {
            FoldersCreated = foldersCreated;
            FilesCreated = filesCreated;
            FilesReplaced = filesReplaced;
        }
    }
}
