using Console.UI.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public abstract class UIElement : IUIElement, INotifyPropertyChanged
    {
        public UIElement Parent { get; set; }
        public Size Size { get; set; } = new Size() { Width = new Drawing.Measurement { IsStretch=true }, Height = new Drawing.Measurement { IsStretch = true } };
        public Point Location { get; set; } = new Point();
        public Drawing.Margin Margin { get; set; } = new Drawing.Margin();
        public Drawing.Margin Padding { get; set; } = new Drawing.Margin();
        public Area AvailableDrawingArea { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Creates a ConsoleGraphics instance
        /// </summary>
        /// <returns></returns>
        protected ConsoleGraphics CreateGraphics()
        {
            return ConsoleGraphicsFactory.Build(this);
        }

        public Area GetDrawingArea()
        {
            var area = AvailableDrawingArea != null ? AvailableDrawingArea : Area.GetConsoleDrawingArea();
            var drawingArea = new Area() { LeftTop = new Point(area.LeftTop.X, area.LeftTop.Y), RightBottom = new Point(area.RightBottom.X, area.RightBottom.Y) };
            area.LeftTop.X += this.Margin.Left;
            area.LeftTop.Y += this.Margin.Top;
            area.RightBottom.X -= this.Margin.Right;
            area.RightBottom.Y -= this.Margin.Bottom;
            return area;
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

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
