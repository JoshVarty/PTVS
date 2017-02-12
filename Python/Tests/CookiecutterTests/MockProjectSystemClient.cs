using System;
using System.Collections.Generic;
using Microsoft.CookiecutterTools.Model;

namespace CookiecutterTests {
    class MockProjectSystemClient : IProjectSystemClient {
        public List<Tuple<ProjectLocation, CreateFilesOperationResult>> Added { get; } = new List<Tuple<ProjectLocation, CreateFilesOperationResult>>();
        public void AddToProject(ProjectLocation location, CreateFilesOperationResult creationResult) {
            Added.Add(Tuple.Create(location, creationResult));
        }

        public ProjectLocation GetSelectedFolderProjectLocation() {
            throw new NotImplementedException();
        }
    }
}
