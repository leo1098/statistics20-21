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
        //double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;

        // --------- STAT STUFF --------
        List<DataPoint> DataPointsForCDF;
        int NumOfSampleMeans = 100;
        int IncreaseSampleMean = 100;
        int IncreaseN = 10;
        int NumOfUnits = 5;
        int MinValueUniform = 1;
        int MaxValueUniform = 10;
        UniformDistribution U;
        List<double> SampleMeans = new List<double>();
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;

        // ----------------------- HANDLERS ------------------
        private void button1_Click(object sender, EventArgs e)
        {
            // create Distribution
            U = new UniformDistribution(MinValueUniform, MaxValueUniform, R.Next());

            // init viewports
            MinX_Win = MinValueUniform;
            MinY_Win = 0;
            MaxX_Win = 50;
            MaxY_Win = 1;
            ViewPort1 = new ResizableRectangle(this.cdfPictureBox, b1, g1, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(10, 10,
                this.cdfPictureBox.Width - 70, this.cdfPictureBox.Height - 70));
            ViewPort1.ModifiedRect += drawCDF;


            MinX_Win = MinValueUniform;
            MinY_Win = 0;
            MaxX_Win = MaxValueUniform;
            MaxY_Win = this.histogramPictureBox.Height - 70;
            ViewPort2 = new ResizableRectangle(this.histogramPictureBox, b2, g2, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(10, 10,
                this.histogramPictureBox.Width - 70, this.histogramPictureBox.Height - 70));
            ViewPort2.ModifiedRect += drawHistogram;

            // Start the timer for simulation
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            NumOfSampleMeans += IncreaseSampleMean;
            NumOfUnits += IncreaseN;
            //add more points to the List of Sample Means
            this.textBox1.Clear();
            this.textBox1.Text = NumOfUnits.ToString();
            this.textBox2.Text = NumOfSampleMeans.ToString();

            SampleMeans.Clear();
            for (int i = 0; i < NumOfSampleMeans; i++)
            {
                List<double> Sample = U.sampleFromUniform(NumOfUnits);
                SampleMeans.Add(Math.Sqrt(NumOfUnits) * (Sample.Average() - 5));
            }
            ViewPort1.MinX_Win = SampleMeans.Min();
            ViewPort1.MaxX_Win = SampleMeans.Max();
            //ViewPort1.MaxX_Win = Math.Sqrt(NumOfUnits) * Sample.Average();
            ViewPort1.Range_X = ViewPort1.MaxX_Win - ViewPort1.MinX_Win;

            //List<double> Sample = U.sampleFromUniform(NumOfSamples);

            //SampleMeans.Add(Sample.Average());

            // to draw the CDF, i sort the list of sample means and then use it as
            // X component. The Y, on the other hand, is a list of equispaced
            // doubles ranging from 0 to 1, where each delta is 1/NumOfSampleMeans
            SampleMeans.Sort();
            DataPointsForCDF = createListOfDataPoints(SampleMeans, createListOfEquispacedDoubles(SampleMeans.Count()));
            drawCDF();

            drawHistogram();
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

        private void drawAxes(ResizableRectangle VP, Graphics g, string Name_X, string Name_Y)
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
            drawAxes(ViewPort1, g1, " ", "");
            Pen P = new Pen(Color.RoyalBlue);
            P.DashStyle = DashStyle.Dash;
            drawHorizontalLine("1", 1, P, ViewPort1, g1);

            g1.DrawLines(Pens.Red, createListOfPointsForChart(DataPointsForCDF, ViewPort1).ToArray());

            this.cdfPictureBox.Image = b1;
        }

        private void drawHistogram()
        {
            g2.Clear(Color.Gainsboro);

            double Step = 1;
            double StartingPoint = 0;

            List<Interval> SampleMeanFreqDistr = UnivariateDistribution_CountinuousVariable(SampleMeans, MinValueUniform, Step);
            addPaddingIntervals(SampleMeanFreqDistr, MaxValueUniform);

            drawAxes(ViewPort2, g2, "", "freq");
            drawHorizontalHistogram(0, ViewPort2, g2, SampleMeanFreqDistr);
            this.histogramPictureBox.Image = b2;

        }

        private void drawHorizontalHistogram(double y, ResizableRectangle VP, Graphics g, List<Interval> FreqDistr)
        {
            double StartingPoint = VP.MinY_Win;

            // draw proportionate rectangles 
            double BarWidth = (double)VP.R.Width / FreqDistr.Count;
            double BarMaxHeight = 3*VP.R.Height;
            int BarNum = 0;
            foreach (Interval I in FreqDistr)
            {
                float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                RectangleF R = new RectangleF(
                    (float)(VP.R.X + (BarNum * BarWidth)),
                    (float)(VP.R.Y + VP.R.Height - (BarMaxHeight * I.RelativeFrequency) - y),
                    (float)BarWidth,
                    BarHeight
                    );
                g.FillRectangles(Brushes.Purple, new[] { R });
                BarNum++;
            }
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
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

        private List<Interval> UnivariateDistribution_CountinuousVariable(List<double> L, double StartingPoint, double Step)
        {

            List<Interval> ListOfIntervals = new List<Interval>();

            // Crate and insert first interval
            Interval Interval0 = new Interval();
            Interval0.LowerInclusiveBound = StartingPoint;
            Interval0.Step = Step;
            ListOfIntervals.Add(Interval0);

            // insertion of values
            foreach (var d in L)
            {
                if (double.IsNaN(d))
                    continue;

                bool ValueInserted = false;
                // if it's in one of the existing intervals, insert it there
                foreach (var I in ListOfIntervals)
                {
                    if (I.containsValue(d))
                    {
                        I.Count += 1;
                        ValueInserted = true;
                        break;
                    }
                }
                if (ValueInserted != true)
                {
                    // if it's less than the lower bound of the first, add new suitable interval in the beginning
                    if (d < ListOfIntervals[0].LowerInclusiveBound)
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval();
                            I.LowerInclusiveBound = ListOfIntervals[0].LowerInclusiveBound - Step;
                            I.Step = Step;

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Insert(0, I);
                        }
                    }
                    else if (d >= (ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step))
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval
                            {
                                LowerInclusiveBound = ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step,
                                Step = Step
                            };

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Add(I);
                        }
                    }
                    else
                    {
                        throw new Exception("Not Accepted value");
                    }
                }
            }

            // set relative frequencies and percentages
            foreach (var I in ListOfIntervals)
            {
                I.RelativeFrequency = (double)I.Count / L.Count();
                I.Percentage = I.RelativeFrequency * 100;

            }

            return ListOfIntervals;
        }

        private void addPaddingIntervals(List<Interval> L, double MaxValue)
        {
            Interval LastInterval = L[L.Count - 1];
            while ((LastInterval.LowerInclusiveBound + LastInterval.Step) < MaxValue)
            {
                Interval I = new Interval
                {
                    LowerInclusiveBound = L[L.Count - 1].LowerInclusiveBound + LastInterval.Step,
                    Step = LastInterval.Step
                };

                L.Add(I);
                LastInterval = I;
            }
        }


    }
}
