using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archilog_Geom
{
    class Mediator : IMediator
    {
        private static Mediator _instance;
        public static Mediator Instance => _instance ?? (_instance = new Mediator());
        
        private ToolBar _toolBar = new ToolBar();
        public ToolBar ToolBar => _toolBar;

        private IShape _currentShape;
        public List<IShape> SelectedShapes { get; } = new List<IShape>();
        public List<IShape> DrawnShapes { get; } = new List<IShape>();
        public bool ClickedOnSelectedShape { get; set; } = false;

        private IRightClickPopUp _rightClickPopUp = null;
        public IRightClickPopUp RightClickPopUp => _rightClickPopUp;

        private static IGraphics g;
        
        private Mediator() { }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            g = new CsGraphics();
            Application.Run((Form) g);

        }

        public void AddShape(IShape shape)
        {
            DrawnShapes.Add(shape);
        }

        public void LoadCurrentShape(int i)
        {
            _currentShape = (IShape) _toolBar.ToolBarShapes[i].Clone();
        }

        public void DeleteCurrentShapeFromToolBar(int i)
        {
            if (_currentShape != null)
            {
                _toolBar.ToolBarShapes.RemoveAt(i);
            }
            _currentShape = null;
            g.RefreshToolBar();
        }

        public void DrawCurrentShape(int x, int y)
        {
            if (_currentShape != null)
            {
                if (_currentShape.GetType() == typeof(Rectangle))
                {
                    Rectangle r = (Rectangle) _currentShape;
                    _currentShape.SetX(x - r.Width/2);
                    _currentShape.SetY(y - r.Height/2);
                }
                else if (_currentShape.GetType() == typeof(Circle))
                {
                    Circle c = (Circle) _currentShape;
                    _currentShape.SetX(x - c.Diameter / 2);
                    _currentShape.SetY(y - c.Diameter / 2);
                }
                DrawnShapes.Add(_currentShape);
                g.RefreshView();
            }

            _currentShape = null;
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
                _toolBar.ToolBarShapes.Add(toolBarNewShape);
            }

            ClickedOnSelectedShape = false;
            g.RefreshToolBar();
        }

        public void DeleteSelectedShapes()
        {
            DrawnShapes.RemoveAll(s => SelectedShapes.Contains(s));

            SelectedShapes.Clear();
            g.RefreshView();
            ClickedOnSelectedShape = false;
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

        public void UpdateGroup(GroupShapes g, Color color)
        {
            g.SetColor(color);
        }

        public void UpdateRectangle(Rectangle r, int x, int y, int width, int height, Color color)
        {
            r.X = x;
            r.Y = y;
            r.Width = width;
            r.Height = height;
            r.SetColor(color);
        }

        public void UpdateCircle(Circle c, int x, int y, int diameter, Color color)
        {
            c.X = x;
            c.Y = y;
            c.Diameter = diameter;
            c.SetColor(color);
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
        }
    }
}
