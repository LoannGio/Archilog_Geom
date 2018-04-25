using Archilog_Geom.Controller;

namespace Archilog_Geom.Model
{
    class PopUpGroup : ARightClickPopUp
    {
        public PopUpGroup(GroupShapes g)
        {
            MyShape = g;
            RightClickPopUpItems.Insert(1, "Degrouper");
            RightClickPopUpItems.Insert(1, "Grouper");
        }

        public void Group()
        {
            Mediator.Instance.CreateGroup((GroupShapes)MyShape);
        }

        public void Degroup()
        {
            Mediator.Instance.DeleteGroup((GroupShapes) MyShape);
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
