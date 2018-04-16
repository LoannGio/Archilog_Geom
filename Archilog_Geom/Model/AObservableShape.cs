﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public abstract class AObservableShape : IShape
    {
        private HashSet<IShapeObserver> listObservers = new HashSet<IShapeObserver>();

        public void Attach(IShapeObserver obs)
        {
            listObservers.Add(obs);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
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
    }
}
