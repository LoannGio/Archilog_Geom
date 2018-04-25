using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    class PopUpCircle : ARightClickPopUp
    {
        public PopUpCircle(Circle c)
        {
            _myShape = c;
        }
    }
}
