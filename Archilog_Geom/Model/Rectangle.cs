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
        private int width, height, x, y, rotationAngle;
        private Point rotationCenter;
        private Color color;

        public Rectangle(int x, int y)
        {
            this.x = x;
            this.y = y;
            width = 10;
            height = 5;
            rotationAngle = 0;
            rotationCenter.X = this.x + width / 2;
            rotationCenter.Y = this.y + height / 2;
            color = Color.Blue;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int RotationAngle { get; set; }

        public Color Color { get; set; }
    }
}
