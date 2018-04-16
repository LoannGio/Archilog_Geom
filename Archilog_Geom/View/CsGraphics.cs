using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace Archilog_Geom
{
    public class CsGraphics : Form, IObservableGraphics
    {
        private Panel _toolBarPanel;
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
                p.Size = new Size(_toolBarPanel.Width, itemHeight);
                #endregion

                #region draw shape on sub panel
                Image img = new Bitmap(p.Width, p.Height);
                Graphics g = Graphics.FromImage(img);

                g.DrawRectangle(new Pen(Color.Blue), 10,10,50,50);
                p.BackgroundImage = img;
                #endregion

                _toolBarPanel.Controls.Add(p);
            }
        }


        private void InitializeComponent()
        {
            this._drawingPanel = new System.Windows.Forms.Panel();
            this._toolBarPanel = new System.Windows.Forms.Panel();
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
            this._toolBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._toolBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolBarPanel.Location = new System.Drawing.Point(13, 31);
            this._toolBarPanel.Name = "_toolBarPanel";
            this._toolBarPanel.Size = new System.Drawing.Size(134, 459);
            this._toolBarPanel.TabIndex = 1;
            // 
            // CsGraphics
            // 
            this.ClientSize = new System.Drawing.Size(836, 493);
            this.Controls.Add(this._toolBarPanel);
            this.Controls.Add(this._drawingPanel);
            this.Name = "CsGraphics";
            this.Text = "Archilog Geom";
            this.ResumeLayout(false);

        }

    }
}