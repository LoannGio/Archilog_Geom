namespace Archilog_Geom.View
{
    partial class GroupEditor
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
            this.colorLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.originYField = new System.Windows.Forms.NumericUpDown();
            this.originXField = new System.Windows.Forms.NumericUpDown();
            this.origineYLabel = new System.Windows.Forms.Label();
            this.origineXLabel = new System.Windows.Forms.Label();
            this.originPoint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).BeginInit();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(116, 122);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 29;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(212, 122);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // colorIndicator
            // 
            this.colorIndicator.Location = new System.Drawing.Point(86, 77);
            this.colorIndicator.Name = "colorIndicator";
            this.colorIndicator.Size = new System.Drawing.Size(75, 23);
            this.colorIndicator.TabIndex = 27;
            this.colorIndicator.UseVisualStyleBackColor = true;
            this.colorIndicator.Click += new System.EventHandler(this.colorIndicator_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(22, 77);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(43, 13);
            this.colorLabel.TabIndex = 26;
            this.colorLabel.Text = "Couleur";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(25, 122);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 25;
            this.submitButton.Text = "OK";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // colorPicker
            // 
            this.colorPicker.FullOpen = true;
            // 
            // originYField
            // 
            this.originYField.Location = new System.Drawing.Point(197, 29);
            this.originYField.Name = "originYField";
            this.originYField.Size = new System.Drawing.Size(53, 20);
            this.originYField.TabIndex = 34;
            // 
            // originXField
            // 
            this.originXField.Location = new System.Drawing.Point(112, 29);
            this.originXField.Name = "originXField";
            this.originXField.Size = new System.Drawing.Size(53, 20);
            this.originXField.TabIndex = 33;
            // 
            // origineYLabel
            // 
            this.origineYLabel.AutoSize = true;
            this.origineYLabel.Location = new System.Drawing.Point(171, 31);
            this.origineYLabel.Name = "origineYLabel";
            this.origineYLabel.Size = new System.Drawing.Size(20, 13);
            this.origineYLabel.TabIndex = 32;
            this.origineYLabel.Text = "Y: ";
            // 
            // origineXLabel
            // 
            this.origineXLabel.AutoSize = true;
            this.origineXLabel.Location = new System.Drawing.Point(83, 31);
            this.origineXLabel.Name = "origineXLabel";
            this.origineXLabel.Size = new System.Drawing.Size(23, 13);
            this.origineXLabel.TabIndex = 31;
            this.origineXLabel.Text = "X : ";
            // 
            // originPoint
            // 
            this.originPoint.AutoSize = true;
            this.originPoint.Location = new System.Drawing.Point(25, 31);
            this.originPoint.Name = "originPoint";
            this.originPoint.Size = new System.Drawing.Size(40, 13);
            this.originPoint.TabIndex = 30;
            this.originPoint.Text = "Origine";
            // 
            // GroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 157);
            this.Controls.Add(this.originYField);
            this.Controls.Add(this.originXField);
            this.Controls.Add(this.origineYLabel);
            this.Controls.Add(this.origineXLabel);
            this.Controls.Add(this.originPoint);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.colorIndicator);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.submitButton);
            this.Name = "GroupEditor";
            this.Text = "GroupEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.originYField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originXField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button colorIndicator;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.NumericUpDown originYField;
        private System.Windows.Forms.NumericUpDown originXField;
        private System.Windows.Forms.Label origineYLabel;
        private System.Windows.Forms.Label origineXLabel;
        private System.Windows.Forms.Label originPoint;
    }
}