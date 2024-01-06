using System.Diagnostics;
using System.Drawing;

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
                //UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WindowMessages.WM_SETREDRAW, 0, 0);
                PInvoke.SendMessage(this, PInvoke.WM_SETREDRAW, (WPARAM)(BOOL)false);
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
                    //UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WindowMessages.WM_SETREDRAW, -1, 0);
                    PInvoke.SendMessage(this, PInvoke.WM_SETREDRAW, (WPARAM)(BOOL)true);
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

        internal Graphics CreateGraphicsInternal()
            => Graphics.FromHwndInternal(Handle);

        // ControlPaint.cs

        private static unsafe HBRUSH CreateHalftoneHBRUSH()
        {
            short* grayPattern = stackalloc short[8];
            for (int i = 0; i < 8; i++)
            {
                grayPattern[i] = (short)(0x5555 << (i & 1));
            }

            using PInvoke.CreateBitmapScope hBitmap = new(8, 8, 1, 1, grayPattern);

            LOGBRUSH lb = new()
            {
                lbStyle = BRUSH_STYLE.BS_PATTERN,
                lbColor = default, // color is ignored since style is BS.PATTERN
                lbHatch = (nuint)(IntPtr)hBitmap
            };

            return PInvoke.CreateBrushIndirect(&lb);
        }
    }
}
