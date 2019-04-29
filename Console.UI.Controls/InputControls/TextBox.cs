using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.InputControls
{
    public class TextBox : TextControl, IControl
    {
        public override void Render()
        {
            var g = CreateGraphics();
            g.FillRect(0,0, g.Width, g.Height, this.BackgroundColor);
            g.DrawText(0, 0, this.Text, this.ForegroundColor);
        }

        public override void Message(IMessage message)
        {
            base.Message(message);
        }
    }
}
