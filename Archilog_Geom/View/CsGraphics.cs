using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Archilog_Geom.Controller;
using Archilog_Geom.Model;
using Rectangle = Archilog_Geom.Model.Rectangle;

namespace Archilog_Geom.View
{
    public class CsGraphics : Form, IGraphics
    {
        private Panel _toolBarPanel;
        private Panel _drawingPanel;
        public Panel DrawingPanel => _drawingPanel;
        private readonly int toolbarItemHeight = 100;
        private PictureBox garbage;
        private Button undo;
        private Button redo;
        private Button import;
        private Button export;
        private Button clearAll;

        public CsGraphics()
        {
            InitializeComponent();
            InitializeToolBar();
            RefreshView();
            RefreshToolBar();
        }

        public void InitializeToolBar()
        {
            _toolBarPanel.Controls.Clear();
            var shapes = Mediator.Instance.ToolBar.Items();
            for (var i = 0; i < shapes.Count; i++)
            {
                #region create the sub panel
                var subPanel = new Panel();
                var itemXposition = 0;
                subPanel.BorderStyle = BorderStyle.FixedSingle;
                subPanel.Location = new Point(itemXposition, i*toolbarItemHeight);
                subPanel.Size = new Size(_toolBarPanel.Width, toolbarItemHeight);
                #endregion

                #region draw shape on sub panel
                Image img = new Bitmap(subPanel.Width, subPanel.Height);
                shapes[i].Accept(new CsGraphicsDrawThumbnail(img, _toolBarPanel.Width, toolbarItemHeight));
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
            var popUp = new ContextMenu();
            foreach (var item in Mediator.Instance.RightClickPopUp.RightClickPopUpItems)
            {
                var mi = new MenuItem(item);
                mi.Click += RightClickMenuItem_Click;
                popUp.MenuItems.Add(mi);
            }

            var mouseX = MousePosition.X - this.Bounds.X;
            var mouseY = MousePosition.Y - this.Bounds.Y - _drawingPanel.Bounds.Y;
            popUp.Show(this, new Point(mouseX, mouseY));
        }

        public void OpenCircleEditMenu(Circle c)
        {
            var CircleEditor = new CircleEditor(this, c);
            CircleEditor.Show();
        }

        public void OpenRectangleEditMenu(Rectangle r)
        {
            var RectangleEditor = new RectangleEditor(this, r);
            RectangleEditor.Show();
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

        private void subPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = (Control)sender;
                var subPanelNumber = c.Top / toolbarItemHeight;
                Mediator.Instance.LoadCurrentShape(subPanelNumber);
            }
        }

        private void subPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = (Control) sender;
                var subPanelNumber = c.Top / toolbarItemHeight;
                var mouseX = e.X + _toolBarPanel.Bounds.X;
                var mouseY = e.Y + subPanelNumber * toolbarItemHeight + _drawingPanel.Bounds.Y;
                if (_drawingPanel.Bounds.Contains(mouseX, mouseY))
                {
                    var dp_X = _drawingPanel.Bounds.X;
                    var toolbar_X = _toolBarPanel.Bounds.X;
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
                var c = (Control)sender;
                var subPanelNumber = c.Top / toolbarItemHeight;
                var mouseX = e.X + (_drawingPanel.Bounds.X - _toolBarPanel.Bounds.X);
                var mouseY = e.Y + subPanelNumber * toolbarItemHeight + _drawingPanel.Bounds.Y;
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
                shape.Accept(new CsGraphicsDrawShape(e));
            }
        }

        private void undo_Click(object sender, EventArgs e)
        {
            Mediator.Instance.Undo();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            Mediator.Instance.Redo();
        }

        private void export_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            var result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
                 Mediator.Instance.Export(saveDialog.FileName);
        }

        private void import_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
                 Mediator.Instance.Import(fileDialog.FileName);
        }

        private void CsGraphics_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mediator.Instance.SaveBeforeAppClosure();
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            Mediator.Instance.ClearDrawingPanel();
        }

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CsGraphics));
            this._drawingPanel = new System.Windows.Forms.Panel();
            this._toolBarPanel = new System.Windows.Forms.Panel();
            this.garbage = new System.Windows.Forms.PictureBox();
            this.undo = new System.Windows.Forms.Button();
            this.redo = new System.Windows.Forms.Button();
            this.import = new System.Windows.Forms.Button();
            this.export = new System.Windows.Forms.Button();
            this.clearAll = new System.Windows.Forms.Button();
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
            // undo
            // 
            this.undo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("undo.BackgroundImage")));
            this.undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.undo.Location = new System.Drawing.Point(13, 2);
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(25, 23);
            this.undo.TabIndex = 7;
            this.undo.UseVisualStyleBackColor = true;
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // redo
            // 
            this.redo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("redo.BackgroundImage")));
            this.redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.redo.Location = new System.Drawing.Point(44, 2);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(25, 23);
            this.redo.TabIndex = 8;
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // import
            // 
            this.import.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("import.BackgroundImage")));
            this.import.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.import.Location = new System.Drawing.Point(122, 2);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(25, 23);
            this.import.TabIndex = 9;
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // export
            // 
            this.export.BackColor = System.Drawing.SystemColors.Control;
            this.export.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("export.BackgroundImage")));
            this.export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.export.Location = new System.Drawing.Point(91, 2);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(25, 23);
            this.export.TabIndex = 10;
            this.export.UseVisualStyleBackColor = false;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // clearAll
            // 
            this.clearAll.Location = new System.Drawing.Point(252, 2);
            this.clearAll.Name = "clearAll";
            this.clearAll.Size = new System.Drawing.Size(75, 23);
            this.clearAll.TabIndex = 11;
            this.clearAll.Text = "Reinitialiser";
            this.clearAll.UseVisualStyleBackColor = true;
            this.clearAll.Click += new System.EventHandler(this.clearAll_Click);
            // 
            // CsGraphics
            // 
            this.ClientSize = new System.Drawing.Size(836, 493);
            this.Controls.Add(this.clearAll);
            this.Controls.Add(this.export);
            this.Controls.Add(this.import);
            this.Controls.Add(this.redo);
            this.Controls.Add(this.undo);
            this.Controls.Add(this.garbage);
            this.Controls.Add(this._toolBarPanel);
            this.Controls.Add(this._drawingPanel);
            this.Name = "CsGraphics";
            this.Text = "Archilog Geom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CsGraphics_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.garbage)).EndInit();
            this.ResumeLayout(false);

        }
    }
}