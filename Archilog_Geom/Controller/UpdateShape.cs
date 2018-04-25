using Archilog_Geom.Model;
using System.Drawing;
using Rectangle = Archilog_Geom.Model.Rectangle;

namespace Archilog_Geom.Controller
{
    public class UpdateShape : IShapeVisitor
    {
        private readonly object[] _params;

        public UpdateShape(params object[] param)
        {
            _params = param;
        }

        public void VisitCircle(Circle circle)
        {
            var x = (int) _params[0];
            var y = (int)_params[1];
            var diameter = (int)_params[2];
            var color = (Color) _params[3];

            circle.Accept(new ReplaceShape(x, y));
            circle.Diameter = diameter;
            circle.Color = color;
        }

        public void VisitRectangle(Rectangle rect)
        {
            var x = (int)_params[0];
            var y = (int)_params[1];
            var width = (int)_params[2];
            var height = (int)_params[3];
            var color = (Color)_params[4];

            rect.Accept(new ReplaceShape(x, y));
            rect.Width = width;
            rect.Height = height;
            rect.Color = color;
        }

        public void VisitGroup(GroupShapes group)
        {
            var x = (int)_params[0];
            var y = (int)_params[1];
            var color = (Color)_params[2];

            group.Color = color;
            foreach (var shape in group.Children)
            {
                shape.Accept(new ReplaceShape(shape.X + (x - group.X),shape.Y + (y - group.Y)));
            }
            group.UpdateBounds();
        }
    }
}
