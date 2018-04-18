using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archilog_Geom
{
    public interface IRightClickPopUp
    {
        void Edit();
        void Delete();
        void Handle(int i);
        List<String> RightClickPopUpItems { get; }
    }
}
