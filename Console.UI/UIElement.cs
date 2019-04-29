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
            var drawingArea = new Area(area.LeftTop.X, area.LeftTop.Y, area.RightBottom.X, area.RightBottom.Y);
            drawingArea.LeftTop.X += this.Margin.Left;
            drawingArea.LeftTop.Y += this.Margin.Top;
            drawingArea.RightBottom.X -= this.Margin.Right;
            drawingArea.RightBottom.Y -= this.Margin.Bottom;
            return drawingArea;
        }

        public Area GetClientArea()
        {
            var area = GetDrawingArea();
            var drawingArea = new Area(area.LeftTop.X, area.LeftTop.Y, area.RightBottom.X, area.RightBottom.Y);
            drawingArea.LeftTop.X += 1;
            drawingArea.LeftTop.Y += 1;
            drawingArea.RightBottom.X -= (short)(1 + this.Margin.Right);
            drawingArea.RightBottom.Y -= (short)(1 + this.Margin.Bottom);
            return drawingArea;
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
