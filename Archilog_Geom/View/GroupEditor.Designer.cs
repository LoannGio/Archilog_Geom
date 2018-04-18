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
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(118, 70);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 29;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(214, 70);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // colorIndicator
            // 
            this.colorIndicator.Location = new System.Drawing.Point(88, 25);
            this.colorIndicator.Name = "colorIndicator";
            this.colorIndicator.Size = new System.Drawing.Size(75, 23);
            this.colorIndicator.TabIndex = 27;
            this.colorIndicator.UseVisualStyleBackColor = true;
            this.colorIndicator.Click += new System.EventHandler(this.colorIndicator_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(24, 25);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(43, 13);
            this.colorLabel.TabIndex = 26;
            this.colorLabel.Text = "Couleur";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(27, 70);
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
            // GroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 113);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.colorIndicator);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.submitButton);
            this.Name = "GroupEditor";
            this.Text = "GroupEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupEditor_FormClosing);
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
    }
}