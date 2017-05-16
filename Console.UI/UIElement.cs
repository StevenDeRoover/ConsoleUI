using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IUIElement
    {
        void Render();
    }

    public abstract class UIElement : IUIElement
    {
        public UIElement Parent { get; set; }
        public Size Size { get; set; } = new Size();
        public Location Location { get; set; } = new Location();

        /// <summary>
        /// Creates a ConsoleGraphics instance
        /// </summary>
        /// <returns></returns>
        protected IConsoleGraphics CreateGraphics()
        {
            return ConsoleGraphicsFactory.Build(Parent);
        }

        internal Area GetDrawingArea(IUIElement uiElement)
        {
            return new Area();
        }

        /// <summary>
        /// Render the UI Element
        /// </summary>
        public virtual void Render()
        {
        }
    }
}
