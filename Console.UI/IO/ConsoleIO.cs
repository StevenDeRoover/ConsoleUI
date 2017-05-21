﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.IO
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CHAR_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] charData;
        public short attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SCREEN_BUFFER_INFO
    {
        public COORD dwSize;
        public COORD dwCursorPosition;
        public short wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
    }

    internal static class Native
    {
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const int FILE_SHARE_READ = 1;
        public const int FILE_SHARE_WRITE = 2;
        public const int CONSOLE_TEXTMODE_BUFFER = 1;
        public const int STD_OUTPUT_HANDLE = -11;
       

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpReadRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpWriteRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr CreateConsoleScreenBuffer(
            UInt32 dwDesiredAccess,
            UInt32 dwShareMode,
            IntPtr secutiryAttributes, UInt32 flags, IntPtr screenBufferData);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(
        IntPtr hConsoleOutput
        );

        public static CHAR_INFO SetForegroundColor(CHAR_INFO charInfo, ConsoleColor color)
        {
            //charInfo.attributes &= ~(1 << 5);
            //charInfo.attributes &= ~(1 << 6);
            //charInfo.attributes &= ~(1 << 7);
            //charInfo.attributes &= ~(1 << 8);
            //charInfo.attributes += (short)color;
            charInfo.attributes = (short)((charInfo.attributes & (charInfo.attributes & 0xF0)) | (short)color);
            return charInfo;
        }
        public static CHAR_INFO SetBackgroundColor(CHAR_INFO charInfo, ConsoleColor color)
        {
            charInfo.attributes = (short)(charInfo.attributes | (ushort)color << 4);
            
            return charInfo;
        }

        private static void With(short x, short y, short width, short height, Func<COORD, CHAR_INFO, CHAR_INFO> doAction, bool loadFromScreen)
        {
            if (x + width > System.Console.WindowWidth)
            {
                width = (short)((System.Console.WindowWidth - 1) - x - 1);
            }
            IntPtr buffer = IntPtr.Zero;
            try
            {
                var stdHandle = Native.GetStdHandle(Native.STD_OUTPUT_HANDLE);
                buffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO)));
                SMALL_RECT rect = new SMALL_RECT() { Left = x, Top = y, Right = (short)(x + width), Bottom = (short)(y + height) };
                COORD leftTop = new COORD() { X = 0, Y = 0 };
                COORD size = new COORD() { X = (short)(width), Y = height };

                if (loadFromScreen)
                {
                    if (!Native.ReadConsoleOutput(stdHandle, buffer, size, leftTop, ref rect))
                    {
                        // 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }

                long LongPtr = buffer.ToInt64(); // Must work both on x86 and x64
                for (short h = 0; h < height; h++)
                {
                    for (short w = 0; w < width; w++)
                    {
                        IntPtr RectPtr = new IntPtr(LongPtr);
                        CHAR_INFO ci = (CHAR_INFO)Marshal.PtrToStructure(RectPtr, typeof(CHAR_INFO));
                        ci = doAction(new COORD() { X = w, Y = h }, ci);
                        //save to safeBuffer
                        Marshal.StructureToPtr(ci, RectPtr, true); // You do not need to erase struct in this case
                        LongPtr += Marshal.SizeOf(typeof(CHAR_INFO));
                    }
                }



                Native.WriteConsoleOutput(stdHandle, buffer, size, leftTop, ref rect);
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                Marshal.FreeHGlobal(buffer);
            }
        }

        internal static void WithNewBuffer(short x, short y, short width, short height, Func<COORD, CHAR_INFO, CHAR_INFO> doAction)
        {
            With(x, y, width, height, doAction, false);
        }

        public static void WithLoadedBuffer(short x, short y, short width, short height, Func<COORD, CHAR_INFO, CHAR_INFO> doAction)
        {
            With(x, y, width, height, doAction, true);
        }
    }
}
