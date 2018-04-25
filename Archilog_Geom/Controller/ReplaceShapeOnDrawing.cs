using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Controller
{
    public class ReplaceShapeOnDrawing : IShapeVisitor
    {
        private int _mouseX;
        private int _mouseY;
        public ReplaceShapeOnDrawing(int mouseX, int mouseY)
        {
            _mouseX = mouseX;
            _mouseY = mouseY;
        }

        public void VisitCircle(Circle circle)
        {
            circle.Accept(new ReplaceShape(_mouseX - circle.Diameter / 2, _mouseY - circle.Diameter / 2));
        }

        public void VisitRectangle(Rectangle rect)
        {
            rect.Accept(new ReplaceShape(_mouseX - rect.Width / 2, _mouseY - rect.Height / 2));
        }

        public void VisitGroup(GroupShapes group)
        {
            foreach (var shape in group.Children)
            {
                ReplaceGroupOnDrawing(shape, _mouseX, _mouseY, group.X, group.XMax, group.Y, group.YMax);
            }
            group.UpdateBounds();
        }

        private void ReplaceGroupOnDrawing(IShape shape, int x, int y, int xMin, int xMax, int yMin, int yMax)
        {
            if (shape.GetType() == typeof(GroupShapes))
            {
                GroupShapes group = (GroupShapes)shape;
                foreach (var child in group.Children)
                {
                    ReplaceGroupOnDrawing(child, x, y, xMin, xMax, yMin, yMax);
                }
                group.UpdateBounds();
            }
            else
            {
                int width = xMax - xMin;
                int height = yMax - yMin;
                shape.Accept(new ReplaceShape(x + shape.X - xMin - width / 2, y + shape.Y - yMin - height / 2));
            }
        }
    }
}
