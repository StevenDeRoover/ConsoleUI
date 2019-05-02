using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Console.UI;
using Console.UI.Controls;
using ConsoleUI.Screens;

namespace ConsoleUI
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private const int GWL_STYLE = -16;



        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [STAThread]
        static void Main(string[] args)
        {
            System.Console.Title = "DCA Data Migration";
            System.Console.SetWindowSize(Math.Min(200, System.Console.LargestWindowWidth),    Math.Min(60, System.Console.LargestWindowHeight));

            Process p = Process.GetCurrentProcess();
            var windowLong = GetWindowLong(p.MainWindowHandle, GWL_STYLE).ToInt32();

            windowLong = windowLong & -131073 & -65537;

            SetWindowLong(p.MainWindowHandle, GWL_STYLE, System.Convert.ToInt32(windowLong));
            
            //ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
            ////DeleteMenu(GetSystemMenu(p.MainWindowHandle, false), SC_CLOSE, MF_BYCOMMAND);
            //DeleteMenu(GetSystemMenu(p.MainWindowHandle, false), 0, MF_BYCOMMAND);
            //DeleteMenu(GetSystemMenu(p.MainWindowHandle, false), 1, MF_BYCOMMAND);
            //DrawMenuBar(p.MainWindowHandle);
            Application.AttachMouse = false;
            Application.Run(new ApplicationContext { MainView = new MainScreen() });
        }
    }
}
