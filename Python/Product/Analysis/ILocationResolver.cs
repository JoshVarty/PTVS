
namespace Microsoft.PythonTools.Analysis {
    /// <summary>
    /// Resolves a location object into the LocationInfo which we expose to the consumer
    /// of the analysis APIs.  This enables an efficient mechanism to track references
    /// during analysis which doesn't involve actually tracking all of the line number
    /// information directly.  Instead we can support different resolvers and location
    /// objects and only lazily turn them back into real line information.
    /// 
    /// See EncodedLocation for more information.
    /// </summary>
    interface ILocationResolver {
        LocationInfo ResolveLocation(object location);
    }
}
