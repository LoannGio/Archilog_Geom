using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archilog_Geom.Controller;
using Archilog_Geom.Model;

namespace Archilog_Geom
{
    public class Mediator : IMediator
    {
        private static Mediator _instance;
        public static Mediator Instance => _instance ?? (_instance = new Mediator());

        public static ToolBar ToolBar { get; private set; } = new ToolBar();

        private IShape _currentShape;
        public static List<IShape> SelectedShapes { get; } = new List<IShape>();
        public static List<IShape> DrawnShapes { get; private set; } = new List<IShape>();
        public static bool ClickedOnSelectedShape { get; set; } = false;

        private static IRightClickPopUp _rightClickPopUp = null;
        public static IRightClickPopUp RightClickPopUp => _rightClickPopUp;

        private static IGraphics g;
        
        private Mediator() { }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolBar.InitFromFile("../../data/init.xml");
            ToolBar.FillShapes();
            CreateMemento();
            g = new CsGraphics();
            Application.Run((Form) g);
            azeaz
                /*
                 * TODO : TU globaux + refactor + attributs a la auber des objets + polygones reguliers 
                 */
        }

        public void LoadCurrentShape(int i)
        {
            _currentShape = (IShape) ToolBar.ToolBarShapes[i].Clone();
        }

        public void DeleteCurrentShapeFromToolBar(int i)
        {
            if (_currentShape != null)
            {
                ToolBar.ToolBarShapes.RemoveAt(i);
            }
            _currentShape = null;
            g.RefreshToolBar();
            CreateMemento();
        }

        public void DrawCurrentShape(int x, int y)
        {
            if (_currentShape != null)
            {
                if (_currentShape.GetType() == typeof(Rectangle))
                {
                    Rectangle r = (Rectangle) _currentShape;
                    _currentShape.X = (x - r.Width/2);
                    _currentShape.Y = (y - r.Height/2);
                }
                else if (_currentShape.GetType() == typeof(Circle))
                {
                    Circle c = (Circle) _currentShape;
                    _currentShape.X = (x - c.Diameter / 2);
                    _currentShape.Y = (y - c.Diameter / 2);
                }
                else if (_currentShape.GetType() == typeof(GroupShapes))
                {
                    GroupShapes group = (GroupShapes) _currentShape;
                    foreach (var shape in group.Children)
                    {
                       ReplaceGroupOnDrawing(shape, x, y, group.X, group.XMax, group.Y, group.YMax);
                    }
                    _currentShape = group;
                }
                DrawnShapes.Add(_currentShape);
                g.RefreshView();
            }
            _currentShape = null;
            CreateMemento();
        }

        private void ReplaceGroupOnDrawing(IShape shape, int x, int y, int xMin, int xMax, int yMin, int yMax)
        {
            if (shape.GetType() == typeof(GroupShapes))
            {
                GroupShapes group = (GroupShapes) shape;
                foreach (var child in group.Children)
                {
                    ReplaceGroupOnDrawing(child, x, y, xMin, xMax, yMin, yMax);
                }
            }
            else
            {
                int width = xMax - xMin;
                int height = yMax - yMin;
                if (shape.GetType() == typeof(Circle))
                {
                    Circle c = (Circle)shape;
                    shape.X = (x + c.X - xMin - width/2);
                    shape.Y = (y + c.Y - yMin - height/2);
                }
                else if (shape.GetType() == typeof(Rectangle))
                {
                    Rectangle r = (Rectangle)shape;
                    shape.X = (x + r.X - xMin - width/2);
                    shape.Y = (y + r.Y - yMin - height/2);
                }
            }
        }

        public void AddRemoveSelectedShape(int x, int y)
        {
            var shape = DrawnShapes.LastOrDefault(s => s.Contains(x, y));
            if (shape == null)
            {
                SelectedShapes.Clear();
            }
            else
            {
                if (!SelectedShapes.Contains(shape))
                    SelectedShapes.Add(shape);
                else
                    SelectedShapes.Remove(shape);
            }     
            g.RefreshView();
        }

        public void DrawingPanelLeftMouseButtonPressed(int mouseX, int mouseY)
        {
            var shape = SelectedShapes.FirstOrDefault(s => s.Contains(mouseX, mouseY));
            if (shape != null)
            {
                ClickedOnSelectedShape = true;
            }
        }

        public void DrawingPanelRightMouseButtonPressed(int mouseX, int mouseY)
        {
            _rightClickPopUp = null;
            bool clickedOnSelectedShape = SelectedShapes.Where(s => s.Contains(mouseX, mouseY)).ToList().Count > 0;
            if (SelectedShapes.Count > 1 && clickedOnSelectedShape)
            {
                GroupShapes tmpSelectedShapes = new GroupShapes();
                foreach (var shape in SelectedShapes)
                {
                    tmpSelectedShapes.Add(shape);                    
                }
                _rightClickPopUp = tmpSelectedShapes.CreateRightClickPopUp();
            }
            else
            {
                var shape = DrawnShapes.LastOrDefault(s => s.Contains(mouseX, mouseY));
                if (shape != null)
                {
                    _rightClickPopUp = shape.CreateRightClickPopUp();
                }
            }
            if(_rightClickPopUp != null)
                g.OpenRightClickPopUp();
        }

