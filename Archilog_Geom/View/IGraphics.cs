﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archilog_Geom
{
    public interface IGraphics
    {

        void InitializeToolBar();
        void RefreshView();
        void RefreshToolBar();
        void OpenRightClickPopUp();
        void OpenCircleEditMenu(Circle c);
        void OpenRectangleEditMenu(Rectangle r);
        void OpenGroupEditMenu(GroupShapes g);
    }
}