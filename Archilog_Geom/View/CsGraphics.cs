using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Archilog_Geom
{
    public class CsGraphics : Form, IObservableGraphics
    {
        private Panel _drawingPanel;
        private Panel _toolbarPanel;

        public CsGraphics()
        {
          InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._drawingPanel = new System.Windows.Forms.Panel();
            this._toolbarPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DrawingPanel
            // 
            this._drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._drawingPanel.Location = new System.Drawing.Point(153, 12);
            this._drawingPanel.Name = "_drawingPanel";
            this._drawingPanel.Size = new System.Drawing.Size(671, 478);
            this._drawingPanel.TabIndex = 0;
            // 
            // ToolbarPanel
            // 
            this._toolbarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolbarPanel.Location = new System.Drawing.Point(1, 12);
            this._toolbarPanel.Name = "_toolbarPanel";
            this._toolbarPanel.Size = new System.Drawing.Size(146, 478);
            this._toolbarPanel.TabIndex = 1;
            // 
            // CsGraphics
            // 
            this.ClientSize = new System.Drawing.Size(836, 493);
            this.Controls.Add(this._toolbarPanel);
            this.Controls.Add(this._drawingPanel);
            this.Name = "CsGraphics";
            this.Text = "Archilog Geom";
            this.ResumeLayout(false);

        }

        public void UpdateToolbarOnView(List<IShape> toolBarShapes)
        {
            throw new NotImplementedException();
        }
    }
}