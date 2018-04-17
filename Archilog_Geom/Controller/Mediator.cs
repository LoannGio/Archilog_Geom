using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archilog_Geom
{
    class Mediator : IMediator
    {
        private static Mediator _instance;

        public static Mediator Instance => _instance ?? (_instance = new Mediator());


        public List<IShape> DrawnShapes { get; } = new List<IShape>();

        private ToolBar _toolBar = new ToolBar();
        public ToolBar ToolBar => _toolBar;

        private IShape _currentShape;
        public List<IShape> SelectedShapes { get; } = new List<IShape>();
        public bool ClickedOnSelectedShape { get; set; } = false;

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
            bool found = false;
            for (int i = DrawnShapes.Count-1; i >= 0; i--)
            {
                var shape = DrawnShapes[i];
                if (shape.Contains(x, y))
                {
                    found = true;
                    if (!SelectedShapes.Contains(shape))
                        SelectedShapes.Add(shape);
                    else
                        SelectedShapes.Remove(shape);
                    break;
                }
            }
            if(!found)
                SelectedShapes.Clear();
            g.RefreshView();
        }

        public void DrawingPanelMouseDownCalled(int mouseX, int mouseY)
        {
            foreach (var shape in SelectedShapes)
            {
                if (shape.Contains(mouseX, mouseY))
                {
                    ClickedOnSelectedShape = true;
                    break;
                }
            }
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
            foreach (var shape in SelectedShapes)
            {
                if(DrawnShapes.Contains(shape))
                    DrawnShapes.Remove(shape);
            }
            SelectedShapes.Clear();
            g.RefreshView();
            ClickedOnSelectedShape = false;
        }
    }
}
