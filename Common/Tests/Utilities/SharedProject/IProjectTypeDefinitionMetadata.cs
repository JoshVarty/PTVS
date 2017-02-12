using System.ComponentModel;

namespace TestUtilities.SharedProject {
    /// <summary>
    /// Metadata interface for getting information about declared project kinds.
    /// MEF requires that this be public.
    /// </summary>
    public interface IProjectTypeDefinitionMetadata {
        string ProjectExtension { get; }
        string ProjectTypeGuid { get; }
        string CodeExtension { get; }

        [DefaultValue("")]
        string SampleCode { get; }
    }
}
