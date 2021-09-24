using System;
using System.Runtime.InteropServices;

namespace DoAnFramework
{
    public static class OSPlatformHelpers
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
