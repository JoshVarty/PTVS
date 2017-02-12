using System;
using System.Windows;

namespace Microsoft.CookiecutterTools.View {
    class PointEventArgs : EventArgs {
        public Point Point { get; }

        public PointEventArgs(Point point) {
            Point = point;
        }
    }
}
