using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.VisualStudioTools.Project {
    /// <summary>
    /// Defines abstract package.
    /// </summary>
    [ComVisible(true)]

    public abstract class ProjectPackage : Microsoft.VisualStudio.Shell.Package {
        #region fields
        /// <summary>
        /// This is the place to register all the solution listeners.
        /// </summary>
        private List<SolutionListener> solutionListeners = new List<SolutionListener>();
        #endregion

        #region properties
        /// <summary>
        /// Add your listener to this list. They should be added in the overridden Initialize befaore calling the base.
        /// </summary>
        internal IList<SolutionListener> SolutionListeners {
            get {
                return this.solutionListeners;
            }
        }
        #endregion

        #region methods
        protected override void Initialize() {
            base.Initialize();

            // Subscribe to the solution events
            this.solutionListeners.Add(new SolutionListenerForProjectOpen(this));
            this.solutionListeners.Add(new SolutionListenerForBuildDependencyUpdate(this));

            foreach (SolutionListener solutionListener in this.solutionListeners) {
                solutionListener.Init();
            }
        }

        protected override void Dispose(bool disposing) {
            // Unadvise solution listeners.
            try {
                if (disposing) {
                    foreach (SolutionListener solutionListener in this.solutionListeners) {
                        solutionListener.Dispose();
                    }
                }
            } finally {

                base.Dispose(disposing);
            }
        }
        #endregion
    }
}
