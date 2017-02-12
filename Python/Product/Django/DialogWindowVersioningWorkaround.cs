using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.PlatformUI;

namespace Microsoft.PythonTools.Django {
    /// <summary>
    /// Works around an issue w/ DialogWindow and targetting multiple versions of VS.
    /// 
    /// Because the Microsoft.VisualStudio.Shell.version.0 assembly changes names
    /// we cannot refer to both v10 and v11 versions from within the same XAML file.
    /// Instead we use this subclass defined in our assembly.
    /// </summary>
    class DialogWindowVersioningWorkaround : DialogWindow {
    }
}
