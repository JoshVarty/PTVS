using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUtilities.SharedProject;

namespace TestUtilities.Python {
    public class PythonProjectTest : SharedProjectTest {
        public static ProjectType PythonProject = ProjectTypes.First(x => x.ProjectExtension == ".pyproj");

        public static ProjectDefinition Project(string name, params ProjectContentGenerator[] items) {
            return new ProjectDefinition(name, PythonProject, items);
        }
    }
}
