﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace System.Windows.Forms
{
    /// <summary>
    ///  Represents a caption in the DataGrid control.
    /// </summary>
    internal sealed class DataGridCaption
    {
        private EventHandlerList? events;

        private const int xOffset = 3;
        private const int yOffset = 1;
        private const int textPadding = 2;
        private const int buttonToText = 4;
        private static readonly ColorMap[] colorMap = new ColorMap[] { new ColorMap() };

        // private static readonly Point minimumBounds = new Point(50, 30);

        private readonly DataGrid dataGrid;
        private bool backButtonVisible;
        private bool downButtonVisible;

        private SolidBrush backBrush = DefaultBackBrush;
        private SolidBrush foreBrush = DefaultForeBrush;
        private readonly Pen textBorderPen = DefaultTextBorderPen;

        private string text = string.Empty;
        private bool textBorderVisible;
        private Font? textFont;

        // use the datagridFont when the textFont is not set
        // we cache this font ( cause we have to make it bold every time we paint the caption )
        //
        private Font dataGridFont = null!;

        private bool backActive;
        private bool downActive;
        private bool backPressed;
        private bool downPressed;

        // if the downButton should point down or not
        private bool downButtonDown;

        private static Bitmap? leftButtonBitmap;
        private static Bitmap? leftButtonBitmap_bidi;
        private static Bitmap? magnifyingGlassBitmap;

        private Rectangle backButtonRect;
        private Rectangle downButtonRect;
        private Rectangle textRect;

        private CaptionLocation lastMouseLocation = CaptionLocation.Nowhere;

        //private EventEntry? eventList;
        private static readonly object EVENT_BACKWARDCLICKED = new object();
        private static readonly object EVENT_DOWNCLICKED = new object();
        // private static readonly object EVENT_CAPTIONCLICKED = new object();

        internal DataGridCaption(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            downButtonVisible = dataGrid.ParentRowsVisible;
            colorMap[0].OldColor = Color.White;
            colorMap[0].NewColor = ForeColor;
            OnGridFontChanged();
        }

        internal void OnGridFontChanged()
        {
            if (dataGridFont is null || !dataGridFont.Equals(dataGrid.Font))
            {
                try
                {
                    dataGridFont = new Font(dataGrid.Font, FontStyle.Bold);
                }
                catch
                {
                }
            }
        }

        // =------------------------------------------------------------------
        // =        Properties
        // =------------------------------------------------------------------

        internal bool BackButtonActive
        {
            get => backActive;
            set
            {
                if (backActive != value)
                {
                    backActive = value;
                    InvalidateCaptionRect(backButtonRect);
                }
            }
        }

        internal bool DownButtonActive
        {
            get => downActive;
            set
            {
                if (downActive != value)
                {
                    downActive = value;
                    InvalidateCaptionRect(downButtonRect);
                }
            }
        }

        private static SolidBrush DefaultBackBrush => (SolidBrush)SystemBrushes.ActiveCaption;

        private static Pen DefaultTextBorderPen => new Pen(SystemColors.ActiveCaptionText);

        private static SolidBrush DefaultForeBrush => (SolidBrush)SystemBrushes.ActiveCaptionText;

        internal Color BackColor
        {
            get => backBrush.Color;
            set
            {
                if (!backBrush.Color.Equals(value))
                {
                    if (value.IsEmpty)
                    {
                        throw new ArgumentException(string.Format(SR.DataGridEmptyColor, "Caption BackColor"));
                    }

                    backBrush = new SolidBrush(value);
                    Invalidate();
                }
            }
        }

        private EventHandlerList Events => events ??= new EventHandlerList();

        internal Font Font
        {
            // use the dataGridFont only if the user
            // did not set the CaptionFont
            //
            get => textFont ?? dataGridFont;
            set
            {
                if (textFont is null || !textFont.Equals(value))
                {
                    textFont = value;
                    // this property gets called in the constructor before dataGrid has a caption
                    // and we don't need this special-handling then...
                    if (dataGrid.Caption is not null)
                    {
                        dataGrid.RecalculateFonts();
                        dataGrid.PerformLayout();
                        dataGrid.Invalidate(); // smaller invalidate rect?
                    }
                }
            }
        }

        internal bool ShouldSerializeFont()
        {
            return textFont is not null && !textFont.Equals(dataGridFont);
        }

        internal bool ShouldSerializeBackColor()
        {
            return !backBrush.Equals(DefaultBackBrush);
        }

        internal void ResetBackColor()
        {
            if (ShouldSerializeBackColor())
            {
                backBrush = DefaultBackBrush;
                Invalidate();
            }
        }

        internal void ResetForeColor()
        {
            if (ShouldSerializeForeColor())
            {
                foreBrush = DefaultForeBrush;
                Invalidate();
            }
        }

        internal bool ShouldSerializeForeColor()
        {
            return !foreBrush.Equals(DefaultForeBrush);
        }

        internal void ResetFont()
        {
            textFont = null;
            Invalidate();
        }

        [AllowNull]
        internal string Text
        {
            get => text;
            set
            {
                text = value ?? string.Empty;

                Invalidate();
            }
        }

        /*
        internal bool TextBorderVisible
        {
            get
            {
                return textBorderVisible;
            }
            set
            {
                textBorderVisible = value;
                Invalidate();
            }
        }
        */

        internal Color ForeColor
        {
            get => foreBrush.Color;
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException(string.Format(SR.DataGridEmptyColor, "Caption ForeColor"));
                }

                foreBrush = new SolidBrush(value);
                colorMap[0].NewColor = ForeColor;
                Invalidate();
            }
        }

        /*
        internal Point MinimumBounds
        {
            get
            {
                return minimumBounds;
            }
        }
        */

        internal bool BackButtonVisible
        {
            get => backButtonVisible;
            set
            {
                if (backButtonVisible != value)
                {
                    backButtonVisible = value;
                    Invalidate();
                }
            }
        }

        /*
        internal bool DownButtonVisible
        {
            get
            {
                return downButtonVisible;
            }
            set
            {
                if (downButtonVisible != value)
                {
                    downButtonVisible = value;
                    Invalidate();
                }
            }
        }
        */

        // =------------------------------------------------------------------
        // =        Methods
        // =------------------------------------------------------------------

        /*
        protected virtual void AddEventHandler(object key, Delegate handler)
        {
            // Locking 'this' here is ok since this is an internal class.
            lock (this)
            {
                if (handler is null)
                {
                    return;
                }

                for (EventEntry? e = eventList; e is not null; e = e.next)
                {
                    if (e.key == key)
                    {
                        e.handler = Delegate.Combine(e.handler, handler);
                        return;
                    }
                }
                eventList = new EventEntry(eventList, key, handler);
            }
        }
        */

        /// <summary>
        ///  Adds a listener for the BackwardClicked event.
        /// </summary>
        internal event EventHandler BackwardClicked
        {
            add => Events.AddHandler(EVENT_BACKWARDCLICKED, value);
            remove => Events.RemoveHandler(EVENT_BACKWARDCLICKED, value);
        }

        /*
        /// <summary>
        ///  Adds a listener for the CaptionClicked event.
        /// </summary>
        internal event EventHandler CaptionClicked
        {
            add => Events.AddHandler(EVENT_CAPTIONCLICKED, value);
            remove => Events.RemoveHandler(EVENT_CAPTIONCLICKED, value);
        }
        */

        internal event EventHandler DownClicked
        {
            add => Events.AddHandler(EVENT_DOWNCLICKED, value);
            remove => Events.RemoveHandler(EVENT_DOWNCLICKED, value);
        }

        private void Invalidate()
        {
            if (dataGrid is not null)
            {
                dataGrid.InvalidateCaption();
            }
        }

        private void InvalidateCaptionRect(Rectangle r)
        {
            if (dataGrid is not null)
            {
                dataGrid.InvalidateCaptionRect(r);
            }
        }

        private void InvalidateLocation(CaptionLocation loc)
        {
            Rectangle r;
            switch (loc)
            {
                case CaptionLocation.BackButton:
                    r = backButtonRect;
                    r.Inflate(1, 1);
                    InvalidateCaptionRect(r);
                    break;
                case CaptionLocation.DownButton:
                    r = downButtonRect;
                    r.Inflate(1, 1);
                    InvalidateCaptionRect(r);
                    break;
            }
        }

        protected void OnBackwardClicked(EventArgs e)
        {
            if (backActive)
            {
                ((EventHandler?)Events[EVENT_BACKWARDCLICKED])?.Invoke(this, e);
            }
        }

        protected void OnCaptionClicked(EventArgs e)
        {
            //((EventHandler?)Events[EVENT_CAPTIONCLICKED])?.Invoke(this, e);
        }

        protected void OnDownClicked(EventArgs e)
        {
            if (downActive && downButtonVisible)
            {
                ((EventHandler?)Events[EVENT_DOWNCLICKED])?.Invoke(this, e);
            }
        }

        private static Bitmap GetBitmap(string bitmapName)
        {
            try
            {
                return DpiHelper.GetBitmapFromIcon(typeof(DataGridCaption), bitmapName);
            }
            catch (Exception e)
            {
                Debug.Fail("Failed to load bitmap: " + bitmapName, e.ToString());
                return null;
            }
        }

        private static Bitmap GetBackButtonBmp(bool alignRight)
        {
            if (alignRight)
            {
                return leftButtonBitmap_bidi ??= GetBitmap("DataGridCaption.backarrow_bidi");
            }
            else
            {
                return leftButtonBitmap ??= GetBitmap("DataGridCaption.backarrow");
            }
        }

        private static Bitmap GetDetailsBmp()
        {
            return magnifyingGlassBitmap ??= GetBitmap("DataGridCaption.Details");
        }

        /*
        protected virtual Delegate? GetEventHandler(object key)
        {
            // Locking 'this' here is ok since this is an internal class.
            lock (this)
            {
                for (EventEntry? e = eventList; e is not null; e = e.next)
                {
                    if (e.key == key)
                    {
                        return e.handler;
                    }
                }
                return null;
            }
        }
        */

        internal static Rectangle GetBackButtonRect(Rectangle bounds, bool alignRight, int downButtonWidth)
        {
            Bitmap backButtonBmp = GetBackButtonBmp(false);
            Size backButtonSize;
            lock (backButtonBmp)
            {
                backButtonSize = backButtonBmp.Size;
            }
            return new Rectangle(bounds.Right - xOffset * 4 - downButtonWidth - backButtonSize.Width,
                                  bounds.Y + yOffset + textPadding,
                                  backButtonSize.Width,
                                  backButtonSize.Height);
        }

        internal static int GetDetailsButtonWidth()
        {
            int width = 0;
            Bitmap detailsBmp = GetDetailsBmp();
            lock (detailsBmp)
            {
                width = detailsBmp.Size.Width;
            }
            return width;
        }

        internal static Rectangle GetDetailsButtonRect(Rectangle bounds, bool alignRight)
        {
            Size downButtonSize;
            Bitmap detailsBmp = GetDetailsBmp();
            lock (detailsBmp)
            {
                downButtonSize = detailsBmp.Size;
            }
            int downButtonWidth = downButtonSize.Width;
            return new Rectangle(bounds.Right - xOffset * 2 - downButtonWidth,
                                  bounds.Y + yOffset + textPadding,
                                  downButtonWidth,
                                  downButtonSize.Height);
        }

        /// <summary>
        ///  Called by the dataGrid when it needs the caption
        ///  to repaint.
        /// </summary>
        internal void Paint(Graphics g, Rectangle bounds, bool alignRight)
        {
            Size textSize = new Size((int)g.MeasureString(text, Font).Width + 2, Font.Height + 2);

            downButtonRect = GetDetailsButtonRect(bounds, alignRight);
            int downButtonWidth = GetDetailsButtonWidth();
            backButtonRect = GetBackButtonRect(bounds, alignRight, downButtonWidth);

            int backButtonArea = backButtonVisible ? backButtonRect.Width + xOffset + buttonToText : 0;
            int downButtonArea = downButtonVisible && !dataGrid.ParentRowsIsEmpty() ? downButtonWidth + xOffset + buttonToText : 0;

            int textWidthLeft = bounds.Width - xOffset - backButtonArea - downButtonArea;

            textRect = new Rectangle(
                                    bounds.X,
                                    bounds.Y + yOffset,
                                    Math.Min(textWidthLeft, 2 * textPadding + textSize.Width),
                                    2 * textPadding + textSize.Height);

            // align the caption text box, downButton, and backButton
            // if the RigthToLeft property is set to true
            if (alignRight)
            {
                textRect.X = bounds.Right - textRect.Width;
                backButtonRect.X = bounds.X + xOffset * 4 + downButtonWidth;
                downButtonRect.X = bounds.X + xOffset * 2;
            }

            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "text size = " + textSize.ToString());
            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "downButtonWidth = " + downButtonWidth.ToString(CultureInfo.InvariantCulture));
            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "textWidthLeft = " + textWidthLeft.ToString(CultureInfo.InvariantCulture));
            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "backButtonRect " + backButtonRect.ToString());
            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "textRect " + textRect.ToString());
            Debug.WriteLineIf(CompModSwitches.DGCaptionPaint.TraceVerbose, "downButtonRect " + downButtonRect.ToString());

            // we should use the code that is commented out
            // with today's code, there are pixels on the backButtonRect and the downButtonRect
            // that are getting painted twice
            //
            g.FillRectangle(backBrush, bounds);

            if (backButtonVisible)
            {
                PaintBackButton(g, backButtonRect, alignRight);
                if (backActive)
                {
                    if (lastMouseLocation == CaptionLocation.BackButton)
                    {
                        backButtonRect.Inflate(1, 1);
                        ControlPaint.DrawBorder3D(g, backButtonRect,
                                                  backPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
                    }
                }
            }
            PaintText(g, textRect, alignRight);

            if (downButtonVisible && !dataGrid.ParentRowsIsEmpty())
            {
                PaintDownButton(g, downButtonRect);
                // the rules have changed, yet again.
                // now: if we show the parent rows and the mouse is
                // not on top of this icon, then let the icon be depressed.
                // if the mouse is pressed over the icon, then show the icon pressed
                // if the mouse is over the icon and not pressed, then show the icon SunkenInner;
                //
                if (lastMouseLocation == CaptionLocation.DownButton)
                {
                    downButtonRect.Inflate(1, 1);
                    ControlPaint.DrawBorder3D(g, downButtonRect,
                                              downPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
                }
            }
        }

        private static void PaintIcon(Graphics g, Rectangle bounds, Bitmap b)
        {
            using ImageAttributes attr = new ImageAttributes();
            attr.SetRemapTable(colorMap, ColorAdjustType.Bitmap);
            g.DrawImage(b, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, attr);
        }

        private static void PaintBackButton(Graphics g, Rectangle bounds, bool alignRight)
        {
            Bitmap backButtonBmp = GetBackButtonBmp(alignRight);
            lock (backButtonBmp)
            {
                PaintIcon(g, bounds, backButtonBmp);
            }
        }

        private static void PaintDownButton(Graphics g, Rectangle bounds)
        {
            Bitmap detailsBmp = GetDetailsBmp();
            lock (detailsBmp)
            {
                PaintIcon(g, bounds, detailsBmp);
            }
        }

        private void PaintText(Graphics g, Rectangle bounds, bool alignToRight)
        {
            Rectangle textBounds = bounds;

            if (textBounds.Width <= 0 || textBounds.Height <= 0)
            {
                return;
            }

            if (textBorderVisible)
            {
                g.DrawRectangle(textBorderPen, textBounds.X, textBounds.Y, textBounds.Width - 1, textBounds.Height - 1);
                textBounds.Inflate(-1, -1);
            }

            if (textPadding > 0)
            {
                Rectangle border = textBounds;
                border.Height = textPadding;
                g.FillRectangle(backBrush, border);

                border.Y = textBounds.Bottom - textPadding;
                g.FillRectangle(backBrush, border);

                border = new Rectangle(textBounds.X, textBounds.Y + textPadding,
                                       textPadding, textBounds.Height - 2 * textPadding);
                g.FillRectangle(backBrush, border);

                border.X = textBounds.Right - textPadding;
                g.FillRectangle(backBrush, border);
                textBounds.Inflate(-textPadding, -textPadding);
            }

            g.FillRectangle(backBrush, textBounds);

            // Brush foreBrush = new SolidBrush(dataGrid.CaptionForeColor);
            using StringFormat format = new StringFormat();
            if (alignToRight)
            {
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                format.Alignment = StringAlignment.Far;
            }
            g.DrawString(text, Font, foreBrush, textBounds, format);
            // foreBrush.Dispose();
        }

        private CaptionLocation FindLocation(int x, int y)
        {
            if (!backButtonRect.IsEmpty)
            {
                if (backButtonRect.Contains(x, y))
                {
                    return CaptionLocation.BackButton;
                }
            }
            if (!downButtonRect.IsEmpty)
            {
                if (downButtonRect.Contains(x, y))
                {
                    return CaptionLocation.DownButton;
                }
            }
            if (!textRect.IsEmpty)
            {
                if (textRect.Contains(x, y))
                {
                    return CaptionLocation.Text;
                }
            }
            return CaptionLocation.Nowhere;
        }

        private bool DownButtonDown
        {
            get => downButtonDown;
            set
            {
                if (downButtonDown != value)
                {
                    downButtonDown = value;
                    InvalidateLocation(CaptionLocation.DownButton);
                }
            }
        }

        /*
        internal bool GetDownButtonDirection()
        {
            return DownButtonDown;
        }
        */

        /// <summary>
        ///  Called by the dataGrid when the mouse is pressed
        ///  inside the caption.
        /// </summary>
        internal void MouseDown(int x, int y)
        {
            CaptionLocation loc = FindLocation(x, y);
            switch (loc)
            {
                case CaptionLocation.BackButton:
                    backPressed = true;
                    InvalidateLocation(loc);
                    break;
                case CaptionLocation.DownButton:
                    downPressed = true;
                    InvalidateLocation(loc);
                    break;
                case CaptionLocation.Text:
                    OnCaptionClicked(EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        ///  Called by the dataGrid when the mouse is released
        ///  inside the caption.
        /// </summary>
        internal void MouseUp(int x, int y)
        {
            CaptionLocation loc = FindLocation(x, y);
            switch (loc)
            {
                case CaptionLocation.DownButton:
                    if (downPressed == true)
                    {
                        downPressed = false;
                        OnDownClicked(EventArgs.Empty);
                    }
                    break;
                case CaptionLocation.BackButton:
                    if (backPressed == true)
                    {
                        backPressed = false;
                        OnBackwardClicked(EventArgs.Empty);
                    }
                    break;
            }
        }

        /// <summary>
        ///  Called by the dataGrid when the mouse leaves
        ///  the caption area.
        /// </summary>
        internal void MouseLeft()
        {
            CaptionLocation oldLoc = lastMouseLocation;
            lastMouseLocation = CaptionLocation.Nowhere;
            InvalidateLocation(oldLoc);
        }

        /// <summary>
        ///  Called by the dataGrid when the mouse is
        ///  inside the caption.
        /// </summary>
        internal void MouseOver(int x, int y)
        {
            CaptionLocation newLoc = FindLocation(x, y);

            InvalidateLocation(lastMouseLocation);
            InvalidateLocation(newLoc);
            lastMouseLocation = newLoc;
        }

        /*
        protected virtual void RaiseEvent(object key, EventArgs e)
        {
            Delegate? handler = GetEventHandler(key);
            if (handler is not null)
            {
                ((EventHandler)handler)(this, e);
            }
        }

        protected virtual void RemoveEventHandler(object key, Delegate handler)
        {
            // Locking 'this' here is ok since this is an internal class.
            lock (this)
            {
                if (handler is null)
                {
                    return;
                }

                for (EventEntry? e = eventList, prev = null; e is not null; prev = e, e = e.next)
                {
                    if (e.key == key)
                    {
                        e.handler = Delegate.Remove(e.handler, handler);
                        if (e.handler is null)
                        {
                            if (prev is null)
                            {
                                eventList = e.next;
                            }
                            else
                            {
                                prev.next = e.next;
                            }
                        }
                        return;
                    }
                }
            }
        }

        protected virtual void RemoveEventHandlers()
        {
            eventList = null;
        }
        */

        internal void SetDownButtonDirection(bool pointDown)
        {
            DownButtonDown = pointDown;
        }

        /// <summary>
        ///  Toggles the direction the "Down Button" is pointing.
        /// </summary>
        internal bool ToggleDownButtonDirection()
        {
            DownButtonDown = !DownButtonDown;
            return DownButtonDown;
        }

        internal enum CaptionLocation
        {
            Nowhere,
            BackButton,
            DownButton,
            Text
        }

        /*
        private sealed class EventEntry
        {
            internal EventEntry? next;
            internal object key;
            internal Delegate? handler;

            internal EventEntry(EventEntry? next, object key, Delegate handler)
            {
                this.next = next;
                this.key = key;
                this.handler = handler;
            }
        }
        */
    }
}

