using System;
using Microsoft.VisualStudio.Imaging.Interop;

namespace Microsoft.CookiecutterTools.Resources {
    static class ImageMonikers {
        public static readonly Guid Guid = new Guid("{50EDF200-5C99-4968-ABC0-CF1A2C490F00}");
        public static readonly ImageMoniker Cancel = new ImageMoniker { Guid = Guid, Id = 1 };
        public static readonly ImageMoniker Cookiecutter = new ImageMoniker { Guid = Guid, Id = 2 };
        public static readonly ImageMoniker CookiecutterTemplate = new ImageMoniker { Guid = Guid, Id = 3 };
        public static readonly ImageMoniker CookiecutterTemplateOK = new ImageMoniker { Guid = Guid, Id = 4 };
        public static readonly ImageMoniker CookiecutterTemplateUpdate = new ImageMoniker { Guid = Guid, Id = 5 };
        public static readonly ImageMoniker CookiecutterTemplateWarning = new ImageMoniker { Guid = Guid, Id = 6 };
        public static readonly ImageMoniker Download = new ImageMoniker { Guid = Guid, Id = 7 };
        public static readonly ImageMoniker NewCookiecutter = new ImageMoniker { Guid = Guid, Id = 8 };
    }
}
