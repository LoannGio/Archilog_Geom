using Archilog_Geom.Controller;
using System;
using System.Windows.Forms;
using Rectangle = Archilog_Geom.Model.Rectangle;

namespace Archilog_Geom.View
{
    public partial class RectangleEditor : Form
    {
        private readonly Rectangle _rect;
        private readonly CsGraphics _parent;

        public RectangleEditor(CsGraphics parent, Rectangle r)
        {
            InitializeComponent();

            _parent = parent;
            _parent.Enabled = false;
            _rect = r;


            widthField.Minimum = 1;
            widthField.Maximum = 200;
            widthField.Value = _rect.Width;

            heightField.Minimum = 1;
            heightField.Maximum = 200;
            heightField.Value = _rect.Height;

            originXField.Minimum = - widthField.Value / 2;
            originXField.Maximum = _parent.DrawingPanel.Width - widthField.Value / 2;
            originXField.Value = _rect.X;

            originYField.Minimum = - heightField.Value / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - heightField.Value / 2;
            originYField.Value = _rect.Y;

            colorIndicator.BackColor = _rect.Color;

            colorPicker.Color = _rect.Color;
        }

        private void colorIndicator_Click(object sender, EventArgs e)
        {
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                colorIndicator.BackColor = colorPicker.Color;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            originXField.Minimum = -widthField.Value / 2;
            originXField.Maximum = _parent.DrawingPanel.Width - widthField.Value / 2;
            originYField.Minimum = -heightField.Value / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - heightField.Value / 2;

            Mediator.Instance.UpdateRectangle(_rect, (int)originXField.Value, (int)originYField.Value, (int)widthField.Value, (int)heightField.Value, colorIndicator.BackColor);

            _parent.DrawingPanel.Refresh();
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            originXField.Minimum = -widthField.Value / 2;
            originXField.Maximum = _parent.DrawingPanel.Width - widthField.Value / 2;
            originYField.Minimum = -heightField.Value / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - heightField.Value / 2;

            Mediator.Instance.UpdateRectangle(_rect, (int)originXField.Value, (int)originYField.Value, (int)widthField.Value, (int)heightField.Value, colorIndicator.BackColor);

            _parent.DrawingPanel.Refresh();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RectangleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.Enabled = true;
        }
    }
}
