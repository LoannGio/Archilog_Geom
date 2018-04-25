using Archilog_Geom.Model;
using Archilog_Geom.View;

namespace Archilog_Geom.Controller
{
    public class EditMenu : IShapeVisitor
    {
        private readonly IGraphics _g;

        public EditMenu(IGraphics g)
        {
            _g = g;
        }

        public void VisitCircle(Circle circle)
        {
            _g.OpenCircleEditMenu(circle);
        }

        public void VisitRectangle(Rectangle rect)
        {
            _g.OpenRectangleEditMenu(rect);
        }

        public void VisitGroup(GroupShapes group)
        {
            _g.OpenGroupEditMenu(group);
        }
    }
}
