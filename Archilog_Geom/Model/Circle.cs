using System;
using System.Drawing;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
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
            var circleCenterX = X + Diameter/2;
            var circleCenterY = Y + Diameter/2;

            return EuclideanDistance(circleCenterX, circleCenterY, x, y) <= Diameter/2;
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
                var x = node.ChildNodes.Item(0);
                var y = node.ChildNodes.Item(1);
                var diameter = node.ChildNodes.Item(2);
                var color = node.ChildNodes.Item(3);

                X = int.Parse(x.InnerText);
                Y = int.Parse(y.InnerText);
                Diameter = int.Parse(diameter.InnerText);
                Color = Color.FromName(color.InnerText.Substring(color.InnerText.IndexOf("[")+1, color.InnerText.IndexOf("]") - color.InnerText.IndexOf("[") -1));
            }
            catch (NullReferenceException)
            {

            }
        }

        public override void Accept(IShapeVisitor v)
        {
            v.VisitCircle(this);
        }

        private int EuclideanDistance(int x1, int y1, int x2, int y2)
        {
            double a = x2 - x1;
            double b = y2 - y1;

            return (int)Math.Sqrt(a * a + b * b);
        }
    }
}
