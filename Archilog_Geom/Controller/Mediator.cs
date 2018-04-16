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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Graphics g = new CsGraphics();
            Application.Run((Form) g);
            string s;
        }
    }
}
