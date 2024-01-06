using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Windows.Win32
{
    partial class PInvoke
    {
        public static LRESULT SendMessage(
            IWin32Window hWnd,
            uint Msg,
            WPARAM wParam = default,
            LPARAM lParam = default)
        {
            LRESULT result = SendMessage((HWND)hWnd.Handle, Msg, wParam, lParam);
            GC.KeepAlive(hWnd);
            return result;
        }
    }
}
