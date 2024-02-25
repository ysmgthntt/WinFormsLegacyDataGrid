// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
//using static Interop;

namespace System.Windows.Forms
{
    // this class is basically a NativeWindow that does toolTipping
    // should be one for the entire grid
    internal sealed class DataGridToolTip /*: MarshalByRefObject*/
    {
        // the toolTip control
        private NativeWindow? tipWindow;

        // the dataGrid which contains this toolTip
        private readonly DataGrid dataGrid;

        // CONSTRUCTOR
        public DataGridToolTip(DataGrid dataGrid)
        {
            Debug.Assert(dataGrid is not null, "can't attach a tool tip to a null grid");
            this.dataGrid = dataGrid;
        }

        // will ensure that the toolTip window was created
        public void CreateToolTipHandle()
        {
            if (tipWindow is null || tipWindow.Handle == IntPtr.Zero)
            {
                /*
                NativeMethods.INITCOMMONCONTROLSEX icc = new NativeMethods.INITCOMMONCONTROLSEX
                {
                    dwICC = NativeMethods.ICC_TAB_CLASSES
                };
                icc.dwSize = Marshal.SizeOf(icc);
                SafeNativeMethods.InitCommonControlsEx(icc);
                */
                unsafe
                {
                    PInvoke.InitCommonControlsEx(new INITCOMMONCONTROLSEX
                    {
                        dwSize = (uint)sizeof(INITCOMMONCONTROLSEX),
                        dwICC = INITCOMMONCONTROLSEX_ICC.ICC_TAB_CLASSES
                    });
                }
                CreateParams cparams = new CreateParams
                {
                    Parent = dataGrid.Handle,
                    ClassName = /*NativeMethods*/PInvoke.TOOLTIPS_CLASS,
                    Style = /*NativeMethods*/(int)PInvoke.TTS_ALWAYSTIP
                };
                tipWindow = new NativeWindow();
                tipWindow.CreateHandle(cparams);

                //User32.SendMessageW(tipWindow, WindowMessages.TTM_SETMAXTIPWIDTH, IntPtr.Zero, (IntPtr)SystemInformation.MaxWindowTrackSize.Width);
                PInvoke.SendMessage(tipWindow, PInvoke.TTM_SETMAXTIPWIDTH, 0, SystemInformation.MaxWindowTrackSize.Width);
                //SafeNativeMethods.SetWindowPos(new HandleRef(tipWindow, tipWindow.Handle), NativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOACTIVATE);
                PInvoke.SetWindowPos(tipWindow, HWND.HWND_NOTOPMOST, 0, 0, 0, 0, SET_WINDOW_POS_FLAGS.SWP_NOSIZE | SET_WINDOW_POS_FLAGS.SWP_NOMOVE | SET_WINDOW_POS_FLAGS.SWP_NOACTIVATE);
                //User32.SendMessageW(tipWindow, WindowMessages.TTM_SETDELAYTIME, (IntPtr)ComCtl32.TTDT.INITIAL, (IntPtr)0);
                PInvoke.SendMessage(tipWindow, PInvoke.TTM_SETDELAYTIME, PInvoke.TTDT_INITIAL, 0);
            }
        }

        public void AddToolTip(string toolTipString, IntPtr toolTipId, Rectangle iconBounds)
        {
            Debug.Assert(tipWindow is not null && tipWindow.Handle != IntPtr.Zero, "the tipWindow was not initialized, bailing out");

            ArgumentNullException.ThrowIfNull(toolTipString);

            if (iconBounds.IsEmpty)
                throw new ArgumentNullException(nameof(iconBounds), SR.DataGridToolTipEmptyIcon);

            //var info = new ComCtl32.ToolInfoWrapper(dataGrid, toolTipId, ComCtl32.TTF.SUBCLASS, toolTipString, iconBounds);
            var info = new ToolInfoWrapper<Control>(dataGrid, toolTipId, TOOLTIP_FLAGS.TTF_SUBCLASS, toolTipString, iconBounds);
            info.SendMessage(tipWindow, /*WindowMessages*/PInvoke.TTM_ADDTOOLW);
        }

        public void RemoveToolTip(IntPtr toolTipId)
        {
            //var info = new ComCtl32.ToolInfoWrapper(dataGrid, toolTipId);
            var info = new ToolInfoWrapper<Control>(dataGrid, toolTipId);
            info.SendMessage(tipWindow!, /*WindowMessages*/PInvoke.TTM_DELTOOLW);
        }

        // will destroy the tipWindow
        public void Destroy()
        {
            Debug.Assert(tipWindow is not null, "how can one destroy a null window");
            tipWindow.DestroyHandle();
            tipWindow = null;
        }
    }
}
