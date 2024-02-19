// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINFORMS_NAMESPACE
namespace System.Windows.Forms
#else
namespace WinFormsLegacyControls
#endif
{
    /// <summary>
    ///  Specifies the style of gridlines in a <see cref='DataGrid'/>.
    /// </summary>
    public enum DataGridLineStyle
    {
        /// <summary>
        ///  No gridlines between cells.
        /// </summary>
        None,

        /// <summary>
        ///  Solid gridlines between cells.
        /// </summary>
        Solid
    }
}
