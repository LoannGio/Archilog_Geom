using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class ToolBar : ICloneable
    {
        public List<IShape> ToolBarShapes { get; } = new List<IShape>();

        public object Clone()
        {
            ToolBar clone = new ToolBar();
            foreach (var shape in ToolBarShapes)
            {
                clone.ToolBarShapes.Add((IShape)shape.Clone());
            }
            return clone;
        }
    }
}
