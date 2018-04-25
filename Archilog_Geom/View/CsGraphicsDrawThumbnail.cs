using Archilog_Geom.Controller;
using Archilog_Geom.Model;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Rectangle = Archilog_Geom.Model.Rectangle;

namespace Archilog_Geom.View
{
    class CsGraphicsDrawThumbnail : IShapeVisitor
    {
      
        private readonly Graphics g;
        private readonly int _panelWidth;
        private readonly int _panelHeight;
        private System.Drawing.Rectangle _drawingRect;

        public CsGraphicsDrawThumbnail(Image img, int panelWidth, int panelHeight)
        {
            _panelWidth = panelWidth;
            _panelHeight = panelHeight;
            g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void VisitCircle(Circle circle)
        {
            var b = new SolidBrush(circle.Color);

            _drawingRect = new System.Drawing.Rectangle(circle.X, circle.Y, circle.Diameter, circle.Diameter);
            _drawingRect = ReplaceShape(_drawingRect);

            g.FillEllipse(b, _drawingRect.X, _drawingRect.Y, _drawingRect.Width, _drawingRect.Height);
        }

        public void VisitRectangle(Rectangle rect)
        {
            var b = new SolidBrush(rect.Color);

            _drawingRect = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            _drawingRect = ReplaceShape(_drawingRect);

            g.FillRectangle(b, _drawingRect);
        }

        public void VisitGroup(GroupShapes group)
        {
            var ratioX = (double)_panelWidth / (double)(group.XMax - group.X);
            var ratioY = (double)_panelHeight / (double)(group.YMax - group.Y);
            var ratio = Math.Min(ratioX, ratioY);
            var x = group.X;
            var y = group.Y;

            foreach (var shape in group.Children)
            {
                VisitGroupRecurs(shape, x, y, ratio);
            }
        }

        private void VisitGroupRecurs(IShape shape, int x, int y, double ratio)
        {
            SolidBrush b;
            if (shape.GetType() == typeof(Rectangle))
            {
                var rect = (Rectangle)shape;
                b = new SolidBrush(rect.Color);

                _drawingRect = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                _drawingRect = ReplaceShapeInGroup(_drawingRect, ratio, x, y);
                g.FillRectangle(b, _drawingRect);

            }
            else if (shape.GetType() == typeof(Circle))
            {
                var circle = (Circle)shape;
                b = new SolidBrush(circle.Color);

                _drawingRect = new System.Drawing.Rectangle(circle.X, circle.Y, circle.Diameter, circle.Diameter);
                _drawingRect = ReplaceShapeInGroup(_drawingRect, ratio, x, y);

                g.FillEllipse(b, _drawingRect.X, _drawingRect.Y, _drawingRect.Width, _drawingRect.Height);

            }
            else if (shape.GetType() == typeof(GroupShapes))
            {
                foreach (var child in ((GroupShapes)shape).Children)
                {
                    VisitGroupRecurs(child, x, y, ratio);
                }
            }
        }

        private System.Drawing.Rectangle ReplaceShapeInGroup(System.Drawing.Rectangle drawingRect, double ratio, int xMin, int yMin)
        {
            var newX = (ratio * (drawingRect.X - xMin));
            var newY = (ratio * (drawingRect.Y - yMin));
            var newWidth = (ratio * drawingRect.Width);
            var newHeight = (ratio * drawingRect.Height);

            drawingRect.X = (int)newX;
            drawingRect.Y = (int)newY;
            drawingRect.Width = (int)newWidth;
            drawingRect.Height = (int)newHeight;

            return drawingRect;
        }

        private System.Drawing.Rectangle ReplaceShape(System.Drawing.Rectangle drawingRect)
        {
            while (drawingRect.Width >= _panelWidth || drawingRect.Height >= _panelHeight)
            {
                drawingRect.Width /= 2;
                drawingRect.Height /= 2;
            }

            drawingRect.X = _panelWidth / 2 - drawingRect.Width / 2;
            drawingRect.Y = _panelHeight / 2 - drawingRect.Height / 2;
            return drawingRect;
        }
    }
}
