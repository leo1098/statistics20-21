using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class Rademacher
    {

        private Random r;
        private int n;
        private double seed;
        public List<DataPoint> RandomWalk;
        Pen pen;

        public Rademacher(int n, int seed)
        {
            this.r = new Random(seed);
            this.n = n;
            RandomWalk = generateRandomWalkListOfValues();
            this.pen = new Pen(Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));

        }

        private List<DataPoint> generateRandomWalkListOfValues()
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

        private int sample()
        {
            double k = r.NextDouble();
            

            if (k <= 0.5) 
                return 1;
            else
                return -1;
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
    }
}
