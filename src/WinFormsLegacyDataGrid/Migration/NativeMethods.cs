// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    internal static class NativeMethods
    {
        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        public const int BS_PATTERN = 3;
        public const int DCX_CACHE = 0x00000002, DCX_LOCKWINDOWUPDATE = 0x00000400;

        public static HandleRef HWND_NOTOPMOST = new HandleRef(null, new IntPtr(-2));
        public const int ICC_TAB_CLASSES = 0x00000008;
        public const int PATINVERT = 0x005A0049;

        public const int
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOACTIVATE = 0x0010;

        public const int TTS_ALWAYSTIP = 0x01;
        public const int WHEEL_DELTA = 120;

        public const string TOOLTIPS_CLASS = "tooltips_class32";

        [StructLayout(LayoutKind.Sequential)]
        public class INITCOMMONCONTROLSEX
        {
            public int dwSize = 8; //ndirect.DllLib.sizeOf(this);
            public int dwICC;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LOGBRUSH
        {
            public int lbStyle;
            public int lbColor;
            public IntPtr lbHatch;
        }

        // GetRegionData structures
        [StructLayout(LayoutKind.Sequential)]
        public struct RGNDATAHEADER
        {
            public int cbSizeOfStruct;
            public int iType;
            public int nCount;
            public int nRgnSize;
            // public Interop.RECT rcBound; // Note that we don't define this field as part of the marshaling
        }

        public static class ActiveX
        {
            public const int DISPID_BORDERSTYLE = unchecked((int)0xFFFFFE08);
        }

        public enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNINITIALIZED = -1,
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }
    }
}


