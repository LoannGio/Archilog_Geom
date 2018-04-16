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
        private Panel toolBarPanel;
        private Panel _drawingPanel;

        public CsGraphics()
        {
          InitializeComponent();
            InitializeToolBar();
       
        }

        public void InitializeToolBar()
        {
            var shapes = Mediator.Instance.ToolBar.ToolBarShapes;
            for (int i = 0; i < shapes.Count; i++)
            {
                #region create the sub panel
                Panel p = new Panel();
                int itemHeight = 100;
                int itemXposition = 0;
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Location = new Point(itemXposition, i*itemHeight);
                p.Size = new Size(toolBarPanel.Width, itemHeight);
                #endregion

                #region draw shape on the sub panel

                Graphics g = p.CreateGraphics();
                g.DrawRectangle(new Pen(Color.Blue), 10,10,50,50);
                #endregion

                toolBarPanel.Controls.Add(p);
                //toolBarPanel.Refresh();
            }

        }

        private void InitializeComponent()
        {
            this._drawingPanel = new System.Windows.Forms.Panel();
            this.toolBarPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _drawingPanel
            // 
            this._drawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._drawingPanel.Location = new System.Drawing.Point(153, 31);
            this._drawingPanel.Name = "_drawingPanel";
            this._drawingPanel.Size = new System.Drawing.Size(671, 459);
            this._drawingPanel.TabIndex = 0;
            // 
            // toolBarPanel
            // 
            this.toolBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.toolBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBarPanel.Location = new System.Drawing.Point(13, 31);
            this.toolBarPanel.Name = "toolBarPanel";
            this.toolBarPanel.Size = new System.Drawing.Size(134, 459);
            this.toolBarPanel.TabIndex = 1;
            // 
            // CsGraphics
            // 
            this.ClientSize = new System.Drawing.Size(836, 493);
            this.Controls.Add(this.toolBarPanel);
            this.Controls.Add(this._drawingPanel);
            this.Name = "CsGraphics";
            this.Text = "Archilog Geom";
            this.ResumeLayout(false);

        }

    }
}