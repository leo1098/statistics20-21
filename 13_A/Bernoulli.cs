using System;
using System.Collections.Generic;

namespace _13_A
{
    class Bernoulli
    {
        private double p;
        private Random r;
        private int n;
        public List<DataPoint> Distribution;
        public List<DataPoint> MeanDistribution;

        public Bernoulli(double p, int numberOfsteps)
        {
            this.p = p;
            this.r = new Random();

            this.n = numberOfsteps;
            this.Distribution = generateDistribution();
            this.MeanDistribution = generateMeanDistribution();
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
    }
}
