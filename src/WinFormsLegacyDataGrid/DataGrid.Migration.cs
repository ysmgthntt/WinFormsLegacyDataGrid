using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using static Interop;

namespace System.Windows.Forms
{
    partial class DataGrid
    {
        // Control.cs

        private short _updateCount;

        private void BeginUpdateInternal()
        {
            if (!IsHandleCreated)
            {
                return;
            }

            if (_updateCount == 0)
            {
                UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WindowMessages.WM_SETREDRAW, 0, 0);
                //PInvoke.SendMessage(this, PInvoke.WM_SETREDRAW, (WPARAM)(BOOL)false);
            }

            _updateCount++;
        }

        private bool EndUpdateInternal()
        {
            return EndUpdateInternal(true);
        }

        private bool EndUpdateInternal(bool invalidate)
        {
            if (_updateCount > 0)
            {
                Debug.Assert(IsHandleCreated, "Handle should be created by now");
                _updateCount--;
                if (_updateCount == 0)
                {
                    UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WindowMessages.WM_SETREDRAW, -1, 0);
                    //PInvoke.SendMessage(this, PInvoke.WM_SETREDRAW, (WPARAM)(BOOL)true);
                    if (invalidate)
                    {
                        Invalidate();
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // ControlPaint.cs

        private static IntPtr CreateHalftoneHBRUSH()
        {
            short[] grayPattern = new short[8];
            for (int i = 0; i < 8; i++)
            {
                grayPattern[i] = (short)(0x5555 << (i & 1));
            }

            IntPtr hBitmap = SafeNativeMethods.CreateBitmap(8, 8, 1, 1, grayPattern);

            NativeMethods.LOGBRUSH lb = new NativeMethods.LOGBRUSH
            {
                lbColor = ColorTranslator.ToWin32(Color.Black),
                lbStyle = NativeMethods.BS_PATTERN,
                lbHatch = hBitmap
            };
            IntPtr brush = SafeNativeMethods.CreateBrushIndirect(ref lb);

            Gdi32.DeleteObject(hBitmap);
            return brush;
        }
    }
}
