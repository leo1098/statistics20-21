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
        Graphics g;
        Bitmap b;
        Random r = new Random();
        ResizableRectangle ViewPort;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;
        double p, m, eps;
        int n, j;
        List<Bernoulli> Bernoullis;


        // ------------ HANDLERS ------------------
        private void printSimulationButton_Click(object sender, EventArgs e)
        {
            // get input values
            n = (int)this.numericN.Value;
            j = (int)this.numericJ.Value;
            m = (double)this.numericM.Value;
            p = (double)this.numericP.Value;
            eps = (double)this.numericEps.Value;
            Bernoullis = new List<Bernoulli>();

            // check on j
            if (j > n)
            {
                MessageBox.Show("J cannot be bigger than n!");
                return;
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = n;
            MaxY_Win = 1;
            ViewPort = new ResizableRectangle(this.chartPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(50, 45, 700, 300));
            ViewPort.ModifiedRect += drawCharts;


            // creation of distributions
            for (int i = 0; i < m; i++)
                Bernoullis.Add(new Bernoulli(p, n, r.Next()));

            drawCharts();

        }


        // -------------GRAPHICS FUNCTIONS----------------

        private void initGraphics()
        {
            b = new Bitmap(this.chartPictureBox.Width, this.chartPictureBox.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.HighQuality;

        }

        private void drawCharts()
        {
            g.Clear(Color.Gainsboro);

            drawAxis();

            drawPaths();

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;
            drawHorizontalLine("p", p, pen);
            drawHorizontalLine("p+eps", p + eps, Pens.Brown);
            drawHorizontalLine("p-eps", p - eps, Pens.Brown);

            drawVerticalHistogram(j-1, ViewPort);
            drawVerticalHistogram(n-1, ViewPort);


            this.chartPictureBox.Image = b;
        }

        private void drawPaths()
        {
            // draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                B.drawPath(ViewPort, g);
                this.chartPictureBox.Image = b;
            }
        }

        private void drawAxis()
        {
            Pen p = new Pen(Color.Black);
            p.EndCap = LineCap.ArrowAnchor;
            string Name_Y = "";
            string Name_X = "trials";

            // X axis
            g.DrawLine(
                p,
                ViewPort.viewport_X(MinX_Win),
                ViewPort.viewport_Y(MinY_Win),
                ViewPort.viewport_X(MaxX_Win),
                ViewPort.viewport_Y(MinY_Win));

            g.DrawString(Name_X, DefaultFont, Brushes.Black,
                ViewPort.viewport_X(MaxX_Win),
                (float)(ViewPort.viewport_Y(MinY_Win) + 0.05 * MaxY_Win));

            // Y axis
            g.DrawLine(
                p,
                ViewPort.viewport_X(MinX_Win),
                ViewPort.viewport_Y(MinY_Win),
                ViewPort.viewport_X(MinX_Win),
                ViewPort.viewport_Y(MaxY_Win + 0.1)
                );

            g.DrawString(
                Name_Y,
                DefaultFont,
                Brushes.Black,
                ViewPort.viewport_X(MinX_Win) - g.MeasureString(Name_Y, DefaultFont).Width,
                (float)(ViewPort.viewport_Y(MaxY_Win) + 0.05 * MaxX_Win));
        }

        private void drawHorizontalLine(string label, double y, Pen pen)
        {
            g.DrawLine(
                pen,
                ViewPort.viewport_X(MinX_Win),
                ViewPort.viewport_Y(MinY_Win + y),
                ViewPort.viewport_X(MaxX_Win),
                ViewPort.viewport_Y(MinY_Win + y));

            g.DrawString(
                label,
                DefaultFont,
                Brushes.Black,
                ViewPort.viewport_X(MinX_Win) - g.MeasureString(label, DefaultFont).Width,
                ViewPort.viewport_Y(MinY_Win + y)
                );
        }

        private void drawVerticalLine(string label, double x, Pen pen)
        {
            // Y axis
            g.DrawLine(
                pen,
                ViewPort.viewport_X(MinX_Win + x),
                ViewPort.viewport_Y(MinY_Win),
                ViewPort.viewport_X(MinX_Win + x),
                ViewPort.viewport_Y(MaxY_Win)
                );

            g.DrawString(
                label,
                DefaultFont,
                Brushes.Black,
                ViewPort.viewport_X(MinX_Win + x),
                ViewPort.viewport_Y(MinY_Win) + g.MeasureString(label, DefaultFont).Height
                );
        }

        private void drawVerticalHistogram(int n, ResizableRectangle V)
        {
            double Step = 0.05;
            double StartingPoint = MinY_Win;


            // create the dataset with the means at step n
            //List<double> MeanAtStepN = new List<double>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    MeanAtStepN.Add(r.NextDouble());
            //}
            List<double> MeanAtStepN = Bernoullis.Select(B => B.MeanDistribution[n].Y).ToList();
            List <Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(MeanAtStepN, StartingPoint, Step);

            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, Step);

            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            // draw proportionate rectangles 
            double BarWidth = (double)V.R.Height / ReversedFrequencyDistribution.Count;
            double BarMaxHeight = V.R.Width * 0.2;
            int BarNum = 0;
            foreach (Interval I in ReversedFrequencyDistribution)
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
            int PathsInsideStrip = MeanAtStepN.Count(M => (M < p +eps) && (M > p - eps));
            drawVerticalLine($"n = {n+1}\npaths in strip = {PathsInsideStrip}", n, Pens.Purple);


        }

        private void addPaddingIntervals(List<Interval> L, double s)
        {
            Interval LastInterval = L[L.Count - 1];
            while ((LastInterval.LowerInclusiveBound + LastInterval.Step) < 1)
            {
                Interval I = new Interval
                {
                    LowerInclusiveBound = L[L.Count - 1].LowerInclusiveBound + s,
                    Step = s
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
