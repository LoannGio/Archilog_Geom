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
            width = 5;
            height = 10;
            rotationAngle = 0;
            rotationCenter.X = this.x + width / 2;
            rotationCenter.Y = this.y + height / 2;
            color = Color.Blue;
        }
    }
}
