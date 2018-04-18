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
        public int Width { get; set; }
        public int Height { get; set; }
        public int RotationAngle { get; set; }
        private Point rotationCenter;
        private Color _color;
        public Color Color => _color;

        public Rectangle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RotationAngle = 0;
            rotationCenter.X = X + Width / 2;
            rotationCenter.Y = Y + Height / 2;
            _color = color;
        }

        public override bool Contains(int x, int y)
        {
            if (x >= X && x <= X + Width && y >= Y && y <= Y + Height)
                return true;
            return false;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
           return new PopUpRectangle(this);
        }

        public override void SetColor(Color c)
        {
            _color = c;
        }
    }
}
