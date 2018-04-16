using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archilog_Geom
{
    class Mediator : IMediator
    {
        private static Mediator _instance;

        public static Mediator Instance => _instance ?? (_instance = new Mediator());

        private List<IShape> _drawnShapes;
        private ToolBar _toolBar = new ToolBar();
        public ToolBar ToolBar => _toolBar;
        private static IGraphics g;
        
        private Mediator() { }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            g = new CsGraphics();
            Application.Run((Form) g);

        }
    }
}
