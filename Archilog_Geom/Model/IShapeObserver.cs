using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public interface IShapeObserver
    {
        void Update(IShape shape);
    }
}
