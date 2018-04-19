using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom.Model
{
    public class CareTaker 
    {
        private static CareTaker _instance;
        public static CareTaker Instance => _instance ?? (_instance = new CareTaker());

        private List<Memento> _listMementoes = new List<Memento>();
        private int _cursor = -1;

        private CareTaker() { }

        public void Add(Memento mem)
        {
            if (_cursor < _listMementoes.Count - 1)
            {
                _listMementoes.RemoveRange(_cursor+1, _listMementoes.Count - 1 - _cursor);
            }
            _listMementoes.Add(mem);
            _cursor++;
        }

        public Memento Undo()
        {
            if (_cursor > 0)
                _cursor--;
            return _listMementoes[_cursor];
        }

        public Memento Redo()
        {
            if (_cursor < _listMementoes.Count - 1)
                _cursor++;
            return _listMementoes[_cursor];
        }
    }
}
