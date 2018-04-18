using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public abstract class AObservableShape : IShape
    {

        public int X { get; set; }
        public int Y { get; set; }
        private HashSet<IShapeObserver> listObservers = new HashSet<IShapeObserver>();

        private List<string> RightClickPopUpItems = new List<string>();

        public void Attach(IShapeObserver obs)
        {
            listObservers.Add(obs);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Detach(IShapeObserver obs)
        {
            listObservers.Remove(obs);
        }

        public void Notify()
        {
            foreach (IShapeObserver obs in listObservers)
            {
                obs.Update(this);
            }
        }

        public void SetX(int i)
        {
            X = i;
        }

        public void SetY(int i)
        {
            Y = i;
        }

        public abstract bool Contains(int x, int y);
        public abstract IRightClickPopUp CreateRightClickPopUp();
        public abstract void SetColor(Color c);
    }
}
