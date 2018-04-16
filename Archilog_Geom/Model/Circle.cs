using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class Circle : AObservableShape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }

        public Circle(int x, int y, int radius, Color color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Color = color;
        }

        
    }
}
