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
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.button1 = new System.Windows.Forms.Button();
            this.labelSelectedImage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toggleDisplayImage = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "open image..";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSelectedImage
            // 
            this.labelSelectedImage.AutoSize = true;
            this.labelSelectedImage.Location = new System.Drawing.Point(270, 38);
            this.labelSelectedImage.Name = "labelSelectedImage";
            this.labelSelectedImage.Size = new System.Drawing.Size(120, 13);
            this.labelSelectedImage.TabIndex = 2;
            this.labelSelectedImage.Text = "Selected Image:  (none)";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(519, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 211);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(519, 229);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 211);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(436, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fourier Image: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(436, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Original Image: ";
            // 
            // toggleDisplayImage
            // 
            this.toggleDisplayImage.Appearance = System.Windows.Forms.Appearance.Button;
            this.toggleDisplayImage.AutoSize = true;
            this.toggleDisplayImage.Location = new System.Drawing.Point(273, 147);
            this.toggleDisplayImage.Name = "toggleDisplayImage";
            this.toggleDisplayImage.Size = new System.Drawing.Size(81, 23);
            this.toggleDisplayImage.TabIndex = 7;
            this.toggleDisplayImage.Text = "Fourier Image";
            this.toggleDisplayImage.UseVisualStyleBackColor = true;
            this.toggleDisplayImage.CheckedChanged += new System.EventHandler(this.toggleDisplayImage_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Currently displaying: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 460);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toggleDisplayImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSelectedImage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelSelectedImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox toggleDisplayImage;
        private System.Windows.Forms.Label label3;

    }
}

