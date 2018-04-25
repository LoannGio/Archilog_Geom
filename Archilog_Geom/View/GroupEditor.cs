using Archilog_Geom.Controller;
using Archilog_Geom.Model;
using System;
using System.Windows.Forms;

namespace Archilog_Geom.View
{
    public partial class GroupEditor : Form
    {
        private readonly CsGraphics _parent;
        private readonly GroupShapes _group;

        public GroupEditor(CsGraphics parent, GroupShapes group)
        {
            InitializeComponent();

            _parent = parent;
            _parent.Enabled = false;
            _group = group;
            var width = group.XMax - group.X;
            var height = group.YMax - group.Y;

            originXField.Minimum = - width / 2;
            originXField.Maximum = _parent.DrawingPanel.Width - width / 2;
            originXField.Value = group.X;

            originYField.Minimum = - height / 2;
            originYField.Maximum = _parent.DrawingPanel.Height - height / 2;
            originYField.Value = group.Y;

            colorIndicator.BackColor = _group.Color;

            colorPicker.Color = _group.Color;
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
            Mediator.Instance.UpdateGroup(_group, colorIndicator.BackColor, (int)originXField.Value, (int)originYField.Value);
            _parent.DrawingPanel.Refresh();
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Mediator.Instance.UpdateGroup(_group, colorIndicator.BackColor, (int)originXField.Value, (int)originYField.Value);
            _parent.DrawingPanel.Refresh();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GroupEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.Enabled = true;
        }
    }
}
