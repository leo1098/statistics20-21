using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_A_CSharp
{
    class Interval
    {
        public int LowerInclusiveBound;
        public int Step;
        public int Count = 0;

        public double RelativeFrequency;
        public double Percentage;

        public bool containsValue(double v) => (v >= LowerInclusiveBound && v < (LowerInclusiveBound + Step));
    }
}
