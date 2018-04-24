using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Controller
{
    public class UpdateShape : IShapeVisitor
    {
        private object[] _params;

        public UpdateShape(params object[] param)
        {
            _params = param;
        }

        public void VisitCircle(Circle circle)
        {
            int x = (int) _params[0];
            int y = (int)_params[1];
            int diameter = (int)_params[2];
            Color color = (Color) _params[3];

            circle.Accept(new ReplaceShape(x, y));
            circle.Diameter = diameter;
            circle.Color = color;
        }

        public void VisitRectangle(Rectangle rect)
        {
            int x = (int)_params[0];
            int y = (int)_params[1];
            int width = (int)_params[2];
            int height = (int)_params[3];
            Color color = (Color)_params[4];

            rect.Accept(new ReplaceShape(x, y));
            rect.Width = width;
            rect.Height = height;
            rect.Color = color;
        }

        public void VisitGroup(GroupShapes group)
        {
            int x = (int)_params[0];
            int y = (int)_params[1];
            Color color = (Color)_params[2];

            group.Color = color;
            foreach (var shape in group.Children)
            {
                shape.Accept(new ReplaceShape(shape.X + (x - group.X),shape.Y + (y - group.Y)));
            }
            group.UpdateBounds();
        }
    }
}
