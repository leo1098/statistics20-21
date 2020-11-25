using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace _13_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initGraphics();
        }

        string nl = Environment.NewLine;
        Graphics g1, g2;
        Bitmap b1, b2;
        Random r = new Random();
        ResizableRectangle ViewPort1, ViewPort2;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;
        double p, mBern, eps;
        int nBern, jBern, nRade, jRade, mRade;
        List<Bernoulli> Bernoullis;
        List<Rademacher> Rademachers;


        // ------------ HANDLERS ------------------
        private void printSimulationButton_Click(object sender, EventArgs e)
        {
            // get input values
            nBern = (int)this.numericNBern.Value;
            jBern = (int)this.numericJBern.Value;
            mBern = (double)this.numericMBern.Value;
            p = (double)this.numericP.Value;
            eps = (double)this.numericEps.Value;
            Bernoullis = new List<Bernoulli>();
            Rademachers = new List<Rademacher>();


            // check on j
            if (jBern > nBern)
            {
                MessageBox.Show("J cannot be bigger than n!");
                return;
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = nBern;
            MaxY_Win = 1;
            ViewPort1 = new ResizableRectangle(this.bernoulliPictureBox, b1, g1, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(50, 45, 700, 300));
            ViewPort1.ModifiedRect += drawChartsBern;


            // creation of distributions
            for (int i = 0; i < mBern; i++)
                Bernoullis.Add(new Bernoulli(p, nBern, r.Next()));

            for (int i = 0; i < mBern; i++)
                Rademachers.Add(new Rademacher(nBern, r.Next()));

            drawChartsBern();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            nRade = (int)this.numericNRade.Value;
            jRade = (int)this.numericJRade.Value;
            mRade = (int)this.numericMRade.Value;
            Rademachers = new List<Rademacher>();


            // check on j
            if (jRade > nRade)
            {
                MessageBox.Show("J cannot be bigger than n!");
                return;
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = -120;
            MaxX_Win = nRade;
            MaxY_Win = 120;
            ViewPort2 = new ResizableRectangle(this.rademacherPictureBox, b1, g1, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(50, 45, 700, 300));
            ViewPort2.ModifiedRect += drawChartsRade;


            // creation of distributions
            for (int i = 0; i < mRade; i++)
                Rademachers.Add(new Rademacher(nRade, r.Next()));

            drawChartsRade();
        }

        // -------------GRAPHICS FUNCTIONS----------------

        private void initGraphics()
        {
            b1 = new Bitmap(this.bernoulliPictureBox.Width, this.bernoulliPictureBox.Height);
            g1 = Graphics.FromImage(b1);
            g1.SmoothingMode = SmoothingMode.HighQuality;

            b2 = new Bitmap(this.rademacherPictureBox.Width, this.rademacherPictureBox.Height);
            g2 = Graphics.FromImage(b2);
            g2.SmoothingMode = SmoothingMode.HighQuality;

        }

        private void drawChartsBern()
        {
            g1.Clear(Color.Gainsboro);

            drawAxis(ViewPort1, g1);

            drawBernoulliPaths();

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;
            drawHorizontalLine("p", p, pen, ViewPort1, g1);
            drawHorizontalLine("p+eps", p + eps, Pens.Brown, ViewPort1, g1);
            drawHorizontalLine("p-eps", p - eps, Pens.Brown, ViewPort1, g1);

            double Step = 0.05;
            double StartingPoint = ViewPort1.MinY_Win;

            List<double> BernAtStepN = new List<double>();
            List<Interval> FrequencyDistribution = new List<Interval>();
            // -------- histogram at time j ---------
            BernAtStepN = Bernoullis.Select(B => B.MeanDistribution[jBern -1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernAtStepN, StartingPoint, Step);
            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, 1);

            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(jBern-1, ViewPort1, g1, ReversedFrequencyDistribution);


            // -------- histogram at time n ---------
            BernAtStepN = Bernoullis.Select(B => B.MeanDistribution[nBern -1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernAtStepN, StartingPoint, Step);
            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, 1);

            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();
            drawVerticalHistogram(nBern-1, ViewPort1, g1, ReversedFrequencyDistribution);


            this.bernoulliPictureBox.Image = b1;
        }

        private void drawChartsRade()
        {
            g2.Clear(Color.Gainsboro);

            drawAxis(ViewPort2, g2);

            drawRademacherPaths();

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;
            drawHorizontalLine("0", 0, pen, ViewPort2, g2);


            double Step = 5;
            double StartingPoint = ViewPort2.MinY_Win;

            List<double> RadesAtStepN = Rademachers.Select(R => R.ListOfValues[jRade-1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort2.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(jRade - 1, ViewPort2, g2, ReversedFrequencyDistribution);



            RadesAtStepN = Rademachers.Select(R => R.ListOfValues[nRade - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort2.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(nRade - 1, ViewPort2, g2, ReversedFrequencyDistribution);


            this.rademacherPictureBox.Image = b2;
        }

        private void drawBernoulliPaths()
        {
            //draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                    B.drawPath(ViewPort1, g1);
                    this.bernoulliPictureBox.Image = b1;
            }
        }

        private void drawRademacherPaths()
        {
            foreach (Rademacher R in Rademachers)
            {
                R.drawPath(ViewPort2, g2);
                this.rademacherPictureBox.Image = b2;
            }
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

        private void drawVerticalLine(string label, double x, Pen pen, ResizableRectangle VP, Graphics g)
        {
            // Y axis
            g.DrawLine(
                pen,
                VP.viewport_X(VP.MinX_Win + x),
                VP.viewport_Y(VP.MinY_Win),
                VP.viewport_X(VP.MinX_Win + x),
                VP.viewport_Y(VP.MaxY_Win)
                );

            g.DrawString(
                label,
                DefaultFont,
                Brushes.Black,
                VP.viewport_X(VP.MinX_Win + x),
                VP.viewport_Y(VP.MinY_Win) + g.MeasureString(label, DefaultFont).Height
                );
        }

        private void drawVerticalHistogram(int n, ResizableRectangle V, Graphics g, List<Interval> FreqDistr)
        {
            // draw proportionate rectangles 
            double BarWidth = (double)V.R.Height / FreqDistr.Count;
            double BarMaxHeight = V.R.Width * 0.4;
            int BarNum = 0;
            foreach (Interval I in FreqDistr)
            {
                float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                RectangleF R = new RectangleF(
                    V.viewport_X(MinX_Win + n),
                    (float)(V.R.Y + (BarNum * BarWidth)),
                    (float)BarHeight,
                    (float)BarWidth
                    );

                SolidBrush B = new SolidBrush(Color.FromArgb(200, 253, 185, 39));
                Pen pen = new Pen(Color.FromArgb(200, 85, 37, 130));
                g.DrawRectangles(pen, new[] { R });
                g.FillRectangle(B, R);                
                BarNum++;
            }

            // count how many means are inside the strip at step n
            //int PathsInsideStrip = MeansAtStepN.Count(M => (M < p +eps) && (M > p - eps));
            //drawVerticalLine($"n = {n+1}\npaths in strip = {PathsInsideStrip}", n, Pens.Purple);


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


        // -------------STATS FUNCTIONS----------------
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



    }
}
