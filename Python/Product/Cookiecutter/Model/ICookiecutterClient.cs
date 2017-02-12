using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    interface ICookiecutterClient {
        bool CookiecutterInstalled { get; }
        Task<bool> IsCookiecutterInstalled();
        Task CreateCookiecutterEnv();
        Task InstallPackage();
        Task<TemplateContext> LoadUnrenderedContextAsync(string localTemplateFolder, string userConfigFilePath);
        Task<TemplateContext> LoadRenderedContextAsync(string localTemplateFolder, string userConfigFilePath, string contextFilePath, string outputFolderPath);
        Task<CreateFilesOperationResult> CreateFilesAsync(string localTemplateFolder, string userConfigFilePath, string contextFilePath, string outputFolderPath);
        Task<string> GetDefaultOutputFolderAsync(string shortName);
    }
}
