﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    class PopUpGroup : ARightClickPopUp
    {
        public PopUpGroup(GroupShapes g)
        {
            _myShape = g;
            RightClickPopUpItems.Insert(1, "Degrouper");
            RightClickPopUpItems.Insert(1, "Grouper");
        }

        public void Group()
        {
            Mediator.Instance.CreateGroup((GroupShapes)_myShape);
        }

        public void Degroup()
        {
            Mediator.Instance.DeleteGroup((GroupShapes) _myShape);
        }

        public override void Handle(int i)
        {
            switch (i)
            {
                case 0:
                    Edit();
                    break;
                case 1:
                    Group();
                    break;
                case 2:
                    Degroup();
                    break;
                case 3:
                    Delete();
                    break;
                default:
                    break;
            }
        }
    }
}
