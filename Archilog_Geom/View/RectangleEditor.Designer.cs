namespace Archilog_Geom.View
{
    partial class RectangleEditor
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
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorIndicator = new System.Windows.Forms.Button();
            this.originYField = new System.Windows.Forms.NumericUpDown();
            this.originXField = new System.Windows.Forms.NumericUpDown();
            this.origineYLabel = new System.Windows.Forms.Label();
            this.origineXLabel = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.originPoint = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.heightField = new System.Windows.Forms.NumericUpDown();
            this.widthField = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthField)).BeginInit();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(142, 148);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 24;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(238, 148);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 23;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // colorIndicator
            // 
            this.colorIndicator.Location = new System.Drawing.Point(112, 103);
            this.colorIndicator.Name = "colorIndicator";
            this.colorIndicator.Size = new System.Drawing.Size(75, 23);
            this.colorIndicator.TabIndex = 22;
            this.colorIndicator.UseVisualStyleBackColor = true;
            this.colorIndicator.Click += new System.EventHandler(this.colorIndicator_Click);
            // 
            // originYField
            // 
            this.originYField.Location = new System.Drawing.Point(223, 21);
            this.originYField.Name = "originYField";
            this.originYField.Size = new System.Drawing.Size(53, 20);
            this.originYField.TabIndex = 21;
            // 
            // originXField
            // 
            this.originXField.Location = new System.Drawing.Point(138, 21);
            this.originXField.Name = "originXField";
            this.originXField.Size = new System.Drawing.Size(53, 20);
            this.originXField.TabIndex = 20;
            // 
            // origineYLabel
            // 
            this.origineYLabel.AutoSize = true;
            this.origineYLabel.Location = new System.Drawing.Point(197, 23);
            this.origineYLabel.Name = "origineYLabel";
            this.origineYLabel.Size = new System.Drawing.Size(20, 13);
            this.origineYLabel.TabIndex = 19;
            this.origineYLabel.Text = "Y: ";
            // 
            // origineXLabel
            // 
            this.origineXLabel.AutoSize = true;
            this.origineXLabel.Location = new System.Drawing.Point(109, 23);
            this.origineXLabel.Name = "origineXLabel";
            this.origineXLabel.Size = new System.Drawing.Size(23, 13);
            this.origineXLabel.TabIndex = 18;
            this.origineXLabel.Text = "X : ";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(48, 103);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(43, 13);
            this.colorLabel.TabIndex = 17;
            this.colorLabel.Text = "Couleur";
            // 
            // originPoint
            // 
            this.originPoint.AutoSize = true;
            this.originPoint.Location = new System.Drawing.Point(51, 23);
            this.originPoint.Name = "originPoint";
            this.originPoint.Size = new System.Drawing.Size(40, 13);
            this.originPoint.TabIndex = 16;
            this.originPoint.Text = "Origine";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(51, 148);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 15;
            this.submitButton.Text = "OK";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // heightField
            // 
            this.heightField.Location = new System.Drawing.Point(292, 63);
            this.heightField.Name = "heightField";
            this.heightField.Size = new System.Drawing.Size(53, 20);
            this.heightField.TabIndex = 29;
            // 
            // widthField
            // 
            this.widthField.Location = new System.Drawing.Point(164, 63);
            this.widthField.Name = "widthField";
            this.widthField.Size = new System.Drawing.Size(53, 20);
            this.widthField.TabIndex = 28;
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(235, 63);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(51, 13);
            this.heightLabel.TabIndex = 27;
            this.heightLabel.Text = "Hauteur :";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(109, 63);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(52, 13);
            this.widthLabel.TabIndex = 26;
            this.widthLabel.Text = "Largeur : ";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(30, 63);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(61, 13);
            this.sizeLabel.TabIndex = 25;
            this.sizeLabel.Text = "Dimensions";
            // 
            // colorPicker
            // 
            this.colorPicker.FullOpen = true;
            // 
            // RectangleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 183);
            this.Controls.Add(this.heightField);
            this.Controls.Add(this.widthField);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.colorIndicator);
            this.Controls.Add(this.originYField);
            this.Controls.Add(this.originXField);
            this.Controls.Add(this.origineYLabel);
            this.Controls.Add(this.origineXLabel);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.originPoint);
            this.Controls.Add(this.submitButton);
            this.Name = "RectangleEditor";
            this.Text = "RectangleEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RectangleEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button colorIndicator;
        private System.Windows.Forms.NumericUpDown originYField;
        private System.Windows.Forms.NumericUpDown originXField;
        private System.Windows.Forms.Label origineYLabel;
        private System.Windows.Forms.Label origineXLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label originPoint;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.NumericUpDown heightField;
        private System.Windows.Forms.NumericUpDown widthField;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.ColorDialog colorPicker;
    }
}