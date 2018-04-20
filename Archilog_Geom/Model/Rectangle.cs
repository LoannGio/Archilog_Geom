using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archilog_Geom
{
    public class Rectangle : AShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int RotationAngle { get; set; }
        private Point rotationCenter;

        public Rectangle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RotationAngle = 0;
            rotationCenter.X = X + Width / 2;
            rotationCenter.Y = Y + Height / 2;
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
            if (x >= X && x <= X + Width && y >= Y && y <= Y + Height)
                return true;
            return false;
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
                XmlNode x = node.ChildNodes.Item(0);
                XmlNode y = node.ChildNodes.Item(1);
                XmlNode width = node.ChildNodes.Item(2);
                XmlNode height = node.ChildNodes.Item(3);
                XmlNode color = node.ChildNodes.Item(4);

                X = Int32.Parse(x.InnerText);
                Y = Int32.Parse(y.InnerText);
                Width = Int32.Parse(width.InnerText);
                Height = Int32.Parse(height.InnerText);
                Color = Color.FromName(color.InnerText.Substring(color.InnerText.IndexOf("[") + 1, color.InnerText.IndexOf("]") - color.InnerText.IndexOf("[") - 1));
            }
            catch (NullReferenceException)
            {

            }
        }
    }
}
