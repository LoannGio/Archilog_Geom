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
        private int toolbarItemHeight = 100;
        private IShape currentShape;

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
                int itemXposition = 0;
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Location = new Point(itemXposition, i*toolbarItemHeight);
                p.Size = new Size(_toolBarPanel.Width, toolbarItemHeight);
                #endregion

                #region draw shape on sub panel
                Image img = new Bitmap(p.Width, p.Height);
                drawShapeOnImage(img, shapes[i]);
                p.BackgroundImage = img;
                p.MouseDown += new MouseEventHandler(this.subPanel_MouseDown);
                #endregion

                _toolBarPanel.Controls.Add(p);
            }
        }

        private void drawShapeOnImage(Image img, IShape shape)
        {
            Graphics g = Graphics.FromImage(img);
            Pen p;
            int xMax = img.Width;
            int yMax = img.Height;

            if (shape.GetType() == typeof(Rectangle))
            {
                Rectangle rect = (Rectangle)shape;
                p = new Pen(rect.Color);

                System.Drawing.Rectangle drawing_rect = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                while (drawing_rect.X + drawing_rect.Width > xMax || drawing_rect.Y + drawing_rect.Height > yMax)
                {
                    drawing_rect.X /= 2;
                    drawing_rect.Y /= 2;
                    drawing_rect.Width /= 2;
                    drawing_rect.Height /= 2;
                }

                g.DrawRectangle(p, drawing_rect);
            }
            else if (shape.GetType() == typeof(Circle))
            {
                Circle circle = (Circle) shape;
                p = new Pen(circle.Color);

                System.Drawing.Rectangle drawing_rect = new System.Drawing.Rectangle(circle.X, circle.Y, circle.Radius, circle.Radius);
                while (drawing_rect.X + drawing_rect.Width > xMax || drawing_rect.Y + drawing_rect.Height > yMax)
                {
                    drawing_rect.X /= 2;
                    drawing_rect.Y /= 2;
                    drawing_rect.Width /= 2;
                    drawing_rect.Height /= 2;
                }

                g.DrawEllipse(p, drawing_rect.X, drawing_rect.Y, drawing_rect.Width, drawing_rect.Height);

            }
            else if (shape.GetType() == typeof(GroupShapes))
            {
                GroupShapes group = (GroupShapes)shape;


            }
        }

        private void subPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Control c = (Control) sender;
            int subPanelNumber = c.Top / 100;
            currentShape = (IShape)Mediator.Instance.ToolBar.ToolBarShapes[subPanelNumber].Clone();
            int x = 10;
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
            this._drawingPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this._drawingPanel_MouseUp);
            // 
            // _toolBarPanel
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

        private void _drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Mediator.Instance.DrawnShapes.Add(currentShape);

            currentShape = null;
        }
    }
}