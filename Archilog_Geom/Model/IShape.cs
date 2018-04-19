using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public interface IShape : ICloneable

    {
        int X { get; set; }
        int Y { get; set; }
        Color Color { get; set; }
        bool Contains(int x, int y);
        IRightClickPopUp CreateRightClickPopUp();
    }
}
