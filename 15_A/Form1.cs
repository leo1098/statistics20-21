using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace _15_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initGraphics();

        }

        Random R = new Random();
        string nl = Environment.NewLine;
        Graphics g1, g2;
        Bitmap b1, b2;
        ResizableRectangle ViewPort1, ViewPort2;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;

        // ------------ HANDLERS ------------------

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> SampleMeans;
            int MaxM = 1000;
            int N = 5;
            int MinValueUniform = 1;
            int MaxValueUniform = 10;


            MinX_Win = MinValueUniform;
            MinY_Win = 0;
            MaxX_Win = MaxValueUniform;
            MaxY_Win = 1;
            ViewPort1 = new ResizableRectangle(this.cdfPictureBox, b1, g1, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(10, 10,
                this.cdfPictureBox.Width - 70, this.cdfPictureBox.Height - 70));
            ViewPort1.ModifiedRect += drawCDF;

            g1.FillRectangle(Brushes.Red, ViewPort1.R);
            this.cdfPictureBox.Image = b1;


            UniformDistribution U = new UniformDistribution(MinValueUniform, MaxValueUniform, R.Next());

            for (int M = 500; M < MaxM; M++)
            {
                SampleMeans = new List<double>();
                for (int i = 0; i < M; i++)
                {
                    List<double> Sample = U.sampleFromUniform(N);
                    SampleMeans.Add(Sample.Average());
                }
                SampleMeans.Sort();

                List<double> PointsOnY = createListOfEquispacedDoubles(M);

                List<DataPoint> DataPointsForCDF = createCDFPoints(SampleMeans, PointsOnY);

            }

        }

        // -------------GRAPHICS FUNCTIONS----------------
        private void initGraphics()
        {
            b1 = new Bitmap(this.cdfPictureBox.Width, this.cdfPictureBox.Height);
            g1 = Graphics.FromImage(b1);
            g1.SmoothingMode = SmoothingMode.HighQuality;

            b2 = new Bitmap(this.cdfPictureBox.Width, this.cdfPictureBox.Height);
            g2 = Graphics.FromImage(b2);
            g2.SmoothingMode = SmoothingMode.HighQuality;

        }


        private void drawAxis(ResizableRectangle VP, Graphics g)
        {
            Pen p = new Pen(Color.Black);
            p.EndCap = LineCap.ArrowAnchor;
            string Name_Y = "";
            string Name_X = "trials";

            // X axis
            g.DrawLine(
                p,
                VP.viewport_X(VP.MinX_Win),
                VP.viewport_Y(VP.MinY_Win),
                VP.viewport_X(VP.MaxX_Win),
                VP.viewport_Y(VP.MinY_Win));

            g.DrawString(Name_X, DefaultFont, Brushes.Black,
                VP.viewport_X(VP.MaxX_Win),
                (float)(VP.viewport_Y(VP.MinY_Win) + 0.05 * VP.MaxY_Win));

            // Y axis
            g.DrawLine(
                p,
                VP.viewport_X(VP.MinX_Win),
                VP.viewport_Y(VP.MinY_Win),
                VP.viewport_X(VP.MinX_Win),
                VP.viewport_Y(VP.MaxY_Win + 0.1)
                );

            g.DrawString(
                Name_Y,
                DefaultFont,
                Brushes.Black,
                VP.viewport_X(VP.MinX_Win) - g.MeasureString(Name_Y, DefaultFont).Width,
                (float)(VP.viewport_Y(VP.MaxY_Win) + 0.05 * VP.MaxX_Win));
        }

        private List<double> createListOfEquispacedDoubles(int M)
        {
            List<double> L = new List<double>();
            ;
            for (int i = 0; i < M; i++)
            {
                L.Add(i / (double)M);
            }

            return L;
        }

        private List<PointF> createCDFPoints(List<double> X, List<double> Y)
        {
            List<DataPoint> Points = new List<DataPoint>();

            for (int j = 0; j < X.Count(); j++)
            {
                Points.Add(new PointF((float)X[j], (float)Y[j]));
            }

            return Points;
        }

        private void drawCDF()
        {
            g1.Clear(Color.Gainsboro);
            drawAxis(ViewPort1, g1);



            this.cdfPictureBox.Image = b1;
        }
    }
}
