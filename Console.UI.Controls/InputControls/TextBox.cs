using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.InputControls
{
    public class TextBox : Control, IControl
    {
        public override void Render()
        {
            var g = CreateGraphics();
            g.FillRect(0,0, g.Width, g.Height, this.BackgroundColor);
        }

        public override void Message(IMessage message)
        {
            if (message is KeyMessage)
            {
                var keyInfo = (message as KeyMessage).KeyInfo;
            }
            base.Message(message);
        }
    }
}
