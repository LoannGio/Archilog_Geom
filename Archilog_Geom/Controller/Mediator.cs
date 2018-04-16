using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archilog_Geom
{
    class Mediator : IMediator
    {
        private List<IShape> _drawnShapes;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IGraphics g = new CsGraphics();
            Application.Run((Form) g);
        }
    }
}
