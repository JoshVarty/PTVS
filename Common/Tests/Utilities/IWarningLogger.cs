using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace TestUtilities
{
    public interface IWarningLogger
    {
        void Log(string message, params object[] arguments);
    }
}
