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
        private int x, y, radius;
        private Color color;

        public Circle(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.radius = 10;
            this.color = Color.Blue;
        }
    }
}
