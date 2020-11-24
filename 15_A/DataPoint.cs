using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_A
{
    class DataPoint
    {
        public double X;
        public double Y;

        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public string ToString()
        {
            return $"[{X} - {Y}]";
        }
    }
}
