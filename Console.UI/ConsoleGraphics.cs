using Console.UI.Drawing;
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
            _availableDrawingArea = _uiElement.GetDrawingArea();
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct CHAR_INFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] charData;
            public short attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public short wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpReadRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpWriteRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr CreateConsoleScreenBuffer(
            UInt32 dwDesiredAccess,
            UInt32 dwShareMode,
            IntPtr secutiryAttributes, UInt32 flags, IntPtr screenBufferData);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleActiveScreenBuffer(
        IntPtr hConsoleOutput
        );

        public void FillRect(short x, short y, short width, short height, ConsoleColor backgroundColor)
        {
            const uint GENERIC_READ = 0x80000000;
            const uint GENERIC_WRITE = 0x40000000;
            const int FILE_SHARE_READ = 1;
            const int FILE_SHARE_WRITE = 2;
            const int CONSOLE_TEXTMODE_BUFFER = 1;
            const int STD_OUTPUT_HANDLE = -11;
            
            var stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            //var scrBuffer = CreateConsoleScreenBuffer(GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);
            //SetConsoleActiveScreenBuffer(scrBuffer);
            
            var buffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO)));
            CHAR_INFO[] ci = new CHAR_INFO[width * height];
            SMALL_RECT rect = new SMALL_RECT() { Left = x, Top = y, Right = (short)(width - 1), Bottom = (short)(height - 1) };
            COORD leftTop = new COORD() { X = x, Y = y };
            COORD size = new COORD() { X = width, Y = height };
            
            for (int i = 0; i < ci.Length; ++i)
            {
                ci[i].charData = System.Console.OutputEncoding.GetBytes(new char[] { ' ', (char)1 });
                ci[i].attributes = (short)(ci[i].attributes | (short)((int)backgroundColor << 4));
            }

            long LongPtr = buffer.ToInt64(); // Must work both on x86 and x64
            for (int I = 0; I < ci.Length; I++)
            {
                IntPtr RectPtr = new IntPtr(LongPtr);
                Marshal.StructureToPtr(ci[I], RectPtr, false); // You do not need to erase struct in this case
                LongPtr += Marshal.SizeOf(typeof(CHAR_INFO));
            }
            
            WriteConsoleOutput(stdHandle, buffer, size, leftTop, ref rect);

            //SetConsoleActiveScreenBuffer(scrBuffer);
        }

        //public void FillRectOld(short x, short y, short width, short height, ConsoleColor backgroundColor)
        //{
        //    IntPtr buffer = default(IntPtr);
        //    var area = Sum(_availableDrawingArea, new Area { LeftTop = new Point(x, y), RightBottom = new Point(width, height) });
        //    try
        //    {
        //        for (short h = area.LeftTop.Y; h < area.RightBottom.Y; h++)
        //        {
        //            for (short w = area.LeftTop.X; w < area.RightBottom.X; w++)
        //            {
        //                buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CHAR_INFO)));
        //                if (buffer == IntPtr.Zero)
        //                    throw new OutOfMemoryException();
        //                IntPtr ptr = buffer;

        //                const int STD_OUTPUT_HANDLE = -11;

        //                COORD coord = new COORD();
        //                SMALL_RECT rc = new SMALL_RECT();
        //                rc.Left = w;
        //                rc.Top = h;
        //                rc.Right = w;
        //                rc.Bottom = h;

        //                COORD size = new COORD();
        //                size.X = 1;
        //                size.Y = 1;

        //                var stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
        //                if (!ReadConsoleOutput(stdHandle, buffer, size, coord, ref rc))
        //                {
        //                    // 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
        //                    throw new Win32Exception(Marshal.GetLastWin32Error());
        //                }

        //                CHAR_INFO ci = (CHAR_INFO)Marshal.PtrToStructure(ptr, typeof(CHAR_INFO));
        //                ci.charData = System.Console.OutputEncoding.GetBytes(new char[] { ' ', (char)0 });
        //                ci.attributes = (short)((int)backgroundColor << 4);
        //                Marshal.StructureToPtr(ci, buffer, false);
        //                var test = WriteConsoleOutput(stdHandle, buffer, size, coord, ref rc);
        //                ptr += Marshal.SizeOf(typeof(CHAR_INFO));
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buffer);
        //    }
        //}

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

        private void WriteCharAt(short x, short y, char character, ConsoleColor color)
        {
            IntPtr buffer = default(IntPtr);
            try
            {
                buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CHAR_INFO)));
                if (buffer == IntPtr.Zero)
                    throw new OutOfMemoryException();
                IntPtr ptr = buffer;

                const int STD_OUTPUT_HANDLE = -11;

                COORD coord = new COORD();
                SMALL_RECT rc = new SMALL_RECT();
                rc.Left = x;
                rc.Top = y;
                rc.Right = x;
                rc.Bottom = y;

                COORD size = new COORD();
                size.X = 1;
                size.Y = 1;

                var stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
                if (!ReadConsoleOutput(stdHandle, buffer, size, coord, ref rc))
                {
                    // 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }



                CHAR_INFO ci = (CHAR_INFO)Marshal.PtrToStructure(ptr, typeof(CHAR_INFO));
                ci.charData = System.Console.OutputEncoding.GetBytes(new char[] { character, (char)0 });
                ci.attributes = (short)((int)color >> 0);
                Marshal.StructureToPtr(ci, buffer, false);
                var test = WriteConsoleOutput(stdHandle, buffer, size, coord, ref rc);
                ptr += Marshal.SizeOf(typeof(CHAR_INFO));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

        }

        public enum LineThickness
        {
            Single,
            Double
        }

        public void DrawHorizontalLine(short line, short start, short end, LineThickness thickness = LineThickness.Single)
        {
            
        }

        public void DrawVerticalLine(short col, short start, short end)
        { }

        public void DrawRect(short x, short y, short width, short height, ConsoleColor foregroundColor, LineThickness thickness = LineThickness.Single)
        {
            var drawingArea = _uiElement.GetDrawingArea();
            try
            {
                const uint GENERIC_READ = 0x80000000;
                const uint GENERIC_WRITE = 0x40000000;
                const int FILE_SHARE_READ = 1;
                const int FILE_SHARE_WRITE = 2;
                const int CONSOLE_TEXTMODE_BUFFER = 1;
                const int STD_OUTPUT_HANDLE = -11;

                var stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);

                var buffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO)));
                SMALL_RECT rect = new SMALL_RECT() { Left = x, Top = y, Right = (short)(width - 1), Bottom = (short)(height - 1) };
                COORD leftTop = new COORD() { X = x, Y = y };
                COORD size = new COORD() { X = (short)width, Y = (short)height };

                if (!ReadConsoleOutput(stdHandle, buffer, size, leftTop, ref rect))
                {
                    // 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                var newBuffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO))); ;
                long LongPtr = newBuffer.ToInt64(); // Must work both on x86 and x64
                for (short h = 0; h < height; h++)
                {
                    for (short w = 0; w < width; w++)
                    {
                        CHAR_INFO ci = (CHAR_INFO)Marshal.PtrToStructure(buffer, typeof(CHAR_INFO));
                        char[] chars = System.Console.OutputEncoding.GetChars(ci.charData);
                        if (w == x && h == y)
                        {
                            //left top
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┌', (char)0 });
                        }
                        if (w == (width - 1) && h == y)
                        {
                            //right top
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┐', (char)0 });
                        }
                        else if (w == x && h == (height - 1))
                        {
                            //left bottom
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '└', (char)0 });
                        }
                        else if (w == (width - 1) && h == (height - 1))
                        {
                            //left bottom
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '┘', (char)0 });
                        }
                        else if ((w == x || w == (width - 1)) && h > y && h < (height - 1))
                        {
                            // left/right line
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '│', (char)0 });
                        }
                        else if ((h == y || h == (height - 1)) && w > x && w < (width - 1))
                        {
                            // top/bottom line
                            ci.charData = Encoding.GetEncoding(437).GetBytes(new char[] { '─', (char)0 });
                        }
                        

                        IntPtr RectPtr = new IntPtr(LongPtr);
                        Marshal.StructureToPtr(ci, RectPtr, false); // You do not need to erase struct in this case
                        LongPtr += Marshal.SizeOf(typeof(CHAR_INFO));
                    }
                }
                WriteConsoleOutput(stdHandle, newBuffer, size, leftTop, ref rect);
            }
            finally
            {

            }
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
