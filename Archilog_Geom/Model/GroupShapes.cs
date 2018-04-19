using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Archilog_Geom
{
    public class GroupShapes : AShape
    {
        public List<IShape> Children { get; } = new List<IShape>();

        public int XMax { get; set; }
        public int YMax { get; set; }

        private Color _color;
        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                foreach (var child in Children)
                {
                    child.Color = value;
                }
            }
        }

        public GroupShapes()
        {
            Color = Color.Blue;
            X = int.MaxValue;
            Y = int.MaxValue;
            XMax = int.MinValue;
            YMax = int.MinValue;
        }

        public void Add(IShape shape)
        {
            Children.Add(shape);
            UpdateBounds(this);
            _color = shape.Color;
        }

        public override bool Contains(int x, int y)
        {
            foreach (var shape in Children)
            {
                if (shape.Contains(x, y))
                    return true;
            }

            return false;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
            return new PopUpGroup(this);
        }

        private void UpdateBounds(GroupShapes group)
        {
            foreach (var shape in group.Children)
            {
                if (shape.GetType() == typeof(GroupShapes))
                {
                    UpdateBounds((GroupShapes)shape);
                }
                else if (shape.GetType() == typeof(Circle))
                {
                    Circle c = (Circle)shape;
                    UpdateMinMax(c.X, c.Y, c.Diameter, c.Diameter);

                }
                else if (shape.GetType() == typeof(Rectangle))
                {
                    Rectangle r = (Rectangle)shape;
                    UpdateMinMax(r.X, r.Y, r.Width, r.Height);
                }
            }
        }

        private void UpdateMinMax(int x, int y, int width, int height)
        {
            if (x < X)
                X = x;
            if (x + width > XMax)
                XMax = x + width;
            if (y < Y)
                Y = y;
            if (y + height > YMax)
                YMax = y + height;
        }

        public override object Clone()
        {
            GroupShapes clone = new GroupShapes();
            foreach (var child in Children)
            {
                clone.Add((IShape)child.Clone());
            }
            return clone;
        }
    }
}
