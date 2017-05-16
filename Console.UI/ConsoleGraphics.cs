using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IConsoleGraphics
    {
        void FillRect(short x, short y, short width, short height, ConsoleColor color);
    }

    internal class ConsoleGraphics : IConsoleGraphics
    {
        private UIElement _uiElement;
        public ConsoleGraphics(UIElement uiElement)
        {

        }

        private Area GetDrawingArea()
        {
            return _uiElement.Parent == null
                ? GetConsoleDrawingArea()
                : _uiElement.Parent.GetDrawingArea(_uiElement);
        }

        private Area GetConsoleDrawingArea()
        {
            return new Area()
            {
                Location = new Location()
                {
                    X = 0,
                    Y = 0
                },
                Size = new Size()
                {
                    Width = (short)System.Console.BufferWidth,
                    Height = (short)System.Console.BufferHeight
                }
            };
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

        public void FillRect(short x, short y, short width, short height, ConsoleColor backgroundColor)
        {
            IntPtr buffer = default(IntPtr);
            var drawingArea = GetDrawingArea();
            try
            {
                for (short h = y; h <= (height + y - 1); h++)
                {
                    for (short w = x; w <= (width + x - 1); w++)
                    {
                        buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CHAR_INFO)));
                        if (buffer == IntPtr.Zero)
                            throw new OutOfMemoryException();
                        IntPtr ptr = buffer;

                        const int STD_OUTPUT_HANDLE = -11;

                        COORD coord = new COORD();
                        SMALL_RECT rc = new SMALL_RECT();
                        rc.Left = w;
                        rc.Top = h;
                        rc.Right = w;
                        rc.Bottom = h;

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
                        ci.charData = System.Console.OutputEncoding.GetBytes(new char[] { ' ', (char)0 });
                        ci.attributes = (short)((int)backgroundColor << 4);
                        Marshal.StructureToPtr(ci, buffer, false);
                        var test = WriteConsoleOutput(stdHandle, buffer, size, coord, ref rc);
                        ptr += Marshal.SizeOf(typeof(CHAR_INFO));
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
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



                CHAR_INFO ci = (CHAR_INFO) Marshal.PtrToStructure(ptr, typeof(CHAR_INFO));
                ci.charData = System.Console.OutputEncoding.GetBytes(new char[] {character, (char) 0});
                ci.attributes = (short) ((int) color >> 0);
                Marshal.StructureToPtr(ci, buffer, false);
                var test = WriteConsoleOutput(stdHandle, buffer, size, coord, ref rc);
                ptr += Marshal.SizeOf(typeof(CHAR_INFO));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

        }

        public void DrawRect(short x, short y, short width, short height, ConsoleColor foregroundColor)
        {

            var drawingArea = GetDrawingArea();
            try
            {
                var minHeight = y;
                var maxHeight = height + y - 1;
                for (short h = y; h <= (height + y - 1); h++)
                {
                       
                }
            }
            finally
            {
                
            }
        }
    }

    internal static class ConsoleGraphicsFactory
    {
        public static IConsoleGraphics Build(IUIElement uiElement)
        {
            return new ConsoleGraphics((UIElement)uiElement);
        }
    }
}
