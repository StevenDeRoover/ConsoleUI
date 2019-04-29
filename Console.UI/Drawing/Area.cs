using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Drawing
{
    public class Area
    {
        public Area()
        {
        }

        public Area(short leftTopX, short leftTopY, short rightBottomX, short rightBottomY)
        {
            this.LeftTop = new Point(leftTopX, leftTopY);
            this.RightBottom = new Point(rightBottomX, rightBottomY);
        }

        public static Area GetConsoleDrawingArea()
        {
            return new Area()
            {
                LeftTop = new Point()
                {
                    X = 0,
                    Y = 0
                },
                RightBottom = new Point()
                {
                    X = (short)System.Console.WindowWidth,
                    Y = (short)System.Console.WindowHeight
                }
            };
        }
        public Point LeftTop { get; set; }
        public Point RightBottom { get; set; }
        public Size Size
        {
            get
            {
                return new Size
                {
                    Width = new Measurement
                    {
                        Amount = (short)(RightBottom.X - LeftTop.X)
                    },
                    Height = new Measurement
                    {
                        Amount = (short)(this.RightBottom.Y - this.LeftTop.Y)
                    }
                };
            }
        }
    }
}