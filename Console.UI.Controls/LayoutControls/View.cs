using Console.UI.Controls.eArgs;
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
        bool IsClosed { get; set; }

        void SetAvailableArea(Area area);
        void Close();
    }

    public class View : UIElement, IView
    {
        public EventHandler<ClosingEventArgs> Closing;
        public View()
        {
            //defaults
            this.Size = new Size { Width = new Drawing.Measurement { IsStretch = true }, Height = new Drawing.Measurement { IsStretch = true } };
        }
        public override void Render()
        {
            var g = CreateGraphics();
            
            if (Child != null)
            {
                Child.AvailableDrawingArea = GetDrawingArea();
                Child.Render();
            }
        }

        public void SetAvailableArea(Area area)
        {
            this.AvailableDrawingArea = area;
        }

        public void Close()
        {
            if (Closing != null) {
                var closingEArgs = new ClosingEventArgs();
                Closing.GetInvocationList().ToList().ForEach((d) => { d.DynamicInvoke(new object[] { this, closingEArgs }); });
                if (closingEArgs.IsDefaultPrevented)
                {
                    return;
                }
            }
            IsClosed = true;
        }

        public UIElement Child { get; set; }
        public bool IsClosed { get; set; }
    }
}
