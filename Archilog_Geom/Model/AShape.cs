using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public abstract class AShape : IShape
    {

        public int X { get; set; }
        public int Y { get; set; }

        private List<string> RightClickPopUpItems = new List<string>();

        public virtual Color Color { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public abstract bool Contains(int x, int y);
        public abstract IRightClickPopUp CreateRightClickPopUp();
    }
}
