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

        //----------- GRAPHICS STUFF --------
        Random R = new Random();
        string nl = Environment.NewLine;
        Graphics g1, g2;
        Bitmap b1, b2;
        ResizableRectangle ViewPort1, ViewPort2;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;

        // --------- STAT STUFF --------
        List<DataPoint> DataPointsForCDF;
        int NumOfSampleMeans = 0;
        int IncreaseSampleMean = 10;
        int NumOfSamples = 5;
        int MinValueUniform = 1;
        int MaxValueUniform = 10;
        UniformDistribution U;
        List<double> SampleMeans = new List<double>();

        // ----------------------- HANDLERS ------------------
        private void button1_Click(object sender, EventArgs e)
        {
            // create Distribution
            U = new UniformDistribution(MinValueUniform, MaxValueUniform, R.Next());

            // init viewports
            MinX_Win = MinValueUniform;
            MinY_Win = 0;
            MaxX_Win = MaxValueUniform;
            MaxY_Win = 1;
            ViewPort1 = new ResizableRectangle(this.cdfPictureBox, b1, g1, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(10, 10,
                this.cdfPictureBox.Width - 70, this.cdfPictureBox.Height - 70));
            ViewPort1.ModifiedRect += drawCDF;


            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = 10;
            MaxY_Win = 50;
            ViewPort2 = new ResizableRectangle(this.histogramPictureBox, b2, g2, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(10, 10,
                this.histogramPictureBox.Width - 70, this.histogramPictureBox.Height - 70));
            //ViewPort2.ModifiedRect += drawHistogram;

            // Start the timer for simulation
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            NumOfSampleMeans += IncreaseSampleMean;
            // add more points to the List of Sample Means
            for (int i = 0; i < IncreaseSampleMean; i++)
            {
                List<double> Sample = U.sampleFromUniform(NumOfSamples);
                SampleMeans.Add(Sample.Average());
            }

            // to draw the CDF, i sort the list of sample means and then use it as
            // X component. The Y, on the other hand, is a list of equispaced
            // doubles ranging from 0 to 1, where each delta is 1/NumOfSampleMeans
            SampleMeans.Sort();
            DataPointsForCDF = createListOfDataPoints(SampleMeans, createListOfEquispacedDoubles(NumOfSampleMeans));

            drawCDF();
        }

        // ------------------ GRAPHICS FUNCTIONS----------------
        private void initGraphics()
        {
            b1 = new Bitmap(this.cdfPictureBox.Width, this.cdfPictureBox.Height);
            g1 = Graphics.FromImage(b1);
            g1.SmoothingMode = SmoothingMode.HighQuality;

            b2 = new Bitmap(this.cdfPictureBox.Width, this.cdfPictureBox.Height);
            g2 = Graphics.FromImage(b2);
            g2.SmoothingMode = SmoothingMode.HighQuality;

        }

        private void drawAxis(ResizableRectangle VP, Graphics g, string Name_X, string Name_Y)
        {
            Pen p = new Pen(Color.Black);
            p.EndCap = LineCap.ArrowAnchor;

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

        private void drawCDF()
        {
            g1.Clear(Color.Gainsboro);

            // draw axis and horizontal line on 1
            drawAxis(ViewPort1, g1, " ", "");
            Pen P = new Pen(Color.RoyalBlue);
            P.DashStyle = DashStyle.Dash;
            drawHorizontalLine("1", 1, P, ViewPort1, g1);

            g1.DrawLines(Pens.Red, createListOfPointsForChart(DataPointsForCDF, ViewPort1).ToArray());

            this.cdfPictureBox.Image = b1;
        }

        //private void drawHorizontalHistogram(double y, ResizableRectangle VP, List<Interval> L)
        //{
            ////double StartingPointX = MinX_Win;
            ////FrequencyDistributionX = new List<Interval>();
            ////FrequencyDistributionX = UnivariateDistribution_CountinuousVariable(DataSetForChart.Select(D => D.X).ToList(), StartingPointX, StepX);

            //// draw proportionate rectangles 
            //double BarWidth = (double)R2.R.Width / FrequencyDistributionX.Count;
            //double BarMaxHeight = R2.R.Height * 0.7;
            //int BarNum = 0;
            //foreach (Interval I in FrequencyDistributionX)
            //{
            //    float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
            //    RectangleF R = new RectangleF(
            //        (float)(R2.R.X + (BarNum * BarWidth)),
            //        (float)(R2.R.Y + R2.R.Height - (BarMaxHeight * I.RelativeFrequency)),
            //        (float)BarWidth,
            //        BarHeight
            //        );

            //    // drawing a line for each interval represening the average
            //    // datapoints in each interval
            //    IEnumerable<double> DataPointsInInterval =
            //        from D in DataSetForChart
            //        where D.X >= I.LowerInclusiveBound && D.X < (I.LowerInclusiveBound + StepX)
            //        select D.X;

            //    double BarMean = computeOnlineMean(DataPointsInInterval.ToList());

            //    PointF StartingPointForMean = new PointF(
            //        (float)R2.viewport_X(BarMean),
            //        R2.R.Y + R2.R.Height);

            //    PointF EndingPointForMean = new PointF(
            //        (float)R2.viewport_X(BarMean),
            //        R2.R.Y + R2.R.Height - BarHeight);

            //    SolidBrush B = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
            //    g.FillRectangle(B, R);
            //    g.DrawLine(Pens.Brown, StartingPointForMean, EndingPointForMean);

            //    BarNum++;
            //}
        //}

        //private void drawVerticalHistogram(int n, ResizableRectangle V)
        //{
        //    double Step = 0.05;
        //    double StartingPoint = MinY_Win;


        //    // create the dataset with the means at step n
        //    //List<double> MeanAtStepN = new List<double>();
        //    //for (int i = 0; i < 10000; i++)
        //    //{
        //    //    MeanAtStepN.Add(r.NextDouble());
        //    //}
        //    List<double> MeanAtStepN = Bernoullis.Select(B => B.MeanDistribution[n].Y).ToList();
        //    List<Interval> FrequencyDistribution = new List<Interval>();
        //    FrequencyDistribution = UnivariateDistribution_CountinuousVariable(MeanAtStepN, StartingPoint, Step);

        //    // add intervals to cover all the range [0,1]
        //    addPaddingIntervals(FrequencyDistribution, Step);

        //    // invert list so the biggest comes before the smallest
        //    List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

        //    // draw proportionate rectangles 
        //    double BarWidth = (double)V.R.Height / ReversedFrequencyDistribution.Count;
        //    double BarMaxHeight = V.R.Width * 0.2;
        //    int BarNum = 0;
        //    foreach (Interval I in ReversedFrequencyDistribution)
        //    {
        //        float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
        //        RectangleF R = new RectangleF(
        //            V.viewport_X(MinX_Win + n),
        //            (float)(V.R.Y + (BarNum * BarWidth)),
        //            (float)BarHeight,
        //            (float)BarWidth
        //            );

        //        SolidBrush B = new SolidBrush(Color.FromArgb(200, 253, 185, 39));
        //        Pen pen = new Pen(Color.FromArgb(200, 85, 37, 130));
        //        g.DrawRectangles(pen, new[] { R });
        //        g.FillRectangle(B, R);
        //        BarNum++;
        //    }

        //    // count how many means are inside the strip at step n
        //    int PathsInsideStrip = MeanAtStepN.Count(M => (M < p + eps) && (M > p - eps));
        //    drawVerticalLine($"n = {n + 1}\npaths in strip = {PathsInsideStrip}", n, Pens.Purple);

        //}
        private void drawHorizontalLine(string label, double y, Pen pen, ResizableRectangle VP, Graphics g)
        {
            g.DrawLine(
                pen,
                VP.viewport_X(VP.MinX_Win),
                VP.viewport_Y(VP.MinY_Win + y),
                VP.viewport_X(VP.MaxX_Win),
                VP.viewport_Y(VP.MinY_Win + y));

            g.DrawString(
                label,
                DefaultFont,
                Brushes.Black,
                VP.viewport_X(VP.MinX_Win) - g.MeasureString(label, DefaultFont).Width,
                VP.viewport_Y(VP.MinY_Win + y)
                );
        }


        // ---------- USEFUL STUFF ----------
        private List<double> createListOfEquispacedDoubles(int M)
        {
            List<double> L = new List<double>();
            for (int i = 0; i < M; i++)
            {
                L.Add(i / (double)M);
            }

            return L;
        }

        private List<DataPoint> createListOfDataPoints(List<double> X, List<double> Y)
        {
            List<DataPoint> Points = new List<DataPoint>();

            for (int j = 0; j < X.Count(); j++)
            {
                Points.Add(new DataPoint(X[j], Y[j]));
            }

            return Points;
        }

        private List<PointF> createListOfPointsForChart(List<DataPoint> L, ResizableRectangle VP)
        {
            List<PointF> Points = new List<PointF>();

            foreach (DataPoint DP in L)
            {
                Points.Add(new PointF(
                    (float)VP.viewport_X(DP.X),
                    (float)VP.viewport_Y(DP.Y)));
            }

            return Points;
        }


    }
}
