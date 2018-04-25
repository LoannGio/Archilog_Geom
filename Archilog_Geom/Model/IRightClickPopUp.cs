using System.Collections.Generic;

namespace Archilog_Geom.Model
{
    public interface IRightClickPopUp
    {
        void Edit();
        void Delete();
        void Handle(int i);
        List<string> RightClickPopUpItems { get; }
    }
}
