using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Archilog_Geom.Model;
using Archilog_Geom.View;
using Rectangle = Archilog_Geom.Model.Rectangle;
using ToolBar = Archilog_Geom.Model.ToolBar;

namespace Archilog_Geom.Controller
{
    public class Mediator : IMediator
    {
        private static Mediator _instance;
        public static Mediator Instance => _instance ?? (_instance = new Mediator());

        public ToolBar ToolBar { get; private set; } = new ToolBar();

        private IShape _currentShape;
        public List<IShape> SelectedShapes { get; } = new List<IShape>();
        public List<IShape> DrawnShapes { get; } = new List<IShape>();
        public bool ClickedOnSelectedShape { get; set; }

        public IRightClickPopUp RightClickPopUp { get; private set; }

        //default graphic lib
        private static IGraphics _g;
        
        private Mediator() { }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Instance.ToolBar.InitFromFile("../../data/init.xml");
            Instance.ToolBar.FillShapes();
            Instance.CreateMemento();

            _g = new CsGraphics();
            Application.Run((Form) _g);
        }

        public void LoadCurrentShape(int i)
        {
            _currentShape = (IShape) ToolBar.Get(i).Clone();
        }

        public void DeleteCurrentShapeFromToolBar(int i)
        {
            if (_currentShape != null)
            {
                ToolBar.RemoveAt(i);
                _currentShape = null;
                _g.RefreshToolBar();
                CreateMemento();
            }
        }

        public void DrawCurrentShape(int x, int y)
        {
            if (_currentShape != null)
            {
                _currentShape.Accept(new ReplaceShapeOnDrawing(x, y));
                DrawnShapes.Add(_currentShape);
                _g.RefreshView();
                _currentShape = null;
                CreateMemento();
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
            _g.RefreshView();
        }

        public void DrawingPanelLeftMouseButtonPressed(int mouseX, int mouseY)
        {
            var shape = SelectedShapes.First(s => s.Contains(mouseX, mouseY));
            if (shape != null)
            {
                ClickedOnSelectedShape = true;
            }
        }

        public void DrawingPanelRightMouseButtonPressed(int mouseX, int mouseY)
        {
            RightClickPopUp = null;
            var clickedOnSelectedShape = SelectedShapes.Where(s => s.Contains(mouseX, mouseY)).ToList().Count > 0;
            if (SelectedShapes.Count > 1 && clickedOnSelectedShape)
            {
                var tmpSelectedShapes = new GroupShapes();
                foreach (var shape in SelectedShapes)
                {
                    tmpSelectedShapes.Add(shape);                    
                }
                RightClickPopUp = tmpSelectedShapes.CreateRightClickPopUp();
            }
            else
            {
                var shape = DrawnShapes.LastOrDefault(s => s.Contains(mouseX, mouseY));
                if (shape != null)
                {
                    RightClickPopUp = shape.CreateRightClickPopUp();
                }
            }
            if(RightClickPopUp != null)
                _g.OpenRightClickPopUp();
        }

        public void SaveShapesInToolbar()
        {
            foreach (var shape in SelectedShapes)
            {
                var toolBarNewShape = (IShape)shape.Clone();
                ToolBar.Add(toolBarNewShape);
            }

            ClickedOnSelectedShape = false;
            _g.RefreshToolBar();
            CreateMemento();
        }

        public void DeleteSelectedShapes()
        {
            DrawnShapes.RemoveAll(s => SelectedShapes.Contains(s));

            SelectedShapes.Clear();
            _g.RefreshView();
            ClickedOnSelectedShape = false;
            CreateMemento();
        }

        public void HandleRightClickMenuItemClick(int i)
        {
            RightClickPopUp.Handle(i);
        }

        public void ShapeEditMenu(IShape shape)
        {
            shape.Accept(new EditMenu(_g));
        }

        public void UpdateGroup(GroupShapes group, Color color, int x, int y)
        {
            group.Accept(new UpdateShape(x, y, color));
            CreateMemento();
        }

        public void UpdateRectangle(Rectangle r, int x, int y, int width, int height, Color color)
        {
            r.Accept(new UpdateShape(x, y, width, height, color));
            CreateMemento();
        }

        public void UpdateCircle(Circle c, int x, int y, int diameter, Color color)
        {
            c.Accept(new UpdateShape(x, y, diameter, color));
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
            _g.RefreshView();
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
                _g.RefreshView();
            }
            CreateMemento();
        }

        public void CreateMemento()
        {
            var save = new Memento();
            save.SetState(DrawnShapes, ToolBar);
            CareTaker.Instance.Add(save);
        }

        public void RestoreState(Memento newState)
        {
            DrawnShapes.Clear();
            SelectedShapes.Clear();
            foreach (var shape in newState.DrawnShapes)
                DrawnShapes.Add((IShape) shape.Clone());
            ToolBar = (ToolBar)newState.ToolBar.Clone();
            _g.RefreshView();
            _g.RefreshToolBar();
        }

        public void Undo()
        {
            var newState = CareTaker.Instance.Undo();
            RestoreState(newState);
        }

        public void Redo()
        {
            var newState = CareTaker.Instance.Redo();
            RestoreState(newState);
        }

        public void Export(string filename)
        {
            var fm = new FileManager();
            if (!filename.ToLower().EndsWith(".xml"))
                filename += ".xml";
            fm.Save(filename, CareTaker.Instance.GetCurrentMemento());
        }

        public void Import(string filename)
        {
            var fm = new FileManager();
            Memento mem = null;
            if (filename.ToLower().EndsWith(".xml"))
                mem = fm.Load(filename);
            if (mem != null && mem != CareTaker.Instance.GetCurrentMemento())
            {
                CareTaker.Instance.ClearMementoes();
                mem.ToolBar.FillShapes();
                CareTaker.Instance.Add(mem);
                RestoreState(mem);
                _g.RefreshView();
                _g.RefreshToolBar();
            }
        }

        public void SaveBeforeAppClosure()
        {
            var path = "../../data/init.xml";
            Export(path);
        }

        public void ClearDrawingPanel()
        {
            DrawnShapes.Clear();
            SelectedShapes.Clear();
            CreateMemento();
            _g.RefreshView();
        }
    }
}
