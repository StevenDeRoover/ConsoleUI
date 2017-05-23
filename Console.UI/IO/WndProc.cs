using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console.UI.IO
{
    public static class WndProc
    {
        private static Stack<IFocusManager> _focusManagers = new Stack<IFocusManager>();
        private static Thread _t;

        public static void Init()
        {
            var handle = Native.GetConsoleInputHandle();

            int mode = 0;
            if (!(Native.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

            mode |= Native.ENABLE_MOUSE_INPUT;
            mode &= ~Native.ENABLE_QUICK_EDIT_MODE;
            mode |= Native.ENABLE_EXTENDED_FLAGS;

            if (!(Native.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }
            _t = new Thread(new ThreadStart(() => {
                Start();
            }));
            _t.Start();
        }

        public static void Start()
        {
            var record = new INPUT_RECORD();
            uint recordLen = 0;
            while (true)
            {
                var handle = Native.GetConsoleInputHandle();
                if (!(Native.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
                switch (record.EventType)
                {
                    case Native.MOUSE_EVENT:
                        {
                            //Console.WriteLine("Mouse event");
                            //Console.WriteLine(string.Format("    X ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.X));
                            //Console.WriteLine(string.Format("    Y ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.Y));
                            //Console.WriteLine(string.Format("    dwButtonState ...: 0x{0:X4}  ", record.MouseEvent.dwButtonState));
                            //Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.MouseEvent.dwControlKeyState));
                            //Console.WriteLine(string.Format("    dwEventFlags ....: 0x{0:X4}  ", record.MouseEvent.dwEventFlags));
                            var message = new MouseMessage(record.MouseEvent);
                            _focusManagers.ToList().ForEach((fm) => {
                                fm.Message(message);
                            });
                        }
                        break;

                    case Native.KEY_EVENT:
                        {
                            //Console.WriteLine("Key event  ");
                            //Console.WriteLine(string.Format("    bKeyDown  .......:  {0,5}  ", record.KeyEvent.bKeyDown));
                            //Console.WriteLine(string.Format("    wRepeatCount ....:   {0,4:0}  ", record.KeyEvent.wRepeatCount));
                            //Console.WriteLine(string.Format("    wVirtualKeyCode .:   {0,4:0}  ", record.KeyEvent.wVirtualKeyCode));
                            //Console.WriteLine(string.Format("    uChar ...........:      {0}  ", record.KeyEvent.UnicodeChar));
                            //Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.KeyEvent.dwControlKeyState));
                            var message = new KeyMessage(record.KeyEvent);
                            _focusManagers.ToList().ForEach((fm) => {
                                fm.Message(message);
                            });
                            //if (record.KeyEvent.wVirtualKeyCode == (int)ConsoleKey.Escape) { return; }
                        }
                        break;
                }
            }
        }

        public static void Attach(IFocusManager focusManager)
        {
            _focusManagers.Push(focusManager);
        }
    }
}
