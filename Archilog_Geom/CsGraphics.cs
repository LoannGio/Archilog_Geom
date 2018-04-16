using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Archilog_Geom
{
    public class CsGraphics : Form, ObservableGraphics
    {
        public CsGraphics()
        {
            Label l = new Label();
            l.Text = "toto";
            l.Show();
            l.Location = new Point(13, 13);
            l.AutoSize = true;
            this.Controls.Add(l);

        }
    }
}