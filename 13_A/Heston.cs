using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class Heston
    {
        int n;
        double mu, k, equilibrium, volOfVol;
        private Random r = new Random(Guid.NewGuid().GetHashCode());
        public List<DataPoint> RandomWalk;
        Pen pen;

        public Heston(int n, double mu, double k, double equilibrium, double volOfVol)
        {
            this.n = n;
            this.mu = mu;
            this.k = k;
            this.equilibrium = equilibrium;
            this.volOfVol = volOfVol;
            RandomWalk = generateRandomWalkListOfValues();
            this.pen = new Pen(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));
        }

        public double sampleNormal(double mean, double stddev)
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
            double Y = 1;
            double vol=0;

            for (int i = 0; i < n; i++)
            {
                //Y += Y * (mu * (double)1/n + Math.Sqrt(vol) * Math.Sqrt(1 / n) * sampleNormal(0, 1));
                Y *= Math.Exp((double)1/n * (mu - 0.5 * vol) + Math.Sqrt(Math.Abs(vol)) * Math.Sqrt((double)1 / n) * sampleNormal(0, 1));
                vol += (double)1/n * (equilibrium - vol)*k + volOfVol*Math.Sqrt(vol)*Math.Sqrt((double)1/n) * sampleNormal(0, 1);
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
