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
        public static string imageName = "test.bmp";

        public List<Form> forms = new List<Form>();

        Bitmap originalImage;
        Bitmap img8bit;
        Bitmap fourierImage;

        int imageToShow = 0;//0=tilt, 1=fourier, 2=raw

        public Form1()
        {
            try
            {
                InitializeComponent();
                this.panel3.Paint += Panel3_Paint;
                this.panel2.Paint += Panel2_Paint;
                this.panelTiltImage.Paint += panelTiltImage_Paint;

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
                    listBox1.Items.Add("\n\n\n");


                    if (!screens[index].Primary)
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

                    this.panel3.Invalidate();
                    this.panel2.Invalidate();
                    foreach (RadioButton radio in this.groupBoxOption.Controls.OfType<RadioButton>().ToList())
                    {
                        radio.CheckedChanged += radioCheckedChanged;
                    }

                    this.textXangle.KeyUp += textXangle_KeyUp;
                    this.calibrationImage = (Bitmap)Bitmap.FromFile(tiltImageName);
                    this.checkBoxCalibration.CheckedChanged += checkBoxCalibration_CheckedChanged;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static Bitmap resizeImage(Bitmap imgToResize, Size size)
        {
            return (new Bitmap(imgToResize, size));
        }
        private void CalculateImages()
        {
            originalImage = (Bitmap)Bitmap.FromFile(imageName);
            int newHeight = int.Parse(Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Height)/Math.Log(2))).ToString());
            originalImage = resizeImage(originalImage, new System.Drawing.Size(newHeight, newHeight));
            // create complex image
            img8bit = BM.CopyToBpp(originalImage, 8);
            ComplexImage complexImage = ComplexImage.FromBitmap(img8bit);
            // do forward Fourier transformation
            complexImage.ForwardFourierTransform();
            // get complex image as bitmat
            fourierImage = complexImage.ToBitmap();

        }

     
        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
                try
            {
                CalculateImages();
                g.DrawImage(img8bit, new Rectangle(new Point(0, 0), new Size(panel3.Width, panel3.Height)));

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioCheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {//otherwise this event will fire (and run) for as many radio buttons as there are...
                string checkedName = this.groupBoxOption.Controls.OfType<RadioButton>()
                               .FirstOrDefault(n => n.Checked).Name;
                switch (checkedName)
                {
                    case "radioFourierTilt":
                        this.panelImage.Visible = false;
                        this.panelTilt.Visible = true;
                        imageToShow = 0;
                        break;
                    case "radioRawImage":
                        this.panelImage.Visible = true;
                        this.panelTilt.Visible = false;
                        imageToShow = 1;
                        break;
                    case "radioFourierImage":
                        this.panelImage.Visible = true;
                        this.panelTilt.Visible = false;
                        imageToShow = 2;
                        break;
                }

                foreach (Form2 form in forms)
                {
                    form.imageToShow = imageToShow;
                    form.tiltImage = tiltImage;
                    form.Invalidate();
                }
            }
           
        }
        private void textXangle_KeyUp(object Sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                makeTiltImage();
            }
        }
        private void makeTiltImage()
        {
            int _width = this.panelTiltImage.Width;
            int _height = this.panelTiltImage.Height;
            tiltImage = new Bitmap(_width,_height);

            //prepare for parse
            this.textXangle.Text=this.textXangle.Text == "" ? (0).ToString() : this.textXangle.Text;
            double thetaXD = double.Parse(this.textXangle.Text);
            double thetaX = Math.PI/180*thetaXD;
            double maxTheta = 2 * Math.PI / (800 * Math.Pow(10, -9)) * Math.Sin(Math.PI / 2) * Math.Max(_width, _height);
            double[,] x = new double[_width, _height];
            double[,] y = new double[_width, _height];
            int color;
            double max = 0;
            double coeff = 2 * Math.PI / (double)(800 * Math.Pow(10,-9)) * Math.Sin(thetaX);
            for (int i = 0; i < _height; i++)
            {
                for (double j = 0; j < _width; j+=1)
                {
                    x[(int)j, (int)i] = coeff *j;
                    if (x[(int)j, (int)i] > max)
                        max = x[(int)j, (int)i];
                }
            }

            int[,] xi = new int[_width, _height];
            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    xi[j, i] = int.Parse((Math.Round((x[j, i] + maxTheta) / (2 * maxTheta) * 255)).ToString());
                    if (this.checkBoxCalibration.Checked)
                    {
                      // xi[j, i] += calibrationImage.GetPixel(j, i).A;
                    }
                    tiltImage.SetPixel(j, i, Color.FromArgb(xi[j, i], xi[j, i], xi[j, i]));
                }
            }

            tiltImage.Save("testtest56.bmp");

            this.panelTiltImage.Invalidate();
        }
        Bitmap calibrationImage;
        Bitmap tiltImage;
        string tiltImageName = "LSH0600812_850nm_calibration.bmp";



        private void panelTiltImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                g.DrawImage(tiltImage, new Rectangle(new Point(0, 0), new Size(this.panelTiltImage.Width, this.panelTiltImage.Height)));

                g.Dispose();

                foreach (Form2 form in forms)
                {
                    form.imageToShow = imageToShow;
                    form.tiltImage = tiltImage;
                    form.Invalidate();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void checkBoxCalibration_CheckedChanged(object sender, EventArgs e)
        {
            makeTiltImage();
        }

        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFDialog = new OpenFileDialog();
            if (oFDialog.ShowDialog() == DialogResult.OK)
            {
                //update the ui display
                imageName = oFDialog.FileName;
                this.panel3.Invalidate();
                this.panel2.Invalidate();

                //update the SLM and other monitor
                foreach (Form2 form in forms)
                {
                    form._imageName = oFDialog.FileName;
                    form.Invalidate();
                }
                //update the label showing which file is open
                labelSelectedImage.InvokeIfRequired(() =>
                {
                    this.labelSelectedImage.Text = String.Format("Selected Image: {0}", oFDialog.SafeFileName);
                });


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
