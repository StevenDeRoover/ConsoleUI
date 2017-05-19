using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    public class Panel : Control
    {
        public UIElement Child { get; set; }

        public string Title { get; set; }

        public enum EnumTitlePosition
        {
            Left,
            Right,
            Center
        }

        public EnumTitlePosition TitlePosition { get; set; }

        public override void Render()
        {
            var g = CreateGraphics();
            g.FillRect(0, 0, g.Width, g.Height, this.BackgroundColor);
            g.DrawRect(0, 0, g.Width, g.Height, this.ForegroundColor);
            //g.DrawText(this.Title)
            if (Child != null)
            {
                Child.AvailableDrawingArea = GetDrawingArea();

                Child.Render();
            }
        }
    }
}
