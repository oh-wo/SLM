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
        public Form2()
        {
            InitializeComponent();
            this.Paint+=Form2_Paint;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            DrawStuff(e.Graphics);
        }
        private void DrawStuff(Graphics g)
        {

            try
            {
                Bitmap originalImage = (Bitmap)Bitmap.FromFile(@"batman-logo.gif");
                // create complex image
                Bitmap img8bit = BM.CopyToBpp(originalImage, 8);
                g.DrawImage(img8bit, new Point(0, 0));
                using (Bitmap large = new Bitmap(512, 512, g))
                {
                    using (Graphics largeGraphics = Graphics.FromImage(large))
                    {
                        ComplexImage complexImage = ComplexImage.FromBitmap(img8bit);
                        // do forward Fourier transformation
                        complexImage.ForwardFourierTransform();
                        // get complex image as bitmat
                        Bitmap fourierImage = complexImage.ToBitmap();
                        g.DrawImage(fourierImage, 0,0,this.Width,this.Height);
                    }
                }

                g.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
