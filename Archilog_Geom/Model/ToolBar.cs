using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    class ToolBar
    {
        private List<IShape> _toolbarShapes = new List<IShape>();
        public List<IShape> ToolBarShapes => _toolbarShapes;

        public ToolBar()
        {
            _toolbarShapes.Add(new Rectangle(10,10));
            _toolbarShapes.Add(new Rectangle(10, 10));
        }
    }
}
