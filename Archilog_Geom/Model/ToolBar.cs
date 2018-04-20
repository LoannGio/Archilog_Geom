using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom
{
    public class ToolBar : ICloneable
    {
        public List<IShape> ToolBarShapes { get; private set; } = new List<IShape>();

        public object Clone()
        {
            ToolBar clone = new ToolBar();
            foreach (var shape in ToolBarShapes)
            {
                clone.ToolBarShapes.Add((IShape)shape.Clone());
            }
            return clone;
        }

        public void InitFromFile(string filename)
        {
            FileManager fm = new FileManager();
            var doc = new XmlDocument();
            try
            {
                doc.Load("../../data/init.xml");

                ToolBarShapes = fm.LoadToolBar(doc.DocumentElement.FirstChild).ToolBarShapes;
            }
            catch (Exception) { }
        }

        public void FillShapes()
        {
            var types = new List<Type>
            {
                typeof(Rectangle),
                typeof(Circle)
            };

            foreach (var shape in ToolBarShapes)
            {
                types.Remove(shape.GetType());
            }

            foreach (Type type  in types)
            {
                IShape shape = (IShape)Activator.CreateInstance(type);
                ToolBarShapes.Add(shape);
            }

        }
    }
}
