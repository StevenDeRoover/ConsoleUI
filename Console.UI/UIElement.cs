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

        void Message(Console.UI.IMessage message);
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
            return ConsoleGraphicsFactory.Build(this);
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

        public virtual void Message(IMessage message)
        {
        }
    }
}
