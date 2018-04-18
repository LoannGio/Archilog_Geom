using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public class GroupShapes : AObservableShape
    {
        public List<IShape> Children { get; } = new List<IShape>();
        public Color Color => System.Drawing.Color.Black;

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
            foreach (var shape in Children)
            {
                if (shape.Contains(x, y))
                    return true;
            }

            return false;
        }

        public override IRightClickPopUp CreateRightClickPopUp()
        {
            return new PopUpGroup(this);
        }

        public override void SetColor(Color c)
        {
            foreach (var shape in Children)
            {
                shape.SetColor(c);
            }
        }
    }
}
