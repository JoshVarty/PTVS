namespace Microsoft.PythonTools.EnvironmentsList {
    public interface IEnvironmentViewExtensionProvider {
        IEnvironmentViewExtension CreateExtension(EnvironmentView view);
    }
}
