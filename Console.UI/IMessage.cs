using Console.UI.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IMessage
    {
    }

    public interface IKeyMessage : IMessage
    {
    }

    public class KeyMessage : IKeyMessage
    {
        private KEY_EVENT_RECORD _record;

        public bool KeyDown { get { return _record.bKeyDown; } }

        public ushort RepeatCount { get { return _record.wRepeatCount; } }

        public ConsoleKey KeyCode { get { return (ConsoleKey)_record.wVirtualKeyCode; } }

        public char AsciiChar { get { return (char)_record.AsciiChar; } }

        public Modifiers Modifier{ get { return (Modifiers)_record.dwControlKeyState; } }

        internal KeyMessage(KEY_EVENT_RECORD record) {
            _record = record;
        }
    }

    public interface IMouseMessage : IMessage
    { }

    public class MouseMessage : IMouseMessage
    {
        private MOUSE_EVENT_RECORD _record;

        internal MouseMessage(MOUSE_EVENT_RECORD record)
        {
            _record = record;
        }
    }
}
