namespace Microsoft.PythonTools.Options {
    public interface IPythonToolsOptionsService {
        void SaveString(string name, string category, string value);
        string LoadString(string name, string category);
        void DeleteCategory(string category);
    }
}
