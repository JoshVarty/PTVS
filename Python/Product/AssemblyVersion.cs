using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// If you get compiler errors CS0579, "Duplicate '<attributename>' attribute", check your 
// Properties\AssemblyInfo.cs file and remove any lines duplicating the ones below.
// (See also AssemblyInfoCommon.cs in this same directory.)

#if !SUPPRESS_COMMON_ASSEMBLY_VERSION
[assembly: AssemblyVersion(AssemblyVersionInfo.StableVersion)]
#endif
[assembly: AssemblyFileVersion(AssemblyVersionInfo.Version)]

class AssemblyVersionInfo {
#if DEV14
    public const string VSMajorVersion = "14";
    public const string VSName = "2015";
#elif DEV15
    public const string VSMajorVersion = "15";
    public const string VSName = "2017";
#else
#error Unrecognized VS Version.
#endif

    public const string VSVersion = VSMajorVersion + ".0";

    // These version strings are automatically updated at build.
    public const string StableVersionPrefix = "1.0.0";
    public const string StableVersion = "1.0.0.0";
    public const string Version = "1.0.0.0";
}
