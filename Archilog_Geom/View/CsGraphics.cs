using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Archilog_Geom.View;

namespace Archilog_Geom
{
    public class CsGraphics : Form, IObservableGraphics
    {
        private Panel _toolBarPanel;
        private Panel _drawingPanel;
        public Panel DrawingPanel => _drawingPanel;
        private Label label1;
        private PictureBox garbage;
        private int toolbarItemHeight = 100;
        private Pen selectedItemPen = new Pen(Color.Black);

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


        public void OpenRightClickPopUp()
        {
            ContextMenu popUp = new ContextMenu();
            foreach (var item in Mediator.Instance.RightClickPopUp.RightClickPopUpItems)
            {
                MenuItem mi = new MenuItem(item);
                mi.Click += RightClickMenuItem_Click;
                popUp.MenuItems.Add(mi);
            }

            int mouseX = MousePosition.X - this.Bounds.X;
            int mouseY = MousePosition.Y - this.Bounds.Y - _drawingPanel.Bounds.Y;
            popUp.Show(this, new Point(mouseX, mouseY));
        }

        public void OpenCircleEditMenu(Circle c)
        {
            var CircleEditor = new CircleEditor(this, c);
            CircleEditor.Show();
        }

        public void OpenRectangleEditMenu(Rectangle r)
        {
            var rectangleEditor = new RectangleEditor(this, r);
            rectangleEditor.Show();
        }

        public void OpenGroupEditMenu(GroupShapes g)
        {
            var GroupEditor = new GroupEditor(this, g);
            GroupEditor.Show();
        }


        private void RightClickMenuItem_Click(object sender, EventArgs eventArgs)
        {
            Mediator.Instance.HandleRightClickMenuItemClick(((MenuItem)sender).Index);
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
                    label1.Text = Mediator.Instance.SelectedShapes.Count.ToString();
                }
                else
                {
                    Mediator.Instance.DrawingPanelLeftMouseButtonPressed(e.X, e.Y);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Mediator.Instance.DrawingPanelRightMouseButtonPressed(e.X, e.Y);
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
            foreach (var shape in Mediator.Instance.DrawnShapes)
            {
                PaintShape(e, shape);
            }
        }

        private void PaintShape(PaintEventArgs e, IShape shape)
        {
            if (shape.GetType() == typeof(Rectangle))
            {
                PaintRectangle(e, (Rectangle)shape);
            }
            else if (shape.GetType() == typeof(Circle))
            {
                PaintCircle(e, (Circle)shape);

            }
            else if (shape.GetType() == typeof(GroupShapes))
            {
                bool AmISelected = Mediator.Instance.SelectedShapes.Contains((GroupShapes)shape);
                PaintGroupShapes(e, (GroupShapes)shape, AmISelected);
            }
        }

        private void PaintGroupShapes(PaintEventArgs e, GroupShapes group, bool AmISelected)
        {
            foreach (var shape in group.Children)
            {
                if (shape.GetType() == typeof(Rectangle))
                {
                    PaintRectangleGroup(e, (Rectangle)shape, AmISelected);
                }
                else if (shape.GetType() == typeof(Circle))
                {
                    PaintCircleGroup(e, (Circle)shape, AmISelected);
                }
                else if (shape.GetType() == typeof(GroupShapes))
                {
                    PaintGroupShapes(e, (GroupShapes)shape, AmISelected);
                }
            }
        }

        private void PaintRectangleGroup(PaintEventArgs e, Rectangle rect, bool AmISelected)
        {
            SolidBrush brush;
            brush = new SolidBrush(rect.Color);
            e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
            if (AmISelected)
            {
                e.Graphics.DrawRectangle(selectedItemPen, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        private void PaintCircleGroup(PaintEventArgs e, Circle circle, bool AmISelected)
        {
            SolidBrush brush;
            brush = new SolidBrush(circle.Color);
            e.Graphics.FillEllipse(brush, circle.X, circle.Y, circle.Diameter, circle.Diameter);
            if (AmISelected)
            {
                e.Graphics.DrawEllipse(selectedItemPen, circle.X, circle.Y, circle.Diameter, circle.Diameter);
            }
        }

        private void PaintRectangle(PaintEventArgs e, Rectangle rect)
        {
            SolidBrush brush;
            brush = new SolidBrush(rect.Color);
            e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
            if (Mediator.Instance.SelectedShapes.Contains(rect))
            {
                e.Graphics.DrawRectangle(selectedItemPen, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        private void PaintCircle(PaintEventArgs e, Circle circle)
        {
            SolidBrush brush;
            brush = new SolidBrush(circle.Color);
            e.Graphics.FillEllipse(brush, circle.X, circle.Y, circle.Diameter, circle.Diameter);
            if (Mediator.Instance.SelectedShapes.Contains(circle))
            {
                e.Graphics.DrawEllipse(selectedItemPen, circle.X, circle.Y, circle.Diameter, circle.Diameter);
            }
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
            this._drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._drawingPanel_Paint);
            this._drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this._drawingPanel_MouseDown);
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
    }
}