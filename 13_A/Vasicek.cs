using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class Vasicek
    {
        private Random r = new Random(Guid.NewGuid().GetHashCode());
        private int n;
        private double stddev, equilibrium, k;
        public List<DataPoint> RandomWalk;
        Pen pen;

        public Vasicek(int n, double stddev, double equilibrium, double k)
        {
            this.n = n;
            this.stddev = stddev;
            this.equilibrium = equilibrium;
            this.k = k;
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
            return y1 * stddev + mean;
            //return y1;
        }

        private List<DataPoint> generateRandomWalkListOfValues()
        {
            // P(t) = P(t-1) + Random step(t) where Random step(t) is: σ * sqrt(1/n) * N(0,1)
            List<DataPoint> L = new List<DataPoint>();
            double Y = 0;

            for (int i = 0; i < n; i++)
            {
                Y += (double)1/n *(equilibrium - Y)*k + stddev* Math.Sqrt((double)1 / n) * sample(0, 1);
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

        public void drawBoundaries(ResizableRectangle ViewPort)
        {
            List<PointF> ExpectedValue = new List<PointF>();
            List<PointF> PositiveSD = new List<PointF>();
            List<PointF> NegativeSD = new List<PointF>();

            
            for (int i = 0; i < n; i++)
            {
                ExpectedValue.Add(new PointF(
                    ViewPort.viewport_X(i),
                    ViewPort.viewport_Y(expectedValue(i))));
                
                PositiveSD.Add(new PointF(
                    ViewPort.viewport_X(i),
                    ViewPort.viewport_Y(expectedValue(i) + standardDev(i))));

                NegativeSD.Add(new PointF(
                    ViewPort.viewport_X(i),
                    ViewPort.viewport_Y(expectedValue(i) - standardDev(i))));
            }

            ViewPort.g.DrawLines(Pens.Black, ExpectedValue.ToArray());
            ViewPort.g.DrawLines(Pens.Black, PositiveSD.ToArray());
            ViewPort.g.DrawLines(Pens.Black, NegativeSD.ToArray());
            ViewPort.PictureBox.Image = ViewPort.b;

        }

        private double expectedValue(double t)
        {
            return equilibrium + (0 - equilibrium) * Math.Exp(-k * t);
        }

        private double standardDev(double t)
        {
            return 2 * Math.Sqrt(stddev*stddev/(2*k)*(1 - Math.Exp(-2*k*t)));
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
