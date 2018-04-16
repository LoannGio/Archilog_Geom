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
        private TableLayoutPanel toolBarPanel;
        private Panel _drawingPanel;

        public CsGraphics()
        {
          InitializeComponent();
            test();
        }

        private void InitializeComponent()
        {
            this._drawingPanel = new System.Windows.Forms.Panel();
            this.toolBarPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // _drawingPanel
            // 
            this._drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._drawingPanel.Location = new System.Drawing.Point(153, 12);
            this._drawingPanel.Name = "_drawingPanel";
            this._drawingPanel.Size = new System.Drawing.Size(671, 478);
            this._drawingPanel.TabIndex = 0;
            // 
            // toolBarPanel
            // 
            this.toolBarPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.toolBarPanel.ColumnCount = 1;
            this.toolBarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.toolBarPanel.Location = new System.Drawing.Point(4, 12);
            this.toolBarPanel.Name = "toolBarPanel";
            this.toolBarPanel.RowCount = 1;
            this.toolBarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.toolBarPanel.Size = new System.Drawing.Size(143, 478);
            this.toolBarPanel.TabIndex = 1;
            this.toolBarPanel.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.toolBarPanel_CellPaint);
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

        private void test()
        {
            
        }

        public void UpdateToolbarOnView(List<IShape> toolBarShapes)
        {
 
        }

        private void toolBarPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

        }
    }
}