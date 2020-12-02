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
        Graphics g1, g2, g3, g4, g5;
        Bitmap b1, b2, b3, b4, b5;
        Random r = new Random();
        ResizableRectangle ViewPort1, ViewPort2, ViewPort3, ViewPort4, ViewPort5;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;
        double p, mBern, eps;
        int nBern, jBern, nRade, jRade, mRade, nBernRW, mBernRW, jBernRW;



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
            Bernoullis.Clear();
            for (int i = 0; i < mBern; i++)
                Bernoullis.Add(new Bernoulli(p, nBern, r.Next()));


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
            ViewPort2 = new ResizableRectangle(this.rademacherPictureBox, b2, g2, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(50, 45, 700, 300));
            ViewPort2.ModifiedRect += drawChartsRade;


            // creation of distributions
            for (int i = 0; i < mRade; i++)
                Rademachers.Add(new Rademacher(nRade, r.Next()));

            drawChartsRade();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // get input values
            nBernRW = (int)this.numericNBernRW.Value;
            jBernRW = (int)this.numericJBernRW.Value;
            mBernRW = (int)this.numericMBernRW.Value;
            double lambda = (double)this.numericLambda.Value;
            p = (double)(lambda / nBernRW);
            Bernoullis = new List<Bernoulli>();


            // check on lambda
            if (lambda > nBernRW)
            {
                MessageBox.Show("Lambda cannot be bigger than n!");
                return;
            }

            // check on j
            if (jBernRW > nBernRW)
            {
                MessageBox.Show("J cannot be bigger than n!");
                return;
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = nBernRW;
            MaxY_Win = nBernRW*p*1.3;
            ViewPort3 = new ResizableRectangle(this.bernoulliRWPictureBox, b3, g3, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(80, 30, 300, 300));
            ViewPort3.ModifiedRect += drawChartsBernRW;




            // creation of distributions
            Bernoullis.Clear();
            for (int i = 0; i < mBernRW; i++)
                Bernoullis.Add(new Bernoulli(p, nBernRW, r.Next()));

            drawChartsBernRW();

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = 300;
            MaxY_Win = 300;
            ViewPort4 = new ResizableRectangle(this.bernoulliJumpPictureBox1,
                b4, g4,
                MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(20, 10, (float)(bernoulliJumpPictureBox1.Width*0.8), (float)(bernoulliJumpPictureBox1.Height * 0.8)));
            ViewPort4.ModifiedRect += drawJumpDistributions;

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = 300;
            MaxY_Win = 300;
            ViewPort5 = new ResizableRectangle(this.bernoulliJumpPictureBox2,
                b5, g5,
                MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(20, 10, (float)(bernoulliJumpPictureBox2.Width * 0.8), (float)(bernoulliJumpPictureBox2.Height * 0.8)));
            ViewPort5.ModifiedRect += drawJumpDistributions;

            drawJumpDistributions();


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

            b3 = new Bitmap(this.bernoulliRWPictureBox.Width, this.bernoulliRWPictureBox.Height);
            g3 = Graphics.FromImage(b3);
            g3.SmoothingMode = SmoothingMode.HighQuality;

            b4 = new Bitmap(this.bernoulliJumpPictureBox1.Width, this.bernoulliJumpPictureBox1.Height);
            g4 = Graphics.FromImage(b4);
            g4.SmoothingMode = SmoothingMode.HighQuality;

            b5 = new Bitmap(this.bernoulliJumpPictureBox2.Width, this.bernoulliJumpPictureBox2.Height);
            g5 = Graphics.FromImage(b5);
            g5.SmoothingMode = SmoothingMode.HighQuality;

        }

        private void drawChartsBern()
        {
            g1.Clear(Color.Gainsboro);

            //drawAxis(ViewPort1, g1, "trials", "");
            ViewPort1.drawAxis("trials", "");

            drawBernoulliPaths();

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;
            ViewPort1.drawHorizontalLine("p", p, pen);
            ViewPort1.drawHorizontalLine("p+eps", p + eps, Pens.Brown);
            ViewPort1.drawHorizontalLine("p-eps", p - eps, Pens.Brown);

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

            // count how many means are inside the strip at step n
            int PathsInsideStrip = BernAtStepN.Count(M => (M < p + eps) && (M > p - eps));
            ViewPort1.drawVerticalLine($"n = {jBern }\npaths in strip = {PathsInsideStrip}", jBern-1, Pens.Purple);

            drawVerticalHistogram(jBern-1, ViewPort1, ReversedFrequencyDistribution);


            // -------- histogram at time n ---------
            BernAtStepN = Bernoullis.Select(B => B.MeanDistribution[nBern -1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernAtStepN, StartingPoint, Step);
            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, 1);

            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            // count how many means are inside the strip at step n
            PathsInsideStrip = BernAtStepN.Count(M => (M < p + eps) && (M > p - eps));
            ViewPort1.drawVerticalLine($"n = {nBern}\npaths in strip = {PathsInsideStrip}", nBern - 1, Pens.Purple);

            drawVerticalHistogram(nBern-1, ViewPort1, ReversedFrequencyDistribution);
        }

        private void drawChartsRade()
        {
            g2.Clear(Color.Gainsboro);

            ViewPort2.drawAxis("num of steps", "Random Walk");

            drawRademacherPaths();

            double Step = 5;
            double StartingPoint = ViewPort2.MinY_Win;

            List<double> RadesAtStepN = Rademachers.Select(R => R.RandomWalk[jRade-1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort2.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(jRade - 1, ViewPort2, ReversedFrequencyDistribution);



            RadesAtStepN = Rademachers.Select(R => R.RandomWalk[nRade - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort2.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(nRade - 1, ViewPort2, ReversedFrequencyDistribution);

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;

            ViewPort2.drawHorizontalLine("0", 0, pen);

        }

        private void drawChartsBernRW()
        {
            ViewPort3.g.Clear(Color.Gainsboro);

            ViewPort3.drawAxis("num of steps", "Random Walk");

            drawBernoulliRWPaths();

            double Step = 3;
            double StartingPoint = ViewPort3.MinY_Win;

            List<double> BernsRWAtStepN = Bernoullis.Select(B => B.RandomWalk[jBernRW - 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernsRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort3.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(jBernRW - 1, ViewPort3, ReversedFrequencyDistribution);



            BernsRWAtStepN = Bernoullis.Select(B => B.RandomWalk[nBernRW - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernsRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort3.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(nBernRW - 1, ViewPort3, ReversedFrequencyDistribution);

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;

            ViewPort3.drawHorizontalLine("0", 0, pen);

        }

        private void drawJumpDistributions()
        {
            // this section will print the distribuion of distance betwee consesutive jumps (exponential)
            ViewPort4.g.Clear(Color.Gainsboro);
            ViewPort4.drawAxis("distance","f");
       
            List<double> L = new List<double>();
            L.Clear();

            foreach (Bernoulli B in Bernoullis)
                L.AddRange(B.distancesBetweenConsecutiveJumps());

            List<Interval> ConsecutiveJumpDistr = UnivariateDistribution_CountinuousVariable(L, 1, 1);
            drawHorizontalHistogram(0, ViewPort4, ConsecutiveJumpDistr);

            // this section will print the distribution of the distance from each jump from the origin (uniform)
            ViewPort5.g.Clear(Color.Gainsboro);
            ViewPort5.drawAxis("distance", "f");

            List<double> L2 = new List<double>();
            L2.Clear();

            foreach (Bernoulli B in Bernoullis)
                L2.AddRange(B.distancesJumpsFromOrigin());

            List<Interval> FromOriginJumpDistr = UnivariateDistribution_CountinuousVariable(L2, 1, 10);
            drawHorizontalHistogram(0, ViewPort5, FromOriginJumpDistr);
        }

        private void drawBernoulliPaths()
        {
            //draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                B.drawSampleMeanPath(ViewPort1);
            }
        }

        private void drawBernoulliRWPaths()
        {
            //draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                B.drawRandomWalkPath(ViewPort3);
            }
        }

        private void drawRademacherPaths()
        {
            foreach (Rademacher R in Rademachers)
            {
                R.drawRandomWalkPath(ViewPort2);
            }
        }

        private void drawVerticalHistogram(int n, ResizableRectangle V, List<Interval> FreqDistr)
        {
            // draw proportionate rectangles 
            Graphics g = V.g;
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

            V.PictureBox.Image = V.b;
        }

        private void drawHorizontalHistogram(int n, ResizableRectangle V, List<Interval> FreqDistr)
        {
            // draw proportionate rectangles 
            Graphics g = V.g;
            double BarWidth = (double)V.R.Width / FreqDistr.Count;
            double max = FreqDistr.Max(I => I.RelativeFrequency);
            double BarMaxHeight = V.R.Height/FreqDistr.Max(I => I.RelativeFrequency);
            int BarNum = 0;
            foreach (Interval I in FreqDistr)
            {
                float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                RectangleF R = new RectangleF(
                    (float)(V.R.X + (BarNum * BarWidth)),
                    (float)(V.R.Y + V.R.Height - (BarMaxHeight * I.RelativeFrequency)),
                    (float)BarWidth + n,
                    BarHeight
                    );

                SolidBrush B = new SolidBrush(Color.FromArgb(200, 253, 185, 39));
                Pen pen = new Pen(Color.FromArgb(200, 85, 37, 130));
                g.DrawRectangles(pen, new[] { R });
                g.FillRectangle(B, R);                
                BarNum++;
            }

            V.PictureBox.Image = V.b;
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
