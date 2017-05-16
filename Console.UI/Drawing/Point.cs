using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Drawing
{
    public class Point
    {
        public Point() { }

        public Point(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }

        public short X { get; set; }
        public short Y { get; set; }
    }
}
