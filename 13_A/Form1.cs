using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _13_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string nl = Environment.NewLine;
        Graphics g;
        Bitmap b;
        ResizableRectangle ViewPort;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, Range_X, Range_Y;
        double p, m;
        int n;
        List<Bernoulli> Bernoullis;

        private void printSimulationButton_Click(object sender, EventArgs e)
        {
            // get input values
            n = (int)this.numericN.Value;
            m = (double)this.numericM.Value;
            p = (double)this.numericP.Value;
            Bernoullis = new List<Bernoulli>();

            // set maximum values for graphics
            MinX_Win = -0.2;
            MinY_Win = -0.2;
            MaxX_Win = n + 0.2*n;
            MaxY_Win = 1 + 0.2;

            // create graphics objects
            b = new Bitmap(this.chartPictureBox.Width, this.chartPictureBox.Height);
            g = Graphics.FromImage(b);
            ViewPort = new ResizableRectangle(this.chartPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(10, 10, 600, 300));
            ViewPort.ModifiedRect += effe;
            g.DrawRectangles(Pens.Red, new[] { ViewPort.R });
            this.chartPictureBox.Image = b;
            

            // creation of distributions
            for (int i= 0; i < m; i++)
                Bernoullis.Add(new Bernoulli(p, n));

        }

        private void effe()
        {
            g.Clear(Color.Gainsboro);
            g.DrawRectangles(Pens.Red, new[] { ViewPort.R });
            this.chartPictureBox.Image = b;
        }
    }
}
