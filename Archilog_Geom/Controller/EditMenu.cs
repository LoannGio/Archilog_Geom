using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Controller
{
    public class EditMenu : IShapeVisitor
    {
        private IGraphics _g;

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
