﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    class PopUpRectangle : ARightClickPopUp
    {
        public PopUpRectangle(Rectangle r)
        {
            _myShape = r;
        }
    }
}
