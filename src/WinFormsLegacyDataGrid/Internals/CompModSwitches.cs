// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//using System.Diagnostics;

namespace System.ComponentModel
{
    internal static class CompModSwitches
    {
        public static TraceSwitch DataGridCursor
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridEditing
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridKeys
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridLayout
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridPainting
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridParents
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridScrolling
            => TraceSwitch.Instance;

        public static TraceSwitch DataGridSelection
            => TraceSwitch.Instance;

        public static TraceSwitch DGCaptionPaint
            => TraceSwitch.Instance;

        public static TraceSwitch DGRelationShpRowLayout
            => TraceSwitch.Instance;

        public static TraceSwitch DGRelationShpRowPaint
            => TraceSwitch.Instance;

        public static TraceSwitch DGRowPaint
            => TraceSwitch.Instance;

        internal sealed class TraceSwitch
        {
            public static readonly TraceSwitch Instance = new TraceSwitch();

            private TraceSwitch() { }

            public bool TraceVerbose => false;
        }
    }
}
