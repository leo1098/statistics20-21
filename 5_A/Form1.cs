using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _5_A
{
    public partial class Form1 : Form
    {
        public Random r = new Random();
        string nl = System.Environment.NewLine;

        List<Athlete> ListOfAthletes = new List<Athlete>();

        public Form1()
        {
            InitializeComponent();
        }

        // --------------- HANDLERS --------
        private void button1_Click(object sender, EventArgs e)
        {
            // generate the dataset
            this.richTextBox1.Clear();
            this.ListOfAthletes.Clear();

            this.richTextBox1.AppendText("Dataset of Athletes competing in a marathon" + nl);
            this.richTextBox1.AppendText("___________________________________________" + nl);

            this.timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // computation of online arithmetic mean
            this.richTextBox2.Clear();
            double OnlineMean = computeOnlineMean(ListOfAthletes.Select((Athlete a) => a.FinishingTime).ToList()); // Passing a list of ages as doubles
            this.richTextBox2.AppendText("________________________" + nl);
            this.richTextBox2.AppendText("Arithmetic Mean: " + OnlineMean.ToString(".##") + nl);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.richTextBox3.Clear();
            // computing and printing frequency distribution
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = computeDiscreteFrequencyInterval(ListOfAthletes.Select((Athlete a) => a.FinishingTime).ToList());

            printFrequencyDistributionInterval(FrequencyDistribution);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // add an item to the dataset at each tick
            addAthlete(this.ListOfAthletes);

        }

        // ------------ FUNCTIONS -----------
        private List<Interval> computeDiscreteFrequencyInterval(List<double> L)
        {
            // define starting point and step
            double StartingPoint = 3.0;
            double Step = 1.0;
            List<Interval> ListOfIntervals = new List<Interval>();

            // Crate and insert first interval
            Interval Interval0 = new Interval();
            Interval0.LowerInclusiveBound = StartingPoint;
            Interval0.Step = Step;
            ListOfIntervals.Add(Interval0);

            // insertion of values
            foreach (var d in L)
            {
                bool ValueInserted = false;
                // if it's in one of the existing intervals, insert it there
                foreach (var I in ListOfIntervals)
                {
                    if (I.containsValue(d))
                    {
                        I.Count += 1;
                        ValueInserted = true;
                        break;
                    }
                }
                if (ValueInserted != true)
                {
                    // if it's less than the lower bound of the first, add new suitable interval in the beginning
                    if (d < ListOfIntervals[0].LowerInclusiveBound)
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval();
                            I.LowerInclusiveBound = ListOfIntervals[0].LowerInclusiveBound - Step;
                            I.Step = Step;

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Insert(0, I);
                        }
                    }
                    else if (d >= ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound)
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval();
                            I.LowerInclusiveBound = ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step;
                            I.Step = Step;

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Add(I);
                        }
                    }
                    else
                    {
                        throw new Exception("Not Accepted value");
                    }
                }
            }

            // set relative frequencies and percentages
            foreach (var I in ListOfIntervals)
            {
                I.RelativeFrequency = (double)I.Count / L.Count();
                I.Percentage = I.RelativeFrequency * 100;

            }

            return ListOfIntervals;
        }

        private void addAthlete(List<Athlete> L)
        {
            int MinAge = 20;
            int MaxAge = 65;

            // finishing time in hours
            double MinFinishingTime = 2.0;
            double MaxFinishingTime = 8.0;

            // Instantiate Athlete
            Athlete a = new Athlete();
            a.Age = r.Next(MinAge, MaxAge);
            a.FinishingTime = MinFinishingTime + (MaxFinishingTime - MinFinishingTime) * r.NextDouble();

            // Add item to list
            L.Add(a);

            // Print into the TextBox
            this.richTextBox1.AppendText(($"Athlete {L.Count}").PadRight(20) + ("Time: " + a.FinishingTime.ToString("#.##")).PadRight(20) + nl);

        }

        private double computeOnlineMean(List<double> L)
        {
            // Computation of arithmetic meanusing the Knuth Formula

            double avg = 0;
            int i = 0;
            foreach (var d in L)
            {
                // update avg value
                i += 1;
                avg += (d - avg) / i;
            }

            return avg;
        }


        private void printFrequencyDistributionInterval(List<Interval> L)
        {
            double tot = 0;
            int count = 0;

            this.richTextBox3.AppendText("Finishing time".PadRight(16) +
                                            "Num of Ath".PadRight(16) +
                                            "Rel Freq".PadRight(16) +
                                            "Perc".PadRight(16) + nl);
            this.richTextBox3.AppendText("_______________________" + nl);

            foreach (var I in L)
            {
                this.richTextBox3.AppendText($"[{I.LowerInclusiveBound}h - " +
                    $"{I.LowerInclusiveBound + I.Step}h]  --> ".PadRight(16) +
                    $"{I.Count}".PadRight(16) +
                    $"{I.RelativeFrequency:0.##}".PadRight(16) +
                    $"{I.Percentage:##.##} %".PadRight(16) + nl);
                tot += I.RelativeFrequency;
                count += I.Count;
            }
            this.richTextBox3.AppendText($"Sum of relative frequencies: {tot}" + nl);
            this.richTextBox3.AppendText($"Total units: {count}");
        }

    }
}
