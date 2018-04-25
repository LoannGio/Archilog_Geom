using System.Drawing;
using System.Windows.Forms;
using Archilog_Geom.Controller;

namespace Archilog_Geom.View
{
    public class CsGraphicsDrawShape : IShapeVisitor
    {
        private PaintEventArgs _e;
        private bool _amISelected;
        public CsGraphicsDrawShape(PaintEventArgs e)
        {
            _e = e;
            _amISelected = false;
        }

        public void VisitCircle(Circle circle)
        {
            SolidBrush brush = new SolidBrush(circle.Color);
            _e.Graphics.FillEllipse(brush, circle.X, circle.Y, circle.Diameter, circle.Diameter);
            if (Mediator.SelectedShapes.Contains(circle) || _amISelected)
            {
                _e.Graphics.DrawEllipse(new Pen(Color.Black), circle.X, circle.Y, circle.Diameter, circle.Diameter);
            }
        }

        public void VisitRectangle(Rectangle rect)
        {
            SolidBrush brush = new SolidBrush(rect.Color);
            _e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
            if (Mediator.SelectedShapes.Contains(rect) || _amISelected)
            {
                _e.Graphics.DrawRectangle(new Pen(Color.Black), rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public void VisitGroup(GroupShapes group)
        {
            if(! _amISelected)
                _amISelected = Mediator.SelectedShapes.Contains(group);

            foreach (var shape in group.Children)
            {
                shape.Accept(this);
            }
        }
    }
}
