using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CookiecutterTools;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.CookiecutterTools.Model;

namespace CookiecutterTests {
    class MockCookiecutterClient : ICookiecutterClient {
        public bool CookiecutterInstalled {
            get {
                throw new NotImplementedException();
            }
        }

        public Task CreateCookiecutterEnv() {
            throw new NotImplementedException();
        }

        public Task<CreateFilesOperationResult> CreateFilesAsync(string localTemplateFolder, string userConfigFilePath, string contextFilePath, string outputFolderPath) {
            throw new NotImplementedException();
        }

        public Task<string> GetDefaultOutputFolderAsync(string shortName) {
            throw new NotImplementedException();
        }

        public Task InstallPackage() {
            throw new NotImplementedException();
        }

        public Task<bool> IsCookiecutterInstalled() {
            throw new NotImplementedException();
        }

        public Task<TemplateContext> LoadUnrenderedContextAsync(string localTemplateFolder, string userConfigFilePath) {
            throw new NotImplementedException();
        }

        public Task<TemplateContext> LoadRenderedContextAsync(string localTemplateFolder, string userConfigFilePath, string contextFilePath, string outputFolderPath) {
            throw new NotImplementedException();
        }
    }
}
