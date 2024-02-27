// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace System.ComponentModel
{
    internal static class CompModSwitches
    {
        private static TraceSwitch? dataGridCursor;
        private static TraceSwitch? dataGridEditing;
        private static TraceSwitch? dataGridKeys;
        private static TraceSwitch? dataGridLayout;
        private static TraceSwitch? dataGridPainting;
        private static TraceSwitch? dataGridParents;
        private static TraceSwitch? dataGridScrolling;
        private static TraceSwitch? dataGridSelection;
        private static TraceSwitch? dgCaptionPaint;
        //private static TraceSwitch? dgEditColumnEditing;
        private static TraceSwitch? dgRelationShpRowLayout;
        private static TraceSwitch? dgRelationShpRowPaint;
        private static TraceSwitch? dgRowPaint;

        public static TraceSwitch DataGridCursor => dataGridCursor ??= new TraceSwitch("DataGridCursor", "DataGrid cursor tracing");

        public static TraceSwitch DataGridEditing => dataGridEditing ??= new TraceSwitch("DataGridEditing", "DataGrid edit related tracing");

        public static TraceSwitch DataGridKeys => dataGridKeys ??= new TraceSwitch("DataGridKeys", "DataGrid keystroke management tracing");

        public static TraceSwitch DataGridLayout => dataGridLayout ??= new TraceSwitch("DataGridLayout", "DataGrid layout tracing");

        public static TraceSwitch DataGridPainting => dataGridPainting ??= new TraceSwitch("DataGridPainting", "DataGrid Painting related tracing");

        public static TraceSwitch DataGridParents => dataGridParents ??= new TraceSwitch("DataGridParents", "DataGrid parent rows");

        public static TraceSwitch DataGridScrolling => dataGridScrolling ??= new TraceSwitch("DataGridScrolling", "DataGrid scrolling");

        public static TraceSwitch DataGridSelection => dataGridSelection ??= new TraceSwitch("DataGridSelection", "DataGrid selection management tracing");

        public static TraceSwitch DGCaptionPaint => dgCaptionPaint ??= new TraceSwitch("DGCaptionPaint", "DataGridCaption");

        //public static TraceSwitch DGEditColumnEditing => dgEditColumnEditing ??= new TraceSwitch("DGEditColumnEditing", "Editing related tracing");

        public static TraceSwitch DGRelationShpRowLayout => dgRelationShpRowLayout ??= new TraceSwitch("DGRelationShpRowLayout", "Relationship row layout");

        public static TraceSwitch DGRelationShpRowPaint => dgRelationShpRowPaint ??= new TraceSwitch("DGRelationShpRowPaint", "Relationship row painting");

        public static TraceSwitch DGRowPaint => dgRowPaint ??= new TraceSwitch("DGRowPaint", "DataGrid Simple Row painting stuff");
    }
}
