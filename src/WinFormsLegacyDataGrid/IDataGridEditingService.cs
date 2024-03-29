﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINFORMS_NAMESPACE
namespace System.Windows.Forms
#else
namespace WinFormsLegacyControls
#endif
{
    /// <summary>
    ///  The DataGrid exposes hooks to request editing commands via this interface.
    /// </summary>
    public interface IDataGridEditingService
    {
        bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber);
        bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort);
    }
}
