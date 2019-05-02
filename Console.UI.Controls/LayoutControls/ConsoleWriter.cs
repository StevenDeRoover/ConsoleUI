using Console.UI;
using Console.UI.Controls.InputControls;
using Console.UI.Controls.LayoutControls;
using Console.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    public class ConsoleWriter : Panel
    {
        private Point _cursor;
        public ConsoleWriter()
        {
            this.BackgroundColor = ConsoleColor.Black;
            this.ForegroundColor = ConsoleColor.White;
            this.DrawBorder = false;
            this._cursor = new Point(0, 0);
        }

        public void Write(string value)
        {
            DrawText(value, false);
        }

        public void WriteLine(string value)
        {
            DrawText(value, true);
        }

        private void DrawText(string value, bool addNewline)
        {
            var g = CreateGraphics();
            var text = value;
            while (!string.IsNullOrEmpty(text))
            {
                var availableSpace = g.Width - _cursor.X;
                if (availableSpace <= 0)
                {
                    _cursor.Y += 1;
                    _cursor.X = 0;
                    availableSpace = g.Width - _cursor.X;
                }
                var index = Math.Min(availableSpace, text.Length);
                var currentText = text.Substring(0, index).Replace("\t", "[TAB]").TrimStart().Replace("[TAB]", "   ");
                if (_cursor.Y > g.Height - 1)
                {
                    g.Move(new Area(0, 1, g.Width, (short)(g.Height - 1)), new Point(0, 0));
                    g.FillRect(0, (short)(g.Height - 1), g.Width, 1, this.BackgroundColor);
                    _cursor.Y = (short)(g.Height - 1);
                    _cursor.X = 0;
                }
                g.DrawText(_cursor.X, _cursor.Y, currentText, this.ForegroundColor);
                _cursor.X += (short)currentText.Length;
                text = text.Substring(index);
                if (addNewline) {
                    _cursor.Y += 1;
                    _cursor.X = 0;
                }
            }
        }
    }
}
