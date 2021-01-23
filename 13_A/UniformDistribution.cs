using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class UniformDistribution
    {
        double minValue;
        double maxValue;
        Random Random;

        public UniformDistribution(double low, double high, int seed)
        {
            this.minValue = low;
            this.maxValue = high;
            this.Random = new Random(seed);
            
        }

        public List<double> createlistOfPoints(int size)
        {
            List<double> L = new List<double>();

            for (int i = 0; i < size; i++)
            {
                L.Add(sample());
            }

            return L;
        }


        public double sample()
        {
            return Random.NextDouble() * (maxValue - minValue) + minValue;
        }

    }

}
