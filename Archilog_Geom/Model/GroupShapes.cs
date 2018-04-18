using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class GroupShapes : AObservableShape
    {
        public List<IShape> Children { get; } = new List<IShape>();

        public void Add(IShape shape)
        {
            Children.Add(shape);
        }

        public void Remove(IShape shape)
        {
            Children.Add(shape);
        }

        public override bool Contains(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
