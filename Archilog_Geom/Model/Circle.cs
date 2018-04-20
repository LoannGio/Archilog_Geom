using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archilog_Geom
{
    public class Circle : AShape
    {
        public int Diameter { get; set; }

        public Circle(int x, int y, int diameter, Color color)
        {
            X = x;
            Y = y;
            Diameter = diameter;
            Color = color;
        }

        public Circle()
        {
            X = 20;
            Y = 20;
            Diameter = 100;
            Color = Color.Red;
        }

        public override bool Contains(int x, int y)
        {
            int circleCenterX = X + Diameter/2;
            int circleCenterY = Y + Diameter/2;

            if (EuclideanDistance(circleCenterX, circleCenterY, x, y) <= Diameter/2)
                return true;

            return false;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
            return new PopUpCircle(this);
        }

        public override XmlNode SerializeXml(XmlDocument doc)
        {
            XmlNode circle = doc.CreateElement("CIRCLE");

            XmlNode x = doc.CreateElement("X");
            x.AppendChild(doc.CreateTextNode(this.X.ToString()));
            XmlNode y = doc.CreateElement("Y");
            y.AppendChild(doc.CreateTextNode(this.Y.ToString()));
            XmlNode diameter = doc.CreateElement("DIAMETER");
            diameter.AppendChild(doc.CreateTextNode(this.Diameter.ToString()));
            XmlNode color = doc.CreateElement("COLOR");
            color.AppendChild(doc.CreateTextNode(this.Color.ToString()));

            circle.AppendChild(x);
            circle.AppendChild(y);
            circle.AppendChild(diameter);
            circle.AppendChild(color);

            return circle;
        }

        public override void XmlToShape(XmlNode node)
        {
            try
            {
                XmlNode x = node.ChildNodes.Item(0);
                XmlNode y = node.ChildNodes.Item(1);
                XmlNode diameter = node.ChildNodes.Item(2);
                XmlNode color = node.ChildNodes.Item(3);

                X = Int32.Parse(x.InnerText);
                Y = Int32.Parse(y.InnerText);
                Diameter = Int32.Parse(diameter.InnerText);
                Color = Color.FromName(color.InnerText.Substring(color.InnerText.IndexOf("[")+1, color.InnerText.IndexOf("]") - color.InnerText.IndexOf("[") -1));
            }
            catch (NullReferenceException)
            {

            }
        }

        private int EuclideanDistance(int x1, int y1, int x2, int y2)
        {
            double a = x2 - x1;
            double b = y2 - y1;

            return (int)Math.Sqrt(a * a + b * b);
        }
    }
}
