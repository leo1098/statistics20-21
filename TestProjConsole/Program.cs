﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            Random r = new Random();
            List<double> L = new List<double>();
            double lambda = 4;

            for (int i = 0; i < 1000; i++)
            {
                double n = -(Math.Log(1 - r.NextDouble()) / lambda);
                L.Add(n);
            }
        }
        static double computeOnlineVariance(List<double> L)
        {
            double SSn, avg_act, avg_prev;
            SSn = avg_prev = avg_act = 0.0;

            for (int i = 1; i <= L.Count; i++)
            {
                avg_prev = computeOnlineMean(L.Take(i - 1).ToList());
                avg_act = computeOnlineMean(L.Take(i).ToList());

                SSn = SSn + (L[i-1] - avg_prev) * (L[i-1] - avg_act);
            }

            return (SSn / L.Count);
        }

        static  double computeOnlineMean(List<double> L)
        {
            // Computation of arithmetic meanusing the Knuth Formula

            double avg = 0.0;
            int i = 0;
            foreach (var d in L)
            {
                if (double.IsNaN(d))
                    continue;
                // update avg value
                i += 1;
                avg += (d - avg) / i;
            }

            return avg;
        }

        static double computeOnlineCovariance (List<double> Y, List<double> X)
        {
            double SCn, avg_act_x, avg_prev_y;
            SCn = avg_prev_y = avg_act_x = 0.0;
            //List<double> XColumn = D.Select(row => row[0]).ToList();
            //List<double> YColumn = D.Select(row => row[1]).ToList();

            for (int i = 1; i <= Y.Count; i++)
            {
                avg_prev_y = computeOnlineMean(Y.Take(i - 1).ToList());
                avg_act_x = computeOnlineMean(X.Take(i).ToList());

                SCn = SCn + (X[i - 1] - avg_act_x) * (Y[i - 1] - avg_prev_y);
            }

            return (SCn) / Y.Count;
        }

    }
}
