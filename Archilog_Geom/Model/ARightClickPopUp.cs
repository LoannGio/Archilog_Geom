using System.Collections.Generic;
using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    abstract class ARightClickPopUp : IRightClickPopUp
    {
        protected IShape MyShape;

        public List<string> RightClickPopUpItems { get; } = new List<string>();

        protected ARightClickPopUp()
        {
            RightClickPopUpItems.Add("Editer");
            RightClickPopUpItems.Add("Supprimer");
        }


        public void Edit()
        {
            Mediator.Instance.ShapeEditMenu(MyShape);
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
            Mediator.Instance.EraseShape(MyShape);
        }
    }
}
