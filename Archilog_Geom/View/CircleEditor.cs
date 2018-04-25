using Archilog_Geom.Controller;
using Archilog_Geom.Model;
using System;
using System.Windows.Forms;

namespace Archilog_Geom.View
{
    public partial class CircleEditor : Form
    {
        private readonly Circle _circle;
        private readonly CsGraphics _parent;

        public CircleEditor(CsGraphics parent, Circle c)
        {
            InitializeComponent();

            _parent = parent;
            _parent.Enabled = false;
            _circle = c;

            diameterField.Minimum = 2;
            diameterField.Maximum = 200;
            diameterField.Value = _circle.Diameter;

            originXField.Minimum = -diameterField.Value /2;
            originXField.Maximum = _parent.DrawingPanel.Width - diameterField.Value/2;
            originXField.Value = _circle.X;

            originYField.Minimum = -diameterField.Value /2;
            originYField.Maximum = _parent.DrawingPanel.Height - diameterField.Value/2;
            originYField.Value = _circle.Y;


            colorIndicator.BackColor = _circle.Color;

            colorPicker.Color = _circle.Color;
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
            originXField.Maximum = _parent.DrawingPanel.Width - diameterField.Value / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - diameterField.Value / 2;
            originXField.Minimum = -diameterField.Value / 2;
            originYField.Minimum = -diameterField.Value / 2;

            Mediator.Instance.UpdateCircle(_circle, (int)originXField.Value, (int)originYField.Value, (int)diameterField.Value, colorIndicator.BackColor);

            _parent.DrawingPanel.Refresh();
            this.Close();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {

            originXField.Maximum = _parent.DrawingPanel.Width - diameterField.Value / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - diameterField.Value / 2;
            originXField.Minimum = -diameterField.Value / 2;
            originYField.Minimum = -diameterField.Value / 2;

            Mediator.Instance.UpdateCircle(_circle, (int)originXField.Value, (int)originYField.Value, (int)diameterField.Value, colorIndicator.BackColor);

            _parent.DrawingPanel.Refresh();
        }

        private void CircleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.Enabled = true;
        }
    }
}
