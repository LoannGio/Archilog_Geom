using System;
using System.Drawing;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    public class Rectangle : AShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
        }

        public Rectangle()
        {
            X = 10;
            Y = 10;
            Width = 50;
            Height = 50;
            Color = Color.LightGreen;
        }

        public override bool Contains(int x, int y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
           return new PopUpRectangle(this);
        }

        public override XmlNode SerializeXml(XmlDocument doc)
        {
            XmlNode rectangle = doc.CreateElement("RECTANGLE");

            XmlNode x = doc.CreateElement("X");
            x.AppendChild(doc.CreateTextNode(this.X.ToString()));
            XmlNode y = doc.CreateElement("Y");
            y.AppendChild(doc.CreateTextNode(this.Y.ToString()));
            XmlNode width = doc.CreateElement("WIDTH");
            width.AppendChild(doc.CreateTextNode(this.Width.ToString()));
            XmlNode height = doc.CreateElement("HEIGHT");
            height.AppendChild(doc.CreateTextNode(this.Height.ToString()));
            XmlNode color = doc.CreateElement("COLOR");
            color.AppendChild(doc.CreateTextNode(this.Color.ToString()));

            rectangle.AppendChild(x);
            rectangle.AppendChild(y);
            rectangle.AppendChild(width);
            rectangle.AppendChild(height);
            rectangle.AppendChild(color);

            return rectangle;
        }

        public override void XmlToShape(XmlNode node)
        {
            try
            {
                var x = node.ChildNodes.Item(0);
                var y = node.ChildNodes.Item(1);
                var width = node.ChildNodes.Item(2);
                var height = node.ChildNodes.Item(3);
                var color = node.ChildNodes.Item(4);

                X = int.Parse(x.InnerText);
                Y = int.Parse(y.InnerText);
                Width = int.Parse(width.InnerText);
                Height = int.Parse(height.InnerText);
                Color = Color.FromName(color.InnerText.Substring(color.InnerText.IndexOf("[") + 1, color.InnerText.IndexOf("]") - color.InnerText.IndexOf("[") - 1));
            }
            catch (NullReferenceException)
            {

            }
        }

        public override void Accept(IShapeVisitor v)
        {
            v.VisitRectangle(this);
        }
    }
}
