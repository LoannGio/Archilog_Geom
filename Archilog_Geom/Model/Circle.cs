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
        public int Diameter { get; set; }
        private Color _color;
        public Color Color => _color;

        public Circle(int x, int y, int diameter, Color color)
        {
            X = x;
            Y = y;
            Diameter = diameter;
            _color = color;
        }


        public override bool Contains(int x, int y)
        {
            int circleCenterX = X + Diameter/2;
            int circleCenterY = Y + Diameter/2;

            if (EuclideanDistance(circleCenterX, circleCenterY, x, y) <= Diameter/2)
                return true;

            return false;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
            return new PopUpCircle(this);
        }

        public override void SetColor(Color c)
        {
            _color = c;
        }

        private int EuclideanDistance(int x1, int y1, int x2, int y2)
        {
            double a = x2 - x1;
            double b = y2 - y1;

            return (int)Math.Sqrt(a * a + b * b);
        }

    }
}
