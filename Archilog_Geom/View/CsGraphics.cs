using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Archilog_Geom
{
    public class CsGraphics : Form, IObservableGraphics
    {
        private Panel _toolBarPanel;
        private Panel _drawingPanel;
        private Label label1;
        private PictureBox garbage;
        private int toolbarItemHeight = 100;

        public CsGraphics()
        {
            InitializeComponent();
            InitializeToolBar();
       
        }

        public void InitializeToolBar()
        {
            _toolBarPanel.Controls.Clear();
            var shapes = Mediator.Instance.ToolBar.ToolBarShapes;
            for (int i = 0; i < shapes.Count; i++)
            {
                #region create the sub panel
                Panel subPanel = new Panel();
                int itemXposition = 0;
                subPanel.BorderStyle = BorderStyle.FixedSingle;
                subPanel.Location = new Point(itemXposition, i*toolbarItemHeight);
                subPanel.Size = new Size(_toolBarPanel.Width, toolbarItemHeight);
                #endregion

                #region draw shape on sub panel
                Image img = new Bitmap(subPanel.Width, subPanel.Height);
                drawShapeOnImage(img, shapes[i]);
                subPanel.BackgroundImage = img;
                subPanel.MouseDown += new MouseEventHandler(this.subPanel_MouseDown);
                subPanel.MouseUp += new MouseEventHandler(this.subPanel_MouseUp);
                #endregion

                _toolBarPanel.Controls.Add(subPanel);
            }
        }

        public void RefreshView()
        {
            _drawingPanel.Refresh();
        }

        public void RefreshToolBar()
        {
            InitializeToolBar();
        }

        private void drawShapeOnImage(Image img, IShape shape)
        {
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush b;
            int xMax = img.Width;
            int yMax = img.Height;

            if (shape.GetType() == typeof(Rectangle))
            {
                Rectangle rect = (Rectangle)shape;
                b = new SolidBrush(rect.Color);

                System.Drawing.Rectangle drawing_rect = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                drawing_rect = ReplaceShape(drawing_rect);

                g.FillRectangle(b, drawing_rect);
            }
            else if (shape.GetType() == typeof(Circle))
            {
                Circle circle = (Circle) shape;
                b = new SolidBrush(circle.Color);

                System.Drawing.Rectangle drawing_rect = new System.Drawing.Rectangle(circle.X, circle.Y, circle.Diameter, circle.Diameter);
                drawing_rect = ReplaceShape(drawing_rect);

                g.FillEllipse(b, drawing_rect.X, drawing_rect.Y, drawing_rect.Width, drawing_rect.Height);

            }
            else if (shape.GetType() == typeof(GroupShapes))
            {
                GroupShapes group = (GroupShapes)shape;


            }
        }

        private System.Drawing.Rectangle ReplaceShape(System.Drawing.Rectangle drawingRect)
        {
            while (drawingRect.Width >= _toolBarPanel.Width || drawingRect.Height >= toolbarItemHeight)
            {
                drawingRect.Width /= 2;
                drawingRect.Height /= 2;
            }

            drawingRect.X = _toolBarPanel.Width / 2 - drawingRect.Width / 2;
            drawingRect.Y = toolbarItemHeight / 2 - drawingRect.Height / 2;
            return drawingRect;
        }

        private void subPanel_MouseDown(object sender, MouseEventArgs e)
        {
<<<<<<< HEAD
            Control c = (Control) sender;
            int subPanelNumber = c.Top / 100;
            currentShape = (IShape)Mediator.Instance.ToolBar.ToolBarShapes[subPanelNumber].Clone();
            int x = 10;
=======
            if (e.Button == MouseButtons.Left)
            {
                Control c = (Control)sender;
                int subPanelNumber = c.Top / toolbarItemHeight;
                Mediator.Instance.LoadCurrentShape(subPanelNumber);
            }
        }

        private void subPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control c = (Control) sender;
                int subPanelNumber = c.Top / toolbarItemHeight;
                int mouseX = e.X + _toolBarPanel.Bounds.X;
                int mouseY = e.Y + subPanelNumber * toolbarItemHeight + _drawingPanel.Bounds.Y;
                if (_drawingPanel.Bounds.Contains(mouseX, mouseY))
                {
                    int dp_X = _drawingPanel.Bounds.X;
                    int toolbar_X = _toolBarPanel.Bounds.X;
                    Mediator.Instance.DrawCurrentShape(e.X - dp_X + toolbar_X, e.Y + subPanelNumber * toolbarItemHeight);
                    _drawingPanel.Refresh();
                }

                #region Delete shape

                mouseX = e.X + _toolBarPanel.Bounds.X;
                mouseY = e.Y + _toolBarPanel.Bounds.Y + subPanelNumber * toolbarItemHeight;
                if (garbage.Bounds.Contains(mouseX, mouseY))
                {
                    Mediator.Instance.DeleteCurrentShapeFromToolBar(subPanelNumber);
                }
                #endregion

            }
        }

        private void _drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    Mediator.Instance.AddRemoveSelectedShape(e.X, e.Y);
                }
                else
                {
                    Mediator.Instance.DrawingPanelMouseDownCalled(e.X, e.Y);
                }
            }
        }

        private void _drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                #region Draw shape
                Control c = (Control)sender;
                int subPanelNumber = c.Top / toolbarItemHeight;
                int mouseX = e.X + (_drawingPanel.Bounds.X - _toolBarPanel.Bounds.X);
                int mouseY = e.Y + subPanelNumber * toolbarItemHeight + _drawingPanel.Bounds.Y;
                if (Mediator.Instance.ClickedOnSelectedShape && _toolBarPanel.Bounds.Contains(mouseX, mouseY))
                {
                    Mediator.Instance.SaveShapesInToolbar();
                }
                #endregion

                #region Delete shape

                mouseX = e.X + _drawingPanel.Bounds.X;
                mouseY = e.Y + _drawingPanel.Bounds.Y;
                if (Mediator.Instance.ClickedOnSelectedShape && garbage.Bounds.Contains(mouseX, mouseY))
                {
                    Mediator.Instance.DeleteSelectedShapes();
                }
                #endregion
            }
        }

        private void _drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush brush;
            Color selectedItemColor = Color.Black;
            foreach (var shape in Mediator.Instance.DrawnShapes)
            {
                if (shape.GetType() == typeof(Rectangle))
                {
                    Rectangle rect = (Rectangle) shape;
                    brush = new SolidBrush(rect.Color);
                    e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
                    if (Mediator.Instance.SelectedShapes.Contains(shape))
                    {
                        e.Graphics.DrawRectangle(new Pen(selectedItemColor), rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }
                else if (shape.GetType() == typeof(Circle))
                {
                    Circle circle = (Circle)shape;
                    brush = new SolidBrush(circle.Color);
                    e.Graphics.FillEllipse(brush, circle.X, circle.Y, circle.Diameter, circle.Diameter);
                    if (Mediator.Instance.SelectedShapes.Contains(shape))
                    {
                        e.Graphics.DrawEllipse(new Pen(selectedItemColor), circle.X, circle.Y, circle.Diameter, circle.Diameter);
                    }
                }
                else if (shape.GetType() == typeof(GroupShapes))
                {

                }
            }
>>>>>>> 2270c05ee011d0752e9d4455c2dceace9039474a
        }

        private void InitializeComponent()
        {
            this._drawingPanel = new System.Windows.Forms.Panel();
            this._toolBarPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.garbage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.garbage)).BeginInit();
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
<<<<<<< HEAD
=======
            this._drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawingPanel_Paint);
            this._drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this._drawingPanel_MouseDown);
>>>>>>> 2270c05ee011d0752e9d4455c2dceace9039474a
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(465, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // garbage
            // 
            this.garbage.Image = global::Archilog_Geom.Properties.Resources.dustbin1;
            this.garbage.Location = new System.Drawing.Point(371, 5);
            this.garbage.Name = "garbage";
            this.garbage.Size = new System.Drawing.Size(20, 20);
            this.garbage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.garbage.TabIndex = 4;
            this.garbage.TabStop = false;
            // 
            // CsGraphics
            // 
            this.ClientSize = new System.Drawing.Size(836, 493);
            this.Controls.Add(this.garbage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._toolBarPanel);
            this.Controls.Add(this._drawingPanel);
            this.Name = "CsGraphics";
            this.Text = "Archilog Geom";
            ((System.ComponentModel.ISupportInitialize)(this.garbage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Mediator.Instance.DrawnShapes.Add(currentShape);

            currentShape = null;
        }
    }
}