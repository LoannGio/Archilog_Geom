namespace Archilog_Geom.View
{
    partial class CircleEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.submitButton = new System.Windows.Forms.Button();
            this.diameterLabel = new System.Windows.Forms.Label();
            this.originPoint = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.diameterField = new System.Windows.Forms.NumericUpDown();
            this.origineXLabel = new System.Windows.Forms.Label();
            this.origineYLabel = new System.Windows.Forms.Label();
            this.originXField = new System.Windows.Forms.NumericUpDown();
            this.originYField = new System.Windows.Forms.NumericUpDown();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.colorIndicator = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.diameterField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).BeginInit();
            this.SuspendLayout();
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(12, 166);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "OK";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // diameterLabel
            // 
            this.diameterLabel.AutoSize = true;
            this.diameterLabel.Location = new System.Drawing.Point(25, 25);
            this.diameterLabel.Name = "diameterLabel";
            this.diameterLabel.Size = new System.Drawing.Size(49, 13);
            this.diameterLabel.TabIndex = 1;
            this.diameterLabel.Text = "Diamètre";
            // 
            // originPoint
            // 
            this.originPoint.AutoSize = true;
            this.originPoint.Location = new System.Drawing.Point(34, 65);
            this.originPoint.Name = "originPoint";
            this.originPoint.Size = new System.Drawing.Size(40, 13);
            this.originPoint.TabIndex = 2;
            this.originPoint.Text = "Origine";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(31, 105);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(43, 13);
            this.colorLabel.TabIndex = 3;
            this.colorLabel.Text = "Couleur";
            // 
            // diameterField
            // 
            this.diameterField.Location = new System.Drawing.Point(95, 23);
            this.diameterField.Name = "diameterField";
            this.diameterField.Size = new System.Drawing.Size(53, 20);
            this.diameterField.TabIndex = 5;
            this.diameterField.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // origineXLabel
            // 
            this.origineXLabel.AutoSize = true;
            this.origineXLabel.Location = new System.Drawing.Point(92, 65);
            this.origineXLabel.Name = "origineXLabel";
            this.origineXLabel.Size = new System.Drawing.Size(23, 13);
            this.origineXLabel.TabIndex = 6;
            this.origineXLabel.Text = "X : ";
            // 
            // origineYLabel
            // 
            this.origineYLabel.AutoSize = true;
            this.origineYLabel.Location = new System.Drawing.Point(180, 65);
            this.origineYLabel.Name = "origineYLabel";
            this.origineYLabel.Size = new System.Drawing.Size(20, 13);
            this.origineYLabel.TabIndex = 7;
            this.origineYLabel.Text = "Y: ";
            // 
            // originXField
            // 
            this.originXField.Location = new System.Drawing.Point(121, 63);
            this.originXField.Name = "originXField";
            this.originXField.Size = new System.Drawing.Size(53, 20);
            this.originXField.TabIndex = 8;
            // 
            // originYField
            // 
            this.originYField.Location = new System.Drawing.Point(206, 63);
            this.originYField.Name = "originYField";
            this.originYField.Size = new System.Drawing.Size(53, 20);
            this.originYField.TabIndex = 9;
            // 
            // colorPicker
            // 
            this.colorPicker.FullOpen = true;
            // 
            // colorIndicator
            // 
            this.colorIndicator.Location = new System.Drawing.Point(95, 100);
            this.colorIndicator.Name = "colorIndicator";
            this.colorIndicator.Size = new System.Drawing.Size(75, 23);
            this.colorIndicator.TabIndex = 10;
            this.colorIndicator.UseVisualStyleBackColor = true;
            this.colorIndicator.Click += new System.EventHandler(this.colorIndicator_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(184, 166);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(95, 166);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 14;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // CircleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 204);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.colorIndicator);
            this.Controls.Add(this.originYField);
            this.Controls.Add(this.originXField);
            this.Controls.Add(this.origineYLabel);
            this.Controls.Add(this.origineXLabel);
            this.Controls.Add(this.diameterField);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.originPoint);
            this.Controls.Add(this.diameterLabel);
            this.Controls.Add(this.submitButton);
            this.Name = "CircleEditor";
            this.Text = "CircleEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CircleEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.diameterField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label diameterLabel;
        private System.Windows.Forms.Label originPoint;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.NumericUpDown diameterField;
        private System.Windows.Forms.Label origineXLabel;
        private System.Windows.Forms.Label origineYLabel;
        private System.Windows.Forms.NumericUpDown originXField;
        private System.Windows.Forms.NumericUpDown originYField;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.Button colorIndicator;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
    }
}