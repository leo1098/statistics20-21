using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _6_A
{
    public partial class Form1 : Form
    {
        string nl = Environment.NewLine;
        Random r = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.richTextBox2.Clear();

            // generating a lot of random big ints
            List<double> Integers = GenerateIntegers();

            // print results in text boxes
            this.richTextBox1.AppendText(naiveIntegerMean(Integers).ToString());
            this.richTextBox2.AppendText(knuthIntegerMean(Integers).ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox3.Clear();
            this.richTextBox4.Clear();

            List<double> Doubles = GenerateHugeDoubles();
            // print results in text boxes
            this.richTextBox3.AppendText(naiveDoubleMean(Doubles).ToString());
            this.richTextBox4.AppendText(knuthDoubleMean(Doubles).ToString());
        }


        private double naiveDoubleMean(List<double> numbers)
        {
            double avg = 0.0;
            double sum = 0;
            int count = 0;

            foreach (double n in numbers)
            {
                sum += n;
                count += 1;
            }

            avg = sum / count;

            return avg;
        }
        private double naiveIntegerMean(List<double> numbers)
        {
            double avg = 0.0;
            int sum = 0;
            int count = 0;

            foreach (int n in numbers)
            {
                sum += n;
                count += 1;
            }

            avg = (double)sum / count;

            return avg;
        }


        private double knuthDoubleMean(List<double> numbers)
        {
            double avg = 0.0;
            int count = 0;

            foreach (double n in numbers)
            {
                count += 1;
                avg += (n - avg) / count;
            }

            return avg;
        }
        private double knuthIntegerMean(List<double> numbers)
        {
            double avg = 0.0;
            int count = 0;

            foreach (int n in numbers)
            {
                count += 1;
                avg += (n - avg) / count;
            }

            return avg;
        }

        private List<double> GenerateIntegers()
        {
            // list to be returned
            List<double> L = new List<double>();

            for (int i = 0; i < 1000; i++)
                L.Add(r.Next(10000000, 200000000));

            return L;
        }
        private List<double> GenerateHugeDoubles()
        {
            // list to be returned
            List<double> L = new List<double>();

            for (int i = 0; i < 1000; i++)
                L.Add(r.NextDouble() * double.MaxValue);

            return L;
        }

    }
}
