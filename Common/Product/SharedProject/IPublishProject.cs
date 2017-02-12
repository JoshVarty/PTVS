using System.Collections.Generic;

namespace Microsoft.VisualStudioTools.Project {
    public interface IPublishProject {
        /// <summary>
        /// Gets the list of files which need to be published.
        /// </summary>
        IList<IPublishFile> Files {
            get;
        }

        /// <summary>
        /// Gets the root directory of the project.
        /// </summary>
        string ProjectDir {
            get;
        }

        /// <summary>
        /// Gets or sets the progress of the publishing.
        /// </summary>
        int Progress {
            get;
            set;
        }
    }
}
