using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    public interface IView : IUIElement
    {
    }

    public class View : UIElement, IView
    {
        public override void Render()
        {
            CreateGraphics().FillRect(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height, ConsoleColor.Green);
        }
    }
}
