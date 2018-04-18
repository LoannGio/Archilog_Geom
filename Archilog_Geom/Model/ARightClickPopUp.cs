using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    abstract class ARightClickPopUp : IRightClickPopUp
    {
        protected IShape myShape;

        public List<String> RightClickPopUpItems { get; } = new List<string>();

        protected ARightClickPopUp()
        {
            RightClickPopUpItems.Add("Edit");
            RightClickPopUpItems.Add("Supprimer");
        }


        public abstract void Edit();

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
            Mediator.Instance.EraseShape(myShape);
        }
    }
}
