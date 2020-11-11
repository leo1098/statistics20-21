using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace _11_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initGraphics();
        }

        // ------------- INTEGRAL STUFF --------------
        public delegate double Function(double x);

        Function F = (x) => { return (300 - x); };
        Function F_1 = (x) => { return 300 - x; };
        double StartPoint = 0;
        double EndPoint = 300;
        int nSteps = 1;

        // ------------ GRAPHICS STUFF -------------
        Rectangle ViewPort = new Rectangle(20, 20, 300, 300);
        Graphics g1, g2;
        Bitmap b1, b2;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, Range_X, Range_Y;


        private void button1_Click(object sender, EventArgs e)
        {


            MinX_Win = 0;
            MaxX_Win = 300;

            MinY_Win = Math.Min(F(MinX_Win), F(MaxX_Win));
            MaxY_Win = Math.Max(F(MinX_Win), F(MaxX_Win));

            Range_X = MaxX_Win - MinX_Win;
            Range_Y = MaxY_Win - MinY_Win;


            drawFunction(F);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double riemann = RiemannSum(F, StartPoint, EndPoint, nSteps);
            double lebesgue = LebesgueSum(F, F_1, StartPoint, EndPoint, nSteps);

            this.riemannSumTextBox.Clear();
            this.riemannSumTextBox.AppendText(riemann.ToString());
            this.lebesgueSumTextBox.Clear();
            this.lebesgueSumTextBox.AppendText(lebesgue.ToString());
            this.stepsTextBox.Clear();
            this.stepsTextBox.AppendText(nSteps.ToString());

            nSteps++;
        }

        bool counting = true;
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.;
            //timer1.Start();
        }

        // -------------- INTEGRALS ----------------
        private double LebesgueSum(Function func, Function invfunc, double start, double end, int steps)
        {
            double MaxY = Math.Max(func(start), func(end));
            double MinY = Math.Min(func(start), func(end));

            List<RectangleF> LebesgueRectangles = new List<RectangleF>();

            double diff = MaxY - MinY;
            double df = diff / (double)steps;
            double sum = 0;
            int i;
            for (i = 0; i < steps; i++)
            {
                float YForRect = (float)F_1(MaxY + df);
                RectangleF R = new RectangleF(
                    viewport_X(MinX_Win),
                    viewport_Y(MaxY - (double)i * df),
                    (float)(F_1(MaxY - (double)i * df)), //width
                    (float)df
                    );

                LebesgueRectangles.Add(R);
                sum += F_1(MaxY - (double)i * df) * df;
            }
            //drawFunction(func);
            g2.DrawRectangles(Pens.Blue, LebesgueRectangles.ToArray());
            this.pictureBoxLebesgue.Image = b2;

            return sum;

        }
        private double RiemannSum(Function func, double start, double end, int steps)
        {
            List<RectangleF> RiemannRectangles = new List<RectangleF>();
            double diff = end - start;
            double dx = diff / (double)steps;
            double sum = 0;
            int i;
            for (i = 0; i < steps; i++)
            {
                RectangleF R = new RectangleF(
                    viewport_X(start + (double)i * dx),
                    viewport_Y(func(start + (double)(i + 1) * dx)),
                    (float)dx,
                    (float)func(start + (double)(i + 1) * dx));

                RiemannRectangles.Add(R);
                sum += func(start + (double)(i - 1) * dx) * dx;
            }
            drawFunction(func);
            g1.DrawRectangles(Pens.Blue, RiemannRectangles.ToArray());
            this.pictureBoxRiemann.Image = b1;

            return sum;
        }


        //------------------GRAPHICS-----------------
        private void initGraphics()
        {
            b1 = new Bitmap(this.pictureBoxRiemann.Width, this.pictureBoxRiemann.Height);
            b2 = new Bitmap(this.pictureBoxLebesgue.Width, this.pictureBoxLebesgue.Height);
            g1 = Graphics.FromImage(b1);
            g2 = Graphics.FromImage(b2);
            g1.SmoothingMode = SmoothingMode.AntiAlias;
            g2.SmoothingMode = SmoothingMode.AntiAlias;
        }
        private float viewport_X(double World_X)
        {
            return (float)(ViewPort.Left + (World_X - MinX_Win) * (ViewPort.Width / Range_X));
        }

        private float viewport_Y(double World_Y)
        {
            return (float)(ViewPort.Top + ViewPort.Height - (World_Y - MinY_Win) * (ViewPort.Height / Range_Y));
        }

        private void drawAxis()
        {
            Pen p = new Pen(Color.Black);
            p.EndCap = LineCap.ArrowAnchor;

            // X axis
            g1.DrawLine(
                p,
                viewport_X(MinX_Win),
                viewport_Y(MinY_Win),
                viewport_X(MaxX_Win),
                viewport_Y(MinY_Win));

            g2.DrawLine(
                p,
                viewport_X(MinX_Win),
                viewport_Y(MinY_Win),
                viewport_X(MaxX_Win),
                viewport_Y(MinY_Win));

            // Y axis
            g1.DrawLine(
                p,
                viewport_X(MinX_Win),
                viewport_Y(MinY_Win),
                viewport_X(MinX_Win),
                viewport_Y(MaxY_Win)
                );

            g2.DrawLine(
                p,
                viewport_X(MinX_Win),
                viewport_Y(MinY_Win),
                viewport_X(MinX_Win),
                viewport_Y(MaxY_Win)
                );
            g1.DrawRectangle(Pens.Red, ViewPort);
            g2.DrawRectangle(Pens.Red, ViewPort);

            this.pictureBoxRiemann.Image = b1;
            this.pictureBoxLebesgue.Image = b2;
        }

        private void drawFunction(Function F)
        {
            g1.Clear(Color.LightGray);
            g2.Clear(Color.LightGray);

            drawAxis();

            List<PointF> Points = new List<PointF>();

            for (int x = (int)MinX_Win; x < MaxX_Win; x++)
            {
                double res = F(x);
                Points.Add(new PointF(viewport_X(x), viewport_Y(res)));
            }

            g1.DrawLines(Pens.Black, Points.ToArray());
            g2.DrawLines(Pens.Black, Points.ToArray());
            this.pictureBoxRiemann.Image = b1;
            this.pictureBoxLebesgue.Image = b2;
        }
    }
}
