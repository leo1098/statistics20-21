using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _13_A
{
    class Bernoulli
    {
        private double p;
        private Random r;
        private int n;
        public List<DataPoint> Distribution;
        public List<DataPoint> MeanDistribution;
        public List<DataPoint> RandomWalk;
        private Pen pen;

        public Bernoulli(double p, int numberOfsteps, int seed)
        {
            this.p = p;
            this.r = new Random(seed);
            this.pen = new Pen(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));

            this.n = numberOfsteps;
            this.Distribution = generateDistribution();
            this.MeanDistribution = generateMeanDistribution();
            this.RandomWalk = generateRandomWalk();
        }


        private List<DataPoint> generateMeanDistribution() 
        {
            List<DataPoint> MeanD = new List<DataPoint>();
            double avg = 0;
            int i = 0;

            foreach (var D in this.Distribution)
            {
                i += 1;
                avg += (D.Y - avg) / i;
                DataPoint DP = new DataPoint(D.X, avg);
                MeanD.Add(DP);
            }


            return MeanD;
        }

        private List<DataPoint> generateDistribution()
        {
            List<DataPoint> D = new List<DataPoint>();

            for (int i = 0; i < n; i++)
            {
                DataPoint DP = new DataPoint(i, sample());
                D.Add(DP);
            }

            return D;
        }

        private int sample()
        {
            double n = r.NextDouble();

            if (n <= p) return 1;
            else return 0;
        }

        public void drawSampleMeanPath(ResizableRectangle ViewPort)
        {
            List<PointF> Points = new List<PointF>();

            // convert datapoints in points
            foreach (DataPoint DP in MeanDistribution)
            {
                Points.Add(new PointF(
                    ViewPort.viewport_X(DP.X),
                    ViewPort.viewport_Y(DP.Y))
                    );
            }

            ViewPort.g.DrawLines(pen, Points.ToArray());
            ViewPort.PictureBox.Image = ViewPort.b;
        }

        private List<DataPoint> generateRandomWalk()
        {
            List<DataPoint> L = new List<DataPoint>();
            double Y = 0;
            for (int i = 0; i < n; i++)
            {
                Y += sample();
                DataPoint DP = new DataPoint(i, Y);
                L.Add(DP);
            }

            return L;
        }

        public void drawRandomWalkPath(ResizableRectangle ViewPort)
        {
            List<PointF> Points = new List<PointF>();

            // convert datapoints in points
            foreach (DataPoint DP in RandomWalk)
            {
                Points.Add(new PointF(
                    ViewPort.viewport_X(DP.X),
                    ViewPort.viewport_Y(DP.Y))
                    );
            }

            ViewPort.g.DrawLines(pen, Points.ToArray());
            ViewPort.PictureBox.Image = ViewPort.b;

        }

        public List<double> consecutiveJumpsList()
        {
            // Returns a list of the times elapsed between consecutive jumps
            List<double> RWValues = RandomWalk.Select(Step => Step.Y).ToList();

            double PrevN = RWValues[0];
            double delta = 0;
            List<double> L = new List<double>();

            foreach (double N in RWValues)
            {
                if (N == PrevN)
                {
                    PrevN = N;
                    delta += 1;
                    continue;
                }
                else
                {
                    L.Add(delta);
                    delta = 1;
                    PrevN = N;
                }
            }

            return L;
        }

    }
}
