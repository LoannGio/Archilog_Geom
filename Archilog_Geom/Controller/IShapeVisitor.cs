using Archilog_Geom.Model;

namespace Archilog_Geom.Controller
{
    public interface IShapeVisitor
    {
        void VisitCircle(Circle circle);
        void VisitRectangle(Rectangle rect);
        void VisitGroup(GroupShapes group);
    }
}
