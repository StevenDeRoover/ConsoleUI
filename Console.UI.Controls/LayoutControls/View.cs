using Console.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    public interface IView : IUIElement
    {
        void SetAvailableArea(Area area);
    }

    public class View : UIElement, IView
    {
        public View()
        {
            //defaults
            this.Size = new Size { Width = new Drawing.Measurement { IsStretch = true }, Height = new Drawing.Measurement { IsStretch = true } };
        }
        public override void Render()
        {
            var g = CreateGraphics();
            //g.FillRect(0,0, g.Width, g.Height);
            if (Child != null)
            {
                GetDrawingArea();
            }
        }

        public void SetAvailableArea(Area area)
        {
            this.AvailableDrawingArea = area;
        }

        public UIElement Child { get; set; }
    }
}
