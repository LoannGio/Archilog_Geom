using System;
using System.Collections.Generic;
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
    }
}
