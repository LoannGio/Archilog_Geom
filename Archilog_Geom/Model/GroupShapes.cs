using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class GroupShapes : AObservableShape
    {
        private List<IShape> children = new List<IShape>();

        public void Add(IShape shape)
        {
            children.Add(shape);
        }

        public void Remove(IShape shape)
        {
            children.Add(shape);
        }

        public IShape GetChild(int i)
        {
            return children.ElementAt(i);
        }
    }
}
