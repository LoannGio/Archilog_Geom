using Archilog_Geom.Model;

namespace Archilog_Geom.Controller
{
    class ReplaceShape : IShapeVisitor
    {
        private readonly int _x;
        private readonly int _y;

        public ReplaceShape(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void VisitCircle(Circle circle)
        {
            circle.X = _x;
            circle.Y = _y;
        }

        public void VisitRectangle(Rectangle rect)
        {
            rect.X = _x;
            rect.Y = _y;
        }

        public void VisitGroup(GroupShapes group)
        {
            group.X = _x;
            group.Y = _y;
        }
    }
}
