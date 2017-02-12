
namespace Microsoft.VisualStudioTools.Wpf {
    public interface ILambdaConverterProvider {
        LambdaConverter GetConverterForLambda(string lambda);
    }
}
