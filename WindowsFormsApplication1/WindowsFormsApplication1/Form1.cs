using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AForge.Imaging;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);


        Form form2 = new WindowsFormsApplication1.Form2();

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;


        public Form1()
        {
            InitializeComponent();
            this.Paint+=Form1_Paint;

            int index;
            int upperBound; 
            Screen[] screens = Screen.AllScreens;
            upperBound = screens.GetUpperBound(0);
        
            for (index = 0; index <= upperBound; index++)
            {

                // For each screen, add the screen properties to a list box.

                listBox1.Items.Add("Device Name: " + screens[index].DeviceName);
                listBox1.Items.Add("Bounds: " + screens[index].Bounds.ToString());
                listBox1.Items.Add("Type: " + screens[index].GetType().ToString());
                listBox1.Items.Add("Working Area: " + screens[index].WorkingArea.ToString());
                listBox1.Items.Add("Primary Screen: " + screens[index].Primary.ToString());
                listBox1.Items.Add(""); listBox1.Items.Add(""); listBox1.Items.Add("");
                


                int x = screens[index].Bounds.Left;
                int y =screens[index].Bounds.Top;
                int cx = screens[index].Bounds.Right;
                int cy = screens[index].Bounds.Height;

                

                if (index == 1)
                {
                    
                    form2.Size = new Size(cx, cy);
                    SetWindowPos(form2.Handle, 0, x, y, cx, cy, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                    form2.TopMost = true;

                    form2.FormBorderStyle = FormBorderStyle.None;


                    form2.WindowState = FormWindowState.Maximized;
                    form2.Show();
                }

            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
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
                        g.DrawImage(fourierImage, 512, 0);
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
