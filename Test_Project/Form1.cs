using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Test_Project
{
    public partial class Form1 : Form
    {
        string nl = Environment.NewLine;
        Bitmap b;
        Graphics g;

        List<DataPoint> DataSet;
        Rectangle ViewPort;

        // RealWorld Window
        public double MinX_Win = -250;
        public double MinY_Win = -250;
        public double MaxX_Win = 250;
        public double MaxY_Win = 250;
        public double Range_X;
        public double Range_Y;

        public Form1()
        {
            InitializeComponent();
            initGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet = new List<DataPoint>();
            Range_X = MaxX_Win - MinX_Win;
            Range_Y = MaxY_Win - MinY_Win;

            // ViewPort
            ViewPort = new Rectangle(100, 50, 200, 200);

            // creation of dataset
            for (int i = -5; i <= 5; i++)
            {
                for (int j = -5; j <= 5; j++)
                {
                    DataPoint D = new DataPoint()
                    {
                        X = i * 50,
                        Y = j * 50
                    };
                    DataSet.Add(D);
                }
            }

            drawViewport();
        }

        private void drawViewport()
        {
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Red, ViewPort);

            foreach (DataPoint D in DataSet)
            {
                int X_View = viewport_X(D.X, MinX_Win, Range_X);
                int Y_View = viewport_Y(D.Y, MinY_Win, Range_Y);

                g.FillEllipse(Brushes.Black, new Rectangle(new Point(X_View - 2, Y_View - 2), new Size(4, 4)));
            }

            pictureBox1.Image = b;
        }

        private void initGraphics()
        {
            b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private int viewport_X(double World_X, double MinX, double Range_X)
        {
            return (int)(ViewPort.Left + (World_X - MinX) * (ViewPort.Width / Range_X));
        }

        private int viewport_Y(double World_Y, double MinY, double Range_Y)
        {
            return (int)(ViewPort.Top + ViewPort.Height - (World_Y - MinY) * (ViewPort.Height / Range_Y));
        }

        Rectangle ViewPort_MouseDown;
        Point Click_Point_Drag;
        bool Dragging;
        bool Resizing;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Click_Point_Drag = new Point(e.X, e.Y);

            if (ViewPort.Contains(Click_Point_Drag))
            {
                ViewPort_MouseDown = this.ViewPort;

                if (e.Button == MouseButtons.Left)
                {
                    Dragging = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Resizing = true;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                int Delta_X = e.X - Click_Point_Drag.X;
                int Delta_Y = e.Y - Click_Point_Drag.Y;

                ViewPort.X = ViewPort_MouseDown.X + Delta_X;
                ViewPort.Y = ViewPort_MouseDown.Y + Delta_Y;
            }
            else if (Resizing)
            {
                int Delta_X = e.X - Click_Point_Drag.X;
                int Delta_Y = e.Y - Click_Point_Drag.Y;

                ViewPort.Width = ViewPort_MouseDown.Width + Delta_X;
                ViewPort.Height = ViewPort_MouseDown.Height + Delta_Y;
            }
            drawViewport();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
            Resizing = false;
        }
    }
}
