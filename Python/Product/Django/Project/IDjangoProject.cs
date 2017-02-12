using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Django.Project {
    /// <summary>
    /// Enables getting a DjangoProject from an aggregated project node.
    /// </summary>
    [ComVisible(true)]
    [Guid("3EF13AFC-56E2-4215-BA9A-65D80FB51F75")]
    public interface IDjangoProject {
        ProjectSmuggler GetDjangoProject();
    }
}
