using System;
using System.Windows.Forms;

namespace Windows.Win32
{
    partial class PInvoke
    {
        public static unsafe BOOL ScrollWindow(IWin32Window hWnd, int XAmount, int YAmount, ref RECT rect, ref RECT clipRect)
        {
            BOOL result;
            fixed (RECT* lpRect = &rect)
            fixed (RECT* lpClipRect = &clipRect)
            {
                result = ScrollWindow((HWND)hWnd.Handle, XAmount, YAmount, lpRect, lpClipRect);
            }
            GC.KeepAlive(hWnd);
            return result;
        }
    }
}
