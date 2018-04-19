using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Model
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
