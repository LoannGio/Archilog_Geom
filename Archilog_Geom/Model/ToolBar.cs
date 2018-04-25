using System;
using System.Collections.Generic;
using System.Xml;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    public class ToolBar : IToolBar
    {
        private List<IShape> _toolBarShapes = new List<IShape>();

        public IShape Get(int i)
        {
            return _toolBarShapes.Count > i ? _toolBarShapes[i] : null;
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

        public List<IShape> Items()
        {
            return _toolBarShapes;
        }

        public object Clone()
        {
            var clone = new ToolBar();
            foreach (var shape in _toolBarShapes)
            {
                clone._toolBarShapes.Add((IShape)shape.Clone());
            }
            return clone;
        }

        public void InitFromFile(string filename)
        {
            var fm = new FileManager();
            var doc = new XmlDocument();
            try
            {
                doc.Load(filename);

                _toolBarShapes = fm.LoadToolBar(doc.DocumentElement.FirstChild)._toolBarShapes;
            }
            catch (Exception)
            {
                // ignored
            }
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

            foreach (var type  in types)
            {
                var shape = (IShape)Activator.CreateInstance(type);
                _toolBarShapes.Add(shape);
            }

        }
    }
}
