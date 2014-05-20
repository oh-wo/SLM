namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.checkBoxCalibration = new System.Windows.Forms.CheckBox();
            this.radioFourierTilt = new System.Windows.Forms.RadioButton();
            this.radioRawImage = new System.Windows.Forms.RadioButton();
            this.radioFourierImage = new System.Windows.Forms.RadioButton();
            this.panelImage = new System.Windows.Forms.Panel();
            this.panelTilt = new System.Windows.Forms.Panel();
            this.panelTiltImage = new System.Windows.Forms.Panel();
            this.trackBarXAngle = new System.Windows.Forms.TrackBar();
            this.textXangle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelSelectedImage = new System.Windows.Forms.Label();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxOption.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.panelTilt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(252, 433);
            this.listBox1.TabIndex = 0;
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.checkBoxCalibration);
            this.groupBoxOption.Controls.Add(this.radioFourierTilt);
            this.groupBoxOption.Controls.Add(this.radioRawImage);
            this.groupBoxOption.Controls.Add(this.radioFourierImage);
            this.groupBoxOption.Location = new System.Drawing.Point(270, 12);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Size = new System.Drawing.Size(200, 433);
            this.groupBoxOption.TabIndex = 12;
            this.groupBoxOption.TabStop = false;
            this.groupBoxOption.Text = "Operation type";
            // 
            // checkBoxCalibration
            // 
            this.checkBoxCalibration.AutoSize = true;
            this.checkBoxCalibration.Checked = true;
            this.checkBoxCalibration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCalibration.Location = new System.Drawing.Point(6, 89);
            this.checkBoxCalibration.Name = "checkBoxCalibration";
            this.checkBoxCalibration.Size = new System.Drawing.Size(113, 17);
            this.checkBoxCalibration.TabIndex = 15;
            this.checkBoxCalibration.Text = "Inlcude Calibration";
            this.checkBoxCalibration.UseVisualStyleBackColor = true;
            // 
            // radioFourierTilt
            // 
            this.radioFourierTilt.AutoSize = true;
            this.radioFourierTilt.Checked = true;
            this.radioFourierTilt.Location = new System.Drawing.Point(6, 19);
            this.radioFourierTilt.Name = "radioFourierTilt";
            this.radioFourierTilt.Size = new System.Drawing.Size(70, 17);
            this.radioFourierTilt.TabIndex = 12;
            this.radioFourierTilt.TabStop = true;
            this.radioFourierTilt.Text = "Fourier tilt";
            this.radioFourierTilt.UseVisualStyleBackColor = true;
            // 
            // radioRawImage
            // 
            this.radioRawImage.AutoSize = true;
            this.radioRawImage.Location = new System.Drawing.Point(6, 66);
            this.radioRawImage.Name = "radioRawImage";
            this.radioRawImage.Size = new System.Drawing.Size(79, 17);
            this.radioRawImage.TabIndex = 14;
            this.radioRawImage.Text = "Raw Image";
            this.radioRawImage.UseVisualStyleBackColor = true;
            // 
            // radioFourierImage
            // 
            this.radioFourierImage.AutoSize = true;
            this.radioFourierImage.Location = new System.Drawing.Point(6, 43);
            this.radioFourierImage.Name = "radioFourierImage";
            this.radioFourierImage.Size = new System.Drawing.Size(89, 17);
            this.radioFourierImage.TabIndex = 13;
            this.radioFourierImage.Text = "Fourier Image";
            this.radioFourierImage.UseVisualStyleBackColor = true;
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.label2);
            this.panelImage.Controls.Add(this.label1);
            this.panelImage.Controls.Add(this.panel2);
            this.panelImage.Controls.Add(this.panel3);
            this.panelImage.Controls.Add(this.labelSelectedImage);
            this.panelImage.Controls.Add(this.buttonOpenImage);
            this.panelImage.Location = new System.Drawing.Point(476, 16);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(713, 446);
            this.panelImage.TabIndex = 13;
            this.panelImage.Visible = false;
            // 
            // panelTilt
            // 
            this.panelTilt.Controls.Add(this.textBox1);
            this.panelTilt.Controls.Add(this.trackBar1);
            this.panelTilt.Controls.Add(this.panelTiltImage);
            this.panelTilt.Controls.Add(this.trackBarXAngle);
            this.panelTilt.Controls.Add(this.textXangle);
            this.panelTilt.Location = new System.Drawing.Point(476, 16);
            this.panelTilt.Name = "panelTilt";
            this.panelTilt.Size = new System.Drawing.Size(710, 446);
            this.panelTilt.TabIndex = 14;
            // 
            // panelTiltImage
            // 
            this.panelTiltImage.Location = new System.Drawing.Point(151, 32);
            this.panelTiltImage.Name = "panelTiltImage";
            this.panelTiltImage.Size = new System.Drawing.Size(449, 370);
            this.panelTiltImage.TabIndex = 2;
            // 
            // trackBarXAngle
            // 
            this.trackBarXAngle.Location = new System.Drawing.Point(0, 133);
            this.trackBarXAngle.Maximum = 1;
            this.trackBarXAngle.Minimum = -1;
            this.trackBarXAngle.Name = "trackBarXAngle";
            this.trackBarXAngle.Size = new System.Drawing.Size(104, 45);
            this.trackBarXAngle.TabIndex = 1;
            // 
            // textXangle
            // 
            this.textXangle.Location = new System.Drawing.Point(9, 62);
            this.textXangle.Name = "textXangle";
            this.textXangle.Size = new System.Drawing.Size(47, 20);
            this.textXangle.TabIndex = 0;
            this.textXangle.Text = "0.05";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Original Image: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Fourier Image: ";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(285, 226);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 211);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(285, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 211);
            this.panel3.TabIndex = 11;
            // 
            // labelSelectedImage
            // 
            this.labelSelectedImage.AutoSize = true;
            this.labelSelectedImage.Location = new System.Drawing.Point(3, 30);
            this.labelSelectedImage.Name = "labelSelectedImage";
            this.labelSelectedImage.Size = new System.Drawing.Size(120, 13);
            this.labelSelectedImage.TabIndex = 10;
            this.labelSelectedImage.Text = "Selected Image:  (none)";
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(3, 4);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(107, 23);
            this.buttonOpenImage.TabIndex = 9;
            this.buttonOpenImage.Text = "open image..";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(110, 112);
            this.trackBar1.Maximum = 1;
            this.trackBar1.Minimum = -1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 104);
            this.trackBar1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 460);
            this.Controls.Add(this.panelTilt);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.groupBoxOption);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            this.panelTilt.ResumeLayout(false);
            this.panelTilt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.RadioButton radioFourierTilt;
        private System.Windows.Forms.RadioButton radioRawImage;
        private System.Windows.Forms.RadioButton radioFourierImage;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelSelectedImage;
        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Panel panelTilt;
        private System.Windows.Forms.TrackBar trackBarXAngle;
        private System.Windows.Forms.TextBox textXangle;
        private System.Windows.Forms.Panel panelTiltImage;
        private System.Windows.Forms.CheckBox checkBoxCalibration;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBar1;

    }
}

