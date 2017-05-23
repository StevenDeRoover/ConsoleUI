using Console.UI.Controls.LayoutControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console.UI;

namespace ConsoleUI.CustomControls
{
    public class PanelWithTime : Panel
    {
        public string Format { get; set; } = "HH:mm:ss";
        public ConsoleColor TimeColor { get; set; }
        public override void Render()
        {
            base.ReserveRightAreaTitle = Format.Length + 1;
            base.Render();
            var g = CreateGraphics();
            g.FillRect((short)(g.Width - Format.Length - 1), 0, (short)Format.Length, 1, BorderColor);
            Timer t = new Timer(new TimerCallback((obj) => {
                DrawTime();
            }), null, 0, 1000); 
        }

        private void DrawTime()
        {
            var g = CreateGraphics();
            var dateTimeString = DateTime.Now.ToString(Format);
            g.DrawText((short)(g.Width - dateTimeString.Length - 1), 0, dateTimeString, TimeColor);
        }

        protected override void DrawTitle(ConsoleGraphics g)
        {
            base.DrawTitle(g);
            DrawTime();
        }
    }
}
