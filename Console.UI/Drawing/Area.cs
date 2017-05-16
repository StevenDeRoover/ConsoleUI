using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Drawing
{
    public class Area
    {
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
                        Amount = (short)(LeftTop.X + RightBottom.X)
                    },
                    Height = new Measurement
                    {
                        Amount = (short)(this.LeftTop.Y + this.RightBottom.Y)
                    }
                };
            }
        }
    }
}