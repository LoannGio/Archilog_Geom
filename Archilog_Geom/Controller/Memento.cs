using System.Collections.Generic;
using Archilog_Geom.Model;

namespace Archilog_Geom.Controller
{
    public class Memento
    {
        public List<IShape> DrawnShapes { get; } = new List<IShape>();
        public ToolBar ToolBar { get; private set; } = new ToolBar();

        public void SetState(List<IShape> drawnShapes, ToolBar toolbar)
        {
            DrawnShapes.Clear();
            foreach (var shape in drawnShapes)
            {
                DrawnShapes.Add((IShape)shape.Clone());
            }

            ToolBar = (ToolBar) toolbar.Clone();
        }
    }
}
