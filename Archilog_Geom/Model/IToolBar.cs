using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Model
{
    public interface IToolBar : ICloneable
    {
        IShape Get(int i);
        void RemoveAt(int i);
        void Add(IShape shape);
        void InitFromFile(string filename);
        void FillShapes();
    }
}
