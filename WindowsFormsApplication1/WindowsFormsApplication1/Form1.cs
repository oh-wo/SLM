﻿using System;
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
using System.Threading;
using System.IO;

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

            makeTiltImage();
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
                if(decimal.TryParse(this.textXangle.Text,out xAngle)){
                    Thread x = new Thread(() => makeTiltImage());
                    x.Start();
                }
            }
        }
        /*
            int _width = this.panelTiltImage.Width;
            int _height = this.panelTiltImage.Height;
            tiltImage = new Bitmap(_width,_height);
            double thetaXD = double.Parse(this.textXangle.Text==""?"0":this.textXangle.Text);
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
            */

        decimal xAngle = (decimal)0.05;

        private void makeTiltImage()
        {
            bool besQ = false;
            int order = 2;
            decimal axiconAngle = (decimal)0.3;//degrees

            int vortexCharge = 0;

           
            decimal yAngle = (decimal)0;

            /////////////////////////////////////////////Define physical parameters

            decimal wavelength = (decimal)(800 * Math.Pow(10, -9));
            decimal refractiveIndex = (decimal)1.46;

            //////////////////////////////////////////////Setting SLM parameters

            //Specify the physical dimensions of the SLM face in metres
            decimal Lx = (decimal)0.012;
            decimal Ly = (decimal)0.016;
            //Specify the number of pixels that are along each dimension
            int resx = 600;
            int resy = 792;
            //Specify the scaling factor
            decimal scalex = 1;
            decimal scaley = 1;
            //Specify the number of levels each pixel can have
            decimal clev = 256;
            //Set the phase in which the mask will act over.
            decimal phaselimit = (decimal)2 * (decimal)Math.PI;
            //Rotation of the mask (radians)
            decimal ellprot = 0; 

            //Not 100% sure what this does
            decimal beta = 0; // lens factor [arb. unit]

            /////////////////////////////////////////////Calculating other parameters

            //Calculate dependent physical parameters
            decimal coneangle = (refractiveIndex-1)*axiconAngle;
            decimal angrad = (decimal)Math.PI/(decimal)180*coneangle;

            decimal k = 2 * (decimal)Math.PI / wavelength;
            decimal kz = (decimal)Math.Sqrt(Math.Pow((double)k, 2) / (1 + Math.Pow(Math.Tan((double)angrad), 2)));
            decimal kr = (decimal)Math.Tan((double)angrad) * kz;

            ///////////////////////////////////////////Setting up hologram mesh

            //Calculating Spacing
            decimal dLx = Lx/(resx-1);
            decimal dLy = Ly/(resy-1);
            //Setting up grid for hologram
            MeshGrid mesh = new MeshGrid(-Lx/2,dLx,Lx/2,-Ly/2,dLy,Ly/2);
            //Applying rotation
            decimal[,] xx = (new MatrixAdd((new MatrixMultiply(mesh.X,(decimal)Math.Cos((double)ellprot))).Mat,(new MatrixMultiply(mesh.Y,(decimal)Math.Sin((double)ellprot))).Mat)).Mat;
            decimal[,] yy = (new MatrixAdd((new MatrixMultiply(mesh.X,(decimal)(-Math.Sin((double)ellprot))).Mat),(new MatrixMultiply(mesh.Y,(decimal)Math.Cos((double)ellprot))).Mat)).Mat;
            
            //Transforing to polar co-ordinates
            CartToPol cartToPol = new CartToPol(xx,yy);

            decimal[,] Phi = new Zeros(resx, resy).Mat;

            ///////////////////////////////////////Calculating Bessel Hologram
            if (besQ && vortexCharge == 0)
            {
                //Calculating hologram
                Phi = new MatrixMultiply(cartToPol.Theta, (decimal)order).Mat;
                Phi = new MatrixAdd(Phi, -Phi.Cast<decimal>().Min()).Mat;
                Phi = new MatrixAdd(Phi, new MatrixMultiply(cartToPol.Rho, kr).Mat).Mat;
                Phi = new MatrixAdd(Phi, new MatrixMultiply(new MatrixExponential(cartToPol.Rho, 2).Mat, beta).Mat).Mat;
            }
            else
            {
                ///////////////////////////////////////Calculating Vortex Hologram
                if (besQ)
                {
                    Phi=new MatrixAdd(Phi,(new MatrixMultiply(cartToPol.Theta,vortexCharge).Mat)).Mat;
                    Phi=new MatrixMod(Phi,phaselimit).Mat;
                }
            }

            ///////////////////////////////////////////Calculating Stering Hologram

            //Calculating Hologram
            WriteMatrixToCsv(Phi,"Phi01.csv");
            Phi = new MatrixAdd(new MatrixAdd(Phi, (2 * (decimal)Math.PI / wavelength) * (decimal)Math.Sin(Math.PI / (double)180 * (double)yAngle)).Mat, xx).Mat;
            WriteMatrixToCsv(Phi, "Phi02.csv");
            Phi = new MatrixAdd(new MatrixAdd(Phi, (2 * (decimal)Math.PI / wavelength) * (decimal)Math.Sin(Math.PI / (double)180 * (double)xAngle)).Mat, yy).Mat;
            WriteMatrixToCsv(Phi, "Phi03.csv");
            Phi = new MatrixMod(Phi, phaselimit).Mat;

            WriteMatrixToCsv(yy, "yy01.csv");
            WriteMatrixToCsv(Phi, "Phi04.csv");

            //draw in c#
            Bitmap tempTiltImage = new Bitmap(resx, resy);

            decimal[,]  mask = new MatrixMultiply(Phi, 254 / (decimal)(Phi.Cast<decimal>().Max())).Mat;

            for (int i = 0; i < (resy-1); i++)
            {
                for (int j = 0; j < (resx - 1); j++)
                {
                    int color = int.Parse(Math.Ceiling(mask[j, i]).ToString());
                    tempTiltImage.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            tiltImage = tempTiltImage;
            tiltImage.Save("testtest56.bmp");

            this.panelTiltImage.Invalidate();
        }


        public void WriteMatrixToCsv(decimal[,] Mat,string filename)
        {
            if (!filename.EndsWith(".csv"))
            {
                filename += ".csv";
            }
            using (var w = new StreamWriter(String.Format("C:\\{0}",filename)))
            {
                int xDim = Mat.GetUpperBound(0) + 1;
                int yDim = Mat.GetUpperBound(1) + 1;
                for (int i = 0; i < (xDim - 1); i++)
                {
                    for (int j = 0; j < (yDim - 1); j++)
                    {
                        w.Write(string.Format("{0:0.00000000}, ", Mat[i, j]));
                    }
                    w.WriteLine("");
                }

                w.Flush();
            }
        }
        

        Bitmap calibrationImage;
        Bitmap tiltImage;
        string tiltImageName = "LSH0600812_850nm_calibration.bmp";

        public class MeshGrid
        {
            public MeshGrid(decimal xL, decimal xS, decimal xU, decimal yL, decimal yS, decimal yU)
            {
                //xL = x lower, xS = x spacing, x upper
                //yL = y lower, yS = y spacing, y upper

                int width = int.Parse(Math.Ceiling((xU - xL) / xS).ToString());
                int height = int.Parse(Math.Ceiling((yU - yL) / yS).ToString());


                X = new decimal[width, height];
                Y = new decimal[width, height];
                
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        X[i, j] = xL + xS * (decimal)i;
                        Y[i, j] = yL + yS * (decimal)j;
                    }
                }

            }
            public decimal[,] X { get; set; }
            public decimal[,] Y { get; set; }
        }
        
        public class MatrixMultiply
        {
            public MatrixMultiply(decimal[,] mat, decimal scalar)
            {
                int xDim = mat.GetUpperBound(0) + 1;
                int yDim = mat.GetUpperBound(1) + 1;
                Mat = new decimal[xDim, yDim];
                for (int i = 0; i < xDim; i++)
                {
                    for (int j = 0; j < yDim; j++)
                    {
                        Mat[i, j] = mat[i, j] * scalar;
                    }
                }
            }
            public decimal[,] Mat { get; set; }
        }
        
        public class MatrixAdd
        {
            public MatrixAdd(decimal[,] mat1, decimal[,] mat2)
            {
                int xDim = mat1.GetUpperBound(0) + 1;
                int yDim = mat1.GetUpperBound(1) + 1;
                Mat = new decimal[xDim, yDim];
                for (int i = 0; i < (xDim-1); i++)
                {
                    for (int j = 0; j < (yDim-1); j++)
                    {
                        Mat[i, j] = mat1[i, j] + mat2[i, j];
                    }
                }
            }
            public MatrixAdd(decimal[,] mat1, decimal scalar)
            {
                int xDim = mat1.GetUpperBound(0) + 1;
                int yDim = mat1.GetUpperBound(1) + 1;
                Mat = new decimal[xDim, yDim];
                for (int i = 0; i < xDim; i++)
                {
                    for (int j = 0; j < yDim; j++)
                    {
                        Mat[i, j] = mat1[i, j] + scalar;
                    }
                }
            }
            public decimal[,] Mat { get; set; }
        }

        public class MatrixExponential
        {
            public MatrixExponential(decimal[,] mat1, decimal scalar)
            {
                int xDim = mat1.GetUpperBound(0) + 1;
                int yDim = mat1.GetUpperBound(1) + 1;
                Mat = new decimal[xDim, yDim];
                for (int i = 0; i < xDim; i++)
                {
                    for (int j = 0; j < yDim; j++)
                    {
                        Mat[i, j] = (decimal)Math.Pow((double)mat1[i, j], (double)scalar);
                    }
                }
            }
            public decimal[,] Mat { get; set; }
        }

        public class CartToPol
        {
            public CartToPol(decimal[,] xMat,decimal[,] yMat)
            {
                int xDim = xMat.GetUpperBound(0) + 1;
                int yDim = xMat.GetUpperBound(1) + 1;
                Theta = new decimal[xDim, yDim];
                Rho = new decimal[xDim, yDim];
                for (int i = 0; i < (xDim-1); i++)
                {
                    for (int j = 0; j < (yDim-1); j++)
                    {
                        Theta[i, j] = (decimal)Math.Atan((double)(yMat[i, j]/xMat[i, j]));
                        Rho[i, j] = (decimal)Math.Sqrt(Math.Pow((double)xMat[i, j], 2) + Math.Pow((double)yMat[i, j], 2));
                    }
                }
            }
            public decimal[,] Theta { get; set; }
            public decimal[,] Rho { get; set; }
        }

        public class MatrixMod
        {
            //Modulus after division
            public MatrixMod(decimal[,] mat1, decimal scalar)
            {
                int xDim = mat1.GetUpperBound(0) + 1;
                int yDim = mat1.GetUpperBound(1) + 1;
                Mat = new decimal[xDim, yDim];
                for (int i = 0; i < xDim; i++)
                {
                    for (int j = 0; j < yDim; j++)
                    {
                        Mat[i, j] = mat1[i, j] % scalar;
                    }
                }
            }
            public decimal[,] Mat { get; set; }
        }
        public class Zeros
        {
            //Modulus after division
            public Zeros(int x, int y)
            {
                Mat = new decimal[x, y];
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        Mat[i, j] = 0;
                    }
                }
            }
            public decimal[,] Mat { get; set; }
        }

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
