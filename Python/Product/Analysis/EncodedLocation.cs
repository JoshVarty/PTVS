using System;

namespace Microsoft.PythonTools.Analysis {
    /// <summary>
    /// Simple structure used to track positions coming from multiple formats w/o having
    /// to resolve the location until much later.  We store a location resolver which can
    /// turn a location object back into a a line and column number.  Usually the resolver
    /// will be a PythonAst instance and the Location will be some Node.  The PythonAst
    /// then provides the location and we don't have to turn an index into line/column
    /// during the analysis.
    /// 
    /// But there's also the XAML analysis which doesn't have a PythonAst and Node, instead
    /// it just has line/column info.  So it uses a singleton instance and boxes the Location
    /// as a SourceLocation.  Because it's the uncommon case the extra overhead there isn't
    /// as important.
    /// 
    /// This ultimately lets us track the line/column info in the same space as just
    /// storing the line/column info directly while still allowing multiple schemes
    /// to be used.
    /// </summary>
    struct EncodedLocation : IEquatable<EncodedLocation> {
        public readonly ILocationResolver Resolver;
        public readonly object Location;

        public EncodedLocation(ILocationResolver resolver, object location) {
            Resolver = resolver;
            Location = location;
        }

        public override int GetHashCode() {
            if (Location != null) {
                return Resolver.GetHashCode() ^ Location.GetHashCode();
            }

            return Resolver.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj is EncodedLocation) {
                return Equals((EncodedLocation)obj);
            }
            return false;
        }

        #region IEquatable<EncodedLocation> Members

        public bool Equals(EncodedLocation other) {
            return Resolver == other.Resolver &&
                Location == other.Location;
        }

        #endregion

        public LocationInfo GetLocationInfo() {
            return Resolver.ResolveLocation(Location);
        }
    }
}
