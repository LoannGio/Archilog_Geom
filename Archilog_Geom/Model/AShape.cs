using System.Drawing;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    public abstract class AShape : IShape
    {
        public int X { get; set; }
        public int Y { get; set; }

        public virtual Color Color { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public abstract bool Contains(int x, int y);
        public abstract IRightClickPopUp CreateRightClickPopUp();
        public abstract XmlNode SerializeXml(XmlDocument doc);
        public abstract void XmlToShape(XmlNode node);
        public abstract void Accept(IShapeVisitor v);
    }
}
