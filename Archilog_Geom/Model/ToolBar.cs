using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    class ToolBar
    {
        public List<IShape> ToolBarShapes { get; } = new List<IShape>();

        public ToolBar()
        {
            ToolBarShapes.Add(new Rectangle(10, 10, 50, 50, Color.LightGreen));
            ToolBarShapes.Add(new Circle(20, 20, 100, Color.Red));
        }
    }
}
