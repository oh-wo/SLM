using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
       public string _imageName;
       public int imageToShow = 0;
       public Bitmap tiltImage;
        public Form2(string imageName)
        {
            _imageName = imageName;
            InitializeComponent();
            this.Paint+=Form2_Paint;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            DrawStuff(e.Graphics);
        }
        private void DrawStuff(Graphics g)
        {
            if (imageToShow != 0)
            {

                try
                {
                    Bitmap originalImage = (Bitmap)Bitmap.FromFile(_imageName);
                    int newHeight = int.Parse(Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Height) / Math.Log(2))).ToString());
                    originalImage = Form1.resizeImage(originalImage, new System.Drawing.Size(newHeight, newHeight));
                    // create complex image
                    Bitmap img8bit = BM.CopyToBpp(originalImage, 8);
                    // g.DrawImage(img8bit, new Point(0, 0));
                    using (Bitmap large = new Bitmap(512, 512, g))
                    {
                        using (Graphics largeGraphics = Graphics.FromImage(large))
                        {
                            ComplexImage complexImage = ComplexImage.FromBitmap(img8bit);
                            // do forward Fourier transformation
                            complexImage.ForwardFourierTransform();
                            // get complex image as bitmat
                            Bitmap fourierImage = complexImage.ToBitmap();
                        }
                    }
                    switch (imageToShow)
                    {
                        case 0:

                            break;
                        case 1:
                            g.DrawImage(originalImage, 0, 0, this.Width, this.Height);
                            break;
                        case 2:
                            // g.DrawImage(fourierImage, 0, 0, this.Width, this.Height);
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }
            if (imageToShow == 0 && tiltImage != null)
            {
                g.DrawImage(tiltImage, 0, 0, this.Width, this.Height);
            }
            g.Dispose();
        }
    }
}
