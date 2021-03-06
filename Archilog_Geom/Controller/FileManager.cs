﻿using Archilog_Geom.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Archilog_Geom.Controller
{
    public class FileManager
    {
        public void Save(string filename, Memento mem)
        {
            var doc = new XmlDocument();
            XmlNode save = doc.CreateElement("SAVE");

            #region Save Toolbar
            XmlNode toolBar = doc.CreateElement("TOOLBAR");
          
            foreach (var shape in mem.ToolBar.Items())
            {
                toolBar.AppendChild(shape.SerializeXml(doc));
            }
            save.AppendChild(toolBar);
            #endregion


            #region Save DrawnShapes
            XmlNode drawnShapes = doc.CreateElement("DRAWNSHAPES");
            
            foreach (var shape in mem.DrawnShapes)
            {
                drawnShapes.AppendChild(shape.SerializeXml(doc));
            }
            save.AppendChild(drawnShapes);
            #endregion
            doc.AppendChild(save);

           File.WriteAllText(filename, string.Empty);
            File.WriteAllText(filename, doc.InnerXml);
        }

        public Memento Load(string filename)
        {
            var mem = new Memento();
            ToolBar toolBar;
            List<IShape> drawnShapes;
            var doc = new XmlDocument();
            try
            {
                doc.Load(filename);

                toolBar = LoadToolBar(doc.DocumentElement.FirstChild);
                drawnShapes = LoadDrawnShapes(doc.DocumentElement.LastChild);
            }
            catch (Exception)
            {
                return CareTaker.Instance.GetCurrentMemento();
            }

            mem.SetState(drawnShapes, toolBar);
            return mem;
        }

        public ToolBar LoadToolBar(XmlNode toolbarNode)
        {
            var toolbar = new ToolBar();
            foreach (XmlNode node  in toolbarNode.ChildNodes)
            {
                IShape shape = null;
                switch (node.Name)
                {
                    case "CIRCLE":
                        shape = new Circle();
                        break;
                    case "RECTANGLE":
                        shape = new Rectangle();
                        break;
                    case "GROUP":
                        shape = new GroupShapes();
                        break;
                }
                if (shape != null)
                {
                    shape.XmlToShape(node);
                    toolbar.Add(shape);
                }
            }

            return toolbar;
        }

        private List<IShape> LoadDrawnShapes(XmlNode drawnShapesNode)
        {
            var drawnShapes = new List<IShape>();
            foreach (XmlNode node in drawnShapesNode.ChildNodes)
            {
                IShape shape = null;
                switch (node.Name)
                {
                    case "CIRCLE":
                        shape = new Circle();
                        break;
                    case "RECTANGLE":
                        shape = new Rectangle();
                        break;
                    case "GROUP":
                        shape = new GroupShapes();
                        break;
                    default:
                        break;
                }
                if (shape != null)
                {
                    shape.XmlToShape(node);
                    drawnShapes.Add(shape);
                }
            }

            return drawnShapes;
        }
    }
}
