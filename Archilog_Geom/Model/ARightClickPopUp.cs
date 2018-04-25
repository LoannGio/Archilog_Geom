using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    abstract class ARightClickPopUp : IRightClickPopUp
    {
        protected IShape _myShape;

        public List<string> RightClickPopUpItems { get; } = new List<string>();

        protected ARightClickPopUp()
        {
            RightClickPopUpItems.Add("Editer");
            RightClickPopUpItems.Add("Supprimer");
        }


        public void Edit()
        {
            Mediator.Instance.ShapeEditMenu(_myShape);
        }

        public virtual void Handle(int i)
        {
            switch (i)
            {
                case 0:
                    Edit();
                    break;
                case 1:
                    Delete();
                    break;
                default:
                    break;
            }
        }

        public void Delete()
        {
            Mediator.Instance.EraseShape(_myShape);
        }
    }
}
