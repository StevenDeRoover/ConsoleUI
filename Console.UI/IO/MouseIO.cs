using Console.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.IO
{
    internal class MouseIO : IFocusManager
    {
        private class MousePositionInfo {
            public Point Point { get; set; }
            public CHAR_INFO charInfo { get; set; }
        }

        private MousePositionInfo _previousPosition;
        public void Message(IMessage message)
        {
            if (message is MouseMessage)
            {
                MouseMessage msg = message as MouseMessage;
                if (_previousPosition != null)
                {
                    Native.WithLoadedBuffer(_previousPosition.Point.X, _previousPosition.Point.Y, 1,1, (pos, charInfo) => {
                        return _previousPosition.charInfo;
                    });
                }

                Native.WithLoadedBuffer(msg.X, msg.Y, 1, 1, (pos, ci) => {
                    _previousPosition = new MousePositionInfo() { charInfo = ci, Point = new Point(msg.X, msg.Y) };
                    ci = Native.SetBackgroundColor(ci, ConsoleColor.White);
                    return ci;
                });

            }
        }
    }
}
