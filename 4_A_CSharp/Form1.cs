using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_A_CSharp
{
    public partial class Form1 : Form
    {
        public Random r = new Random();
        string nl = System.Environment.NewLine;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            int NumOfAthletes = 120;

            // init
            this.richTextBox1.AppendText($"Dataset of {NumOfAthletes} Athletes competing in a Marathon" + nl);
            this.richTextBox1.AppendText("________________________" + nl + nl);
            this.richTextBox1.AppendText("Athlete Num".PadRight(20) + "Age ".PadRight(20) + nl);



            // creation of sample dataset
            List<Athlete> ListOfAthletes = generateDatasetAthletes(NumOfAthletes);

            // computation of online arithmetic mean
            double OnlineMean = computeOnlineMean(ListOfAthletes.Select((Athlete a) => (double)a.Age).ToList()); // Passing a list of ages as doubles
            this.richTextBox1.AppendText("________________________" + nl);
            this.richTextBox1.AppendText("Arithmetic Mean: " + OnlineMean.ToString(".##") + nl);

            // computation of distribution
            // pairs of (age, frequency)
            SortedDictionary<int, FrequencyItem> FrequencyDistribution;
            FrequencyDistribution = computeDiscreteFrequencyDistribution(ListOfAthletes.Select((Athlete a) => a.Age).ToList()); // Passing a list of ages as integers
            printFrequencyDistribution(FrequencyDistribution);
        }

        private void printFrequencyDistribution(SortedDictionary<int, FrequencyItem> F)
        {
            this.richTextBox1.AppendText("________________________" + nl);
            this.richTextBox1.AppendText("Age  Count  Rel  Per" + nl);

            double tot = 0;
            foreach (var Item in F)
            {
                this.richTextBox1.AppendText($"{Item.Key} -- " +
                    $"{Item.Value.Count} -- " +
                    $"{Item.Value.RelativeFrequency:.##} -- " +
                    $"{Item.Value.Percentage:.##} %" + nl);

                tot += Item.Value.RelativeFrequency;
            }
            this.richTextBox1.AppendText($"Sum of relative frequencies: {tot}" + nl);
        }

        private SortedDictionary<int, FrequencyItem> computeDiscreteFrequencyDistribution(List<int> ListOfUnits)
        {
            // Frequency Distribution to be returned
            SortedDictionary<int, FrequencyItem> FreqDistr = new SortedDictionary<int, FrequencyItem>();

            // count number of occurrencies
            foreach (var d in ListOfUnits)
            {
                // if the element in the list is already being counted, i increment the counter
                if (FreqDistr.ContainsKey(d))
                {
                    FreqDistr[d].Count += 1;
                }
                // otherwise i create the element in the dictionary
                else
                {
                    FreqDistr.Add(d, new FrequencyItem());
                }
            }

            // updating relative frequencies and percentages
            foreach (var Item in FreqDistr.Values)
            {
                Item.RelativeFrequency = Item.Count / (double)ListOfUnits.Count;
                Item.Percentage = Item.RelativeFrequency * 100;
            }

            return FreqDistr;
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

        private List<Athlete> generateDatasetAthletes(int numOfAthletes)
        {
            // List to be returned
            List<Athlete> L = new List<Athlete>();
            int MinAge = 18;
            int MaxAge = 65;

            // Populate the List
            for (int i = 0; i < numOfAthletes; i++)
            {
                // Instantiate Age
                Athlete a = new Athlete();
                a.Age = r.Next(MinAge, MaxAge);

                // Add item to list
                L.Add(a);

                // Print into the TextBox
                this.richTextBox1.AppendText(("Athlete " + (i + 1)).PadRight(20) + ("Age " + a.Age).PadRight(20) + nl);
            }

            return L;
        }
    }
}