        public void SaveShapesInToolbar()
        {
            foreach (var shape in SelectedShapes)
            {
                IShape toolBarNewShape = (IShape)shape.Clone();
                ToolBar.ToolBarShapes.Add(toolBarNewShape);
            }

            ClickedOnSelectedShape = false;
            g.RefreshToolBar();
            CreateMemento();
        }

        public void DeleteSelectedShapes()
        {
            DrawnShapes.RemoveAll(s => SelectedShapes.Contains(s));

            SelectedShapes.Clear();
            g.RefreshView();
            ClickedOnSelectedShape = false;
            CreateMemento();
        }

        public void HandleRightClickMenuItemClick(int i)
        {
            _rightClickPopUp.Handle(i);
        }

        public void CircleEditMenu(Circle c)
        {
            g.OpenCircleEditMenu(c);
        }

        public void RectangleEditMenu(Rectangle r)
        {
            g.OpenRectangleEditMenu(r);
        }

        public void GroupEditMenu(GroupShapes group)
        {
            g.OpenGroupEditMenu(group);
        }

        public void UpdateGroup(GroupShapes group, Color color, int x, int y)
        {
            group.Color = color;
            foreach (var shape in group.Children)
            {
                ReplaceGroupOnEdition(shape, x, y, group.X, group.Y);
            }
            CreateMemento();
        }

        private void ReplaceGroupOnEdition(IShape shape, int x, int y, int xMin, int yMin)
        {
            if (shape.GetType() == typeof(GroupShapes))
            {
                GroupShapes group = (GroupShapes)shape;
                foreach (var child in group.Children)
                {
                    ReplaceGroupOnEdition(child, x, y, xMin, yMin);
                }
            }
            else if (shape.GetType() == typeof(Circle))
            {
                Circle c = (Circle)shape;
                shape.X = (x + c.X - xMin);
                shape.Y = (y + c.Y - yMin);
            }
            else if (shape.GetType() == typeof(Rectangle))
            {
                Rectangle r = (Rectangle)shape;
                shape.X = (x + r.X - xMin);
                shape.Y = (y + r.Y - yMin);
            }
        }

        public void UpdateRectangle(Rectangle r, int x, int y, int width, int height, Color color)
        {
            r.X = x;
            r.Y = y;
            r.Width = width;
            r.Height = height;
            r.Color = color;
            CreateMemento();
        }

        public void UpdateCircle(Circle c, int x, int y, int diameter, Color color)
        {
            c.X = x;
            c.Y = y;
            c.Diameter = diameter;
            c.Color = color;
            CreateMemento();
        }

        public void EraseShape(IShape shape)
        {
            if (DrawnShapes.Contains(shape))
            {
                DrawnShapes.Remove(shape);
            }
            else
            {
                foreach (var child in ((GroupShapes)shape).Children)
                {
                    DrawnShapes.Remove(child);
                }
            }
            g.RefreshView();
            CreateMemento();
        }

        public void CreateGroup(GroupShapes gr)
        {
            foreach (var shape in gr.Children)
            {
                DrawnShapes.Remove(shape);
                SelectedShapes.Remove(shape);
            }
            DrawnShapes.Add(gr);
            SelectedShapes.Add(gr);
            CreateMemento();
        }

        public void DeleteGroup(GroupShapes gr)
        {
            if (DrawnShapes.Contains(gr))
            {
                DrawnShapes.Remove(gr);
                foreach (var shape in gr.Children)
                {
                    DrawnShapes.Add(shape);
                }
                SelectedShapes.Clear();
                g.RefreshView();
            }
            CreateMemento();
        }

        private static void CreateMemento()
        {
            Memento save = new Memento();
            save.SetState(DrawnShapes, ToolBar);
            CareTaker.Instance.Add(save);
        }

        private void RestoreState(Memento newState)
        {
            DrawnShapes.Clear();
            SelectedShapes.Clear();
            foreach (var shape in newState.DrawnShapes)
            {
                DrawnShapes.Add((IShape)shape.Clone());
            }
            ToolBar = (ToolBar)newState.ToolBar.Clone();
            g.RefreshView();
            g.RefreshToolBar();
        }

        public void Undo()
        {
            Memento newState = CareTaker.Instance.Undo();
            RestoreState(newState);
        }

        public void Redo()
        {
            Memento newState = CareTaker.Instance.Redo();
            RestoreState(newState);
        }

        public void Export(string filename)
        {
            FileManager fm = new FileManager();
            if (!filename.ToLower().EndsWith(".xml"))
                filename += ".xml";
            fm.Save(filename, CareTaker.Instance.GetCurrentMemento());
        }

        public void Import(string filename)
        {
            FileManager fm = new FileManager();
            Memento mem = null;
            if (filename.ToLower().EndsWith(".xml"))
                mem = fm.Load(filename);
            if (mem != null && mem != CareTaker.Instance.GetCurrentMemento())
            {
                CareTaker.Instance.ClearMementoes();
                mem.ToolBar.FillShapes();
                CareTaker.Instance.Add(mem);
                RestoreState(mem);
                g.RefreshView();
                g.RefreshToolBar();
            }
        }

        public void SaveBeforeAppClosure()
        {
            string path = "../../data/init.xml";
            Export(path);
        }

        public void ClearDrawingPanel()
        {
            DrawnShapes.Clear();
            SelectedShapes.Clear();
            CreateMemento();
            g.RefreshView();
        }
    }
}
