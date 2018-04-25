using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Archilog_Geom.Controller;
using Archilog_Geom.Model;

namespace Archilog_Geom
{
    public class ToolBar : IToolBar
    {
        private List<IShape> _toolBarShapes = new List<IShape>();

        public IShape Get(int i)
        {
            if(_toolBarShapes.Count > i)
                return _toolBarShapes[i];
            return null;
        }

        public void RemoveAt(int i)
        {
            if (_toolBarShapes.Count > i)
                _toolBarShapes.RemoveAt(i);
        }

        public void Add(IShape shape)
        {
            _toolBarShapes.Add(shape);
        }

        public object Clone()
        {
            ToolBar clone = new ToolBar();
            foreach (var shape in _toolBarShapes)
            {
                clone._toolBarShapes.Add((IShape)shape.Clone());
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

                _toolBarShapes = fm.LoadToolBar(doc.DocumentElement.FirstChild)._toolBarShapes;
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

            foreach (var shape in _toolBarShapes)
            {
                types.Remove(shape.GetType());
            }

            foreach (Type type  in types)
            {
                IShape shape = (IShape)Activator.CreateInstance(type);
                _toolBarShapes.Add(shape);
            }

        }
    }
}
