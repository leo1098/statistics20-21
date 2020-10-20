using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Project
{
    public partial class Form1 : Form
    {
        string nl = Environment.NewLine;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double N1 = 1E100;
            this.richTextBox1.AppendText($"N1 before adding : {N1}" + nl);

            for (int i = 0; i < 1E9; i++)
            {
                N1 += 1;
                // this.richTextBox1.AppendText($"N1 after adding : {N1}" + nl);
            }
            this.richTextBox1.AppendText($"N1 after adding : {N1}" + nl);
        }
    }
}
