using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Archilog_Geom.Controller;

namespace Archilog_Geom
{
    public class GroupShapes : AShape
    {
        private List<IShape> _children = new List<IShape>();
        public ReadOnlyCollection<IShape> Children => _children.AsReadOnly();

        public int XMax { get; set; }
        public int YMax { get; set; }

        private Color _color;
        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                foreach (var child in _children)
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
            _children.Add(shape);
            UpdateBounds();
            _color = shape.Color;
        }

        public bool ContainsRecurs(IShape shape)
        {
            bool isContained = false;
            if (Children.Contains(shape))
                isContained = true;
            else
                foreach (var child in Children.Where(c => c.GetType() == typeof(GroupShapes)))
                    if (((GroupShapes) child).ContainsRecurs(shape))
                        isContained = true;

            return isContained;
        }

        public override bool Contains(int x, int y)
        {
            foreach (var shape in _children)
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

        public override XmlNode SerializeXml(XmlDocument doc)
        {
            XmlNode groupShapes = doc.CreateElement("GROUP");

            foreach (var shape in _children)
            {
                groupShapes.AppendChild(shape.SerializeXml(doc));
            }
           
            return groupShapes;
        }

        public override void XmlToShape(XmlNode node)
        {
            try
            {
                foreach (XmlNode shapeNode in node.ChildNodes)
                {
                    IShape shape = null;
                    switch (shapeNode.Name)
                    {
                        case "CIRCLE":
                            shape = new Circle();
                            shape.XmlToShape(shapeNode);
                            break;
                        case "RECTANGLE":
                            shape = new Rectangle();
                            shape.XmlToShape(shapeNode);
                            break;
                        case "GROUP":
                            shape = new GroupShapes();
                            shape.XmlToShape(shapeNode);
                            break;
                    }
                    if(shape != null)
                        Add(shape);
                }
            }
            catch (NullReferenceException)
            {

            }
        }

        public override void Accept(IShapeVisitor v)
        {
            v.VisitGroup(this);
        }

        public void UpdateBounds()
        {
            X = int.MaxValue;
            Y = int.MaxValue;
            XMax = int.MinValue;
            YMax = int.MinValue;
            foreach (var shape in Children)
            {
                if (shape.GetType() == typeof(GroupShapes))
                {
                    ((GroupShapes)shape).UpdateBounds();
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
            foreach (var child in _children)
            {
                clone.Add((IShape)child.Clone());
            }
            return clone;
        }
    }
}
