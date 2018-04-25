using System;
using System.Collections.Generic;

namespace Archilog_Geom.Model
{
    public interface IToolBar : ICloneable
    {
        IShape Get(int i);
        void RemoveAt(int i);
        void Add(IShape shape);
        List<IShape> Items();
        void InitFromFile(string filename);
        void FillShapes();
    }
}
