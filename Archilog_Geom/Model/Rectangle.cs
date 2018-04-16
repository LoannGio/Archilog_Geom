using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class Rectangle : AObservableShape
    {
        private int Width { get; set; }
        private int Height { get; set; }
        private int X { get; set; }
        private int Y { get; set; }
        private int RotationAngle { get; set; }
        private Point rotationCenter;
        public Color Color { get; set; }

        public Rectangle(int x, int y)
        {
            X = x;
            Y = y;
            Width = 10;
            Height = 5;
            RotationAngle = 0;
            rotationCenter.X = X + Width / 2;
            rotationCenter.Y = Y + Height / 2;
            Color = Color.Blue;
        }
    }
}
