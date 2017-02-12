namespace TestUtilities.SharedProject {
    /// <summary>
    /// Interface for getting metadata for when we import our IProjectProcessor
    /// class.  MEF requires this to be public.
    /// </summary>
    public interface IProjectProcessorMetadata {
        string ProjectExtension { get; }
    }
}
