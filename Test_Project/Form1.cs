using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            Graphics dc = this.CreateGraphics();
            this.Show();
            Rectangle rect = new Rectangle(50, 30, 100, 100);
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Red,
             Color.Yellow, LinearGradientMode.BackwardDiagonal);
            dc.FillRectangle(lBrush, rect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            Form Form2 = new Form();
            Form2.MdiParent = this;
            Form2.Show();
        }
    }
}
