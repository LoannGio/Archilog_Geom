using System;
using System.Drawing;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    public interface IShape : ICloneable
    {
        int X { get; set; }
        int Y { get; set; }
        Color Color { get; set; }
        bool Contains(int x, int y);
        IRightClickPopUp CreateRightClickPopUp();
        XmlNode SerializeXml(XmlDocument doc);
        void XmlToShape(XmlNode node);
        void Accept(IShapeVisitor v);
    }
}
