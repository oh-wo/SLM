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


        //Form form2 = new WindowsFormsApplication1.Form2();

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;
        public static string imageName = "fresnelLens.gif";

        public List<Form> forms = new List<Form>();

        Bitmap originalImage;
        Bitmap img8bit;
        Bitmap fourierImage;

        public Form1()
        {
            InitializeComponent();
            this.panel1.Paint += Panel1_Paint;
            
            

            this.panel2.Paint += Panel2_Paint;
            

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
                


                

                

                if (index >=1 )
                {
                    int x = screens[index].Bounds.Left;
                    int y = screens[index].Bounds.Top;
                    int cx = screens[index].Bounds.Right;
                    int cy = screens[index].Bounds.Height;
                    Form2 form2 = new Form2(imageName);
                    form2.Size = new Size(cx, cy);
                    SetWindowPos(form2.Handle, 0, x, y, cx, cy, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                    form2.TopMost = true;

                    form2.FormBorderStyle = FormBorderStyle.None;


                    form2.WindowState = FormWindowState.Maximized;
                    form2.Show();
                    forms.Add(form2);
                }

            }
            this.panel1.Invalidate();
            this.panel2.Invalidate();
        }

        private void CalculateImages()
        {
            originalImage = (Bitmap)Bitmap.FromFile(imageName);
            // create complex image
            img8bit = BM.CopyToBpp(originalImage, 8);
            ComplexImage complexImage = ComplexImage.FromBitmap(img8bit);
            // do forward Fourier transformation
            complexImage.ForwardFourierTransform();
            // get complex image as bitmat
            fourierImage = complexImage.ToBitmap();
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
                try
            {
                CalculateImages();
                g.DrawImage(img8bit, new Rectangle(new Point(0, 0), new Size(panel1.Width, panel1.Height)));

                g.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                CalculateImages();
                g.DrawImage(fourierImage, new Rectangle(new Point(0, 0), new Size(panel2.Width, panel2.Height)));


                g.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFDialog = new OpenFileDialog();
            if (oFDialog.ShowDialog() == DialogResult.OK)
            {
                //update the ui display
                imageName = oFDialog.FileName;
                this.panel1.Invalidate();
                this.panel2.Invalidate();

                //update the SLM and other monitor
                foreach (Form2 form in forms)
                {
                    form._imageName = oFDialog.FileName;
                }
                //update the label showing which file is open
                labelSelectedImage.InvokeIfRequired(() =>
                {
                    this.labelSelectedImage.Text = String.Format("Selected Image: {0}", oFDialog.SafeFileName);
                });

                
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toggleDisplayImage_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toggleDisplayImage.Checked)
            {
                this.toggleDisplayImage.Text = "Original Image";
                foreach (Form2 form in forms)
                {
                    form.showFourierImage = false;
                    form.Invalidate();
                }
            }
            else
            {
                this.toggleDisplayImage.Text = "Fourier Image";
                foreach (Form2 form in forms)
                {
                    form.showFourierImage = true;
                    form.Invalidate();
                }
            }
        }
        
        
        

    }
    public static class Extensions
    {
        public static void InvokeIfRequired(this Label label, MethodInvoker action)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
