using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Python;

namespace FastCgiTest {
    [TestClass]
    public class FastCgiTests3x : FastCgiTests {
        [ClassInitialize]
        public static new void DoDeployment(TestContext context) {
            AssertListener.Initialize();
            PythonTestData.Deploy();
        }

        public override PythonVersion PythonVersion {
            get {
                return PythonPaths.Python34 ?? PythonPaths.Python34_x64 ??
                    PythonPaths.Python33 ?? PythonPaths.Python33_x64;
            }
        }
    }
}
