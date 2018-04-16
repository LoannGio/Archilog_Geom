using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archilog_Geom
{
    class Mediator : IMediator
    {
        [STAThread]
        static void Main()
        {
            //récupérer le form a partir de la Graphics et la créer comme ça
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CsGraphics());
        }
    }
}
