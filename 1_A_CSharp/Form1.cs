using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_A_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox.Text = "";
        }

        private void putTextButton_Click(object sender, EventArgs e)
        {
            this.textBox.Text = "A simple string :)";
        }

        private void textBox_MouseEnter(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            this.textBox.BackColor = randomColor;
        }

        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            this.textBox.BackColor = Color.White;
        }
    }
}
