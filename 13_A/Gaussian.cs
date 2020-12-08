using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    // this class represent a normal N(0,1)
    class Gaussian
    {
        private Random r;
        private int n;
        private double sigma;
        public List<DataPoint> RandomWalk;
        Pen pen;

        public Gaussian(int n, int seed, double sigma)
        {
            this.r = new Random(seed);
            this.n = n;
            this.sigma = sigma;
            RandomWalk = generateRandomWalkListOfValues();
            this.pen = new Pen(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));
        }

        public double sample(double mean, double stddev)
        {
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            double x1 = 1 - r.NextDouble();
            double x2 = 1 - r.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            //return y1 * stddev + mean;
            // stdev = 1, mean = 0
            return y1;
        }

        private List<DataPoint> generateRandomWalkListOfValues()
        {
            // P(t) = P(t-1) + Random step(t) where Random step(t) is: σ * sqrt(1/n) * N(0,1)
            List<DataPoint> L = new List<DataPoint>();
            double Y = 0;

            for (int i = 0; i < n; i++)
            {
                Y += sigma * Math.Sqrt((double)1/n) * sample(0,1);
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

        public double getMinRandomWalk()
        {
            return RandomWalk.Min(DP => DP.Y);
        }

        public double getMaxRandomWalk()
        {
            return RandomWalk.Max(DP => DP.Y);
        }
    }
}
