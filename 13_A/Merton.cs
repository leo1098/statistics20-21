﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class Merton
    {
        private Random r = new Random(Guid.NewGuid().GetHashCode());
        private int n;
        private double stddev, lambda, mean;
        public List<DataPoint> RandomWalk;
        Pen pen;

        public Merton(int n, double stddev, double lambda, double mean)
        {
            this.n = n;
            this.stddev = stddev;
            this.lambda = lambda;
            this.mean = mean;
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

        public double sampleLogNormal(double mean, double stddev)
        {
            
            return Math.Exp(mean + stddev * sampleNormal(0,1));
        }

        public double samplePoisson(int t)
        {
            return (Math.Pow(lambda, t) * Math.Exp(-lambda)) / factorial(t);
        }

        public double jumpSize(int t)
        {
            double sum = 0;
            double n = samplePoisson(t);
            for (int i = 0; i<n; i++)
            {
                sum += (sampleLogNormal(0, 1) - 1);
            }

            return sum;
        }

        private List<DataPoint> generateRandomWalkListOfValues()
        {
            // P(t) = P(t-1) + Random step(t) where Random step(t) is: σ * sqrt(1/n) * N(0,1)
            List<DataPoint> L = new List<DataPoint>();
            double Y = 0;

            for (int i = 0; i < n; i++)
            {
                Y = Y*(mean*1/n + stddev*Math.Sqrt(1/n)*sampleNormal(0,1) + jumpSize(i));
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

        public int factorial(int num)
        {
            Console.WriteLine(num);
            int f = 1;
            for (int i = 1; i <= num; i++)
                f = f * i;

            return f;

        }
    }
}
