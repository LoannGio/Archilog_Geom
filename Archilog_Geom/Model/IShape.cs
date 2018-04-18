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
        void Attach(IShapeObserver obs);
        void Detach(IShapeObserver obs);
        void Notify();
        void SetX(int i);
        void SetY(int i);
        bool Contains(int x, int y);
        IRightClickPopUp CreateRightClickPopUp();
        void SetColor(Color c);
    }
}
