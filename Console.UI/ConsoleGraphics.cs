﻿using Console.UI.Drawing;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console.UI.IO;

namespace Console.UI
{

    public class ConsoleGraphics
    {
        private UIElement _uiElement;
        private Area _availableDrawingArea;

        public short Width { get { return _availableDrawingArea.Size.Width.Amount.Value; } }
        public short Height { get { return _availableDrawingArea.Size.Height.Amount.Value; } }

        /// <summary>
        /// private constructor not available
        /// </summary>
        private ConsoleGraphics()
        { }
        internal ConsoleGraphics(UIElement uiElement)
        {
            _uiElement = uiElement;
            _availableDrawingArea = _uiElement.AvailableDrawingArea;
        }

        public void FillRect(short x, short y, short width, short height, ConsoleColor backgroundColor)
        {
            var left = _availableDrawingArea.LeftTop.X + x;
            var top = _availableDrawingArea.LeftTop.Y + y;
            var theWidth = Math.Min(_availableDrawingArea.Size.Width.Amount.GetValueOrDefault(), width);
            var theHeight = Math.Min(_availableDrawingArea.Size.Height.Amount.GetValueOrDefault(), height);
            Native.WithNewBuffer((short)left, (short)top, theWidth, theHeight, (point, charinfo) =>
            {
                charinfo.charData = System.Console.OutputEncoding.GetBytes(new char[] { ' ', (char)0 });
                charinfo.attributes = 0;
                charinfo = Native.SetBackgroundColor(charinfo, backgroundColor);
                return charinfo;
            });
        }

        private Area Sum(Area outer, Area inner)
        {
            return new Area
            {
                LeftTop = new Point(
                     Math.Min(outer.RightBottom.X, (short)(outer.LeftTop.X + inner.LeftTop.X)),
                     Math.Min(outer.RightBottom.Y, (short)(outer.LeftTop.Y + inner.LeftTop.Y))
                    ),
                RightBottom = new Point(
                    Math.Min(outer.RightBottom.X, (short)(outer.RightBottom.X + inner.RightBottom.X)),
                    Math.Min(outer.RightBottom.Y, (short)(outer.RightBottom.Y + inner.RightBottom.Y))
                    )
            };
        }

        public void Move(Area from, Point to)
        {
            short leftFrom = (short)(_availableDrawingArea.LeftTop.X + from.LeftTop.X);
            short topFrom = (short)(_availableDrawingArea.LeftTop.Y + from.LeftTop.Y);
            short width = Math.Min(_availableDrawingArea.Size.Width.Amount.GetValueOrDefault(), from.Size.Width.Amount.GetValueOrDefault());
            short height =(short)( Math.Min(_availableDrawingArea.Size.Height.Amount.GetValueOrDefault(), from.Size.Width.Amount.GetValueOrDefault()) - 1);

            short leftTo = (short)(_availableDrawingArea.LeftTop.X + to.X);
            short topTo = (short)(_availableDrawingArea.LeftTop.Y + to.Y);

            Native.Move(leftFrom, topFrom, width, height, leftTo, topTo);
        }

        public void DrawText(short left, int top, string text, ConsoleColor color)
        {
            if (!string.IsNullOrEmpty(text))
            {

                var x = _availableDrawingArea.LeftTop.X + left;
                var y = _availableDrawingArea.LeftTop.Y + top;
                var theText = text ?? "";
                var width = (_availableDrawingArea.Size.Width.Amount.GetValueOrDefault() - left);
                width = Math.Min((theText ?? "").Length, width);
                Native.WithLoadedBuffer((short)(x), (short)(y), (short)width, 1, (point, ci) =>
                {
                    if (point.X < theText.Length)
                    {
                        ci.charData = System.Console.OutputEncoding.GetBytes(new char[] { theText[point.X], (char)0 });
                        ci = Native.SetForegroundColor(ci, color);
                    }

                    return ci;
                });
            }
        }

        public void DrawHorizontalLine(short x, short line, short width, ConsoleColor color)
        {
            Native.WithLoadedBuffer(x, line, width, 1, (point, ci) =>
            {
                ci.charData = System.Console.OutputEncoding.GetBytes(new char[] { '─', (char)0 });
                ci = Native.SetForegroundColor(ci, color);
                return ci;
            });
        }

        public enum LineThickness
        {
            Single,
            Double
        }

        public void DrawRect(short x, short y, short width, short height, ConsoleColor foregroundColor, LineThickness thickness = LineThickness.Single)
        {
            var drawingArea = _uiElement.GetDrawingArea();

            Native.WithLoadedBuffer((short)(drawingArea.LeftTop.X + x), (short)(drawingArea.LeftTop.Y + y), Math.Min((short)drawingArea.Size.Width.Amount, width), Math.Min((short)drawingArea.Size.Height.Amount, height), (point, charinfo) =>
            {
                var w = point.X;
                var h = point.Y;
                if (w == x && h == y)
                {
                    //left top
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┌', (char)0 });
                }
                if (w == (width - 1) && h == y)
                {
                    //right top
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┐', (char)0 });
                }
                else if (w == x && h == (height - 1))
                {
                    //left bottom
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '└', (char)0 });
                }
                else if (w == (width - 1) && h == (height - 1))
                {
                    //left bottom
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┘', (char)0 });
                }
                else if ((w == x || w == (width - 1)) && h > y && h < (height - 1))
                {
                    // left/right line
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '│', (char)0 });
                }
                else if ((h == y || h == (height - 1)) && w > x && w < (width - 1))
                {
                    // top/bottom line
                    charinfo.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '─', (char)0 });
                }
                charinfo = Native.SetForegroundColor(charinfo, foregroundColor);
                return charinfo;
            });
        }
    }

    internal static class ConsoleGraphicsFactory
    {
        public static ConsoleGraphics Build(UIElement uiElement)
        {
            return new ConsoleGraphics((UIElement)uiElement);
        }
    }
}
