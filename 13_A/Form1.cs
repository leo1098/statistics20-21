using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
        Random r = new Random();
        ResizableRectangle ViewPort;
        double MinX_Win, MinY_Win, MaxX_Win, MaxY_Win;
        double p, eps;
        int n, j, m, drift, eq, k;
        double sigma, mu, psi;

        List<Bernoulli> Bernoullis;
        List<Rademacher> Rademachers;
        List<Gaussian> Gaussians;
        List<GBM> GBMs;
        List<Vasicek> Vasiceks;
        List<Heston> Hestons;


        // ------------ HANDLERS ------------------
            
        private void BernoulliSampleMeanButton_Click(object sender, EventArgs e)
        {
            initGraphics(this.bernoulliPictureBox);

            // get input values
            n = (int)this.numericNBern.Value;
            j = (int)this.numericJBern.Value;
            m = (int)this.numericMBern.Value;
            p = (double)this.numericP.Value;
            eps = (double)this.numericEps.Value;
            Bernoullis = new List<Bernoulli>();

            // check on j
            if (j > n)
            {
                this.numericJBern.Value = (int)this.numericNBern.Value / 2;
                //MessageBox.Show("J cannot be bigger than n!");
                j = n / 2;
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = n;
            MaxY_Win = 1;
            ViewPort = new ResizableRectangle(this.bernoulliPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(50, 45, 700, 300));
            ViewPort.ModifiedRect += drawChartsBern;


            // creation of distributions
            Bernoullis.Clear();
            for (int i = 0; i < m; i++)
                Bernoullis.Add(new Bernoulli(p, n, r.Next()));


            drawChartsBern();

        }

        private void RademacherRandomWalk_Click(object sender, EventArgs e)
        {
            initGraphics(this.rademacherPictureBox);

            n = (int)this.numericNRade.Value;
            j = (int)this.numericJRade.Value;
            m = (int)this.numericMRade.Value;
            Rademachers = new List<Rademacher>();


            // check on j
            if (j > n)
            {
                //MessageBox.Show("J cannot be bigger than n!");
                this.numericJRade.Value = (int)n / 2;
                j = n / 2;
            }

            // creation of distributions
            MinY_Win = MaxY_Win = 0;
            for (int i = 0; i < m; i++)
            {
                Rademacher R = new Rademacher(n, r.Next());
                if (R.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = R.getMaxRandomWalk();
                if (R.getMinRandomWalk() <= MinY_Win) MinY_Win = R.getMinRandomWalk();
                Rademachers.Add(R);
            }

            // set values for graphics
            MinX_Win = 0;
            //MinY_Win = -120;
            MaxX_Win = n;
            //MaxY_Win = 120;
            ViewPort = new ResizableRectangle(this.rademacherPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(50, 45, (float)(0.8 * this.gaussianPictureBox.Width), (float)(0.8 * this.gaussianPictureBox.Height)));
            ViewPort.ModifiedRect += drawChartsRade;



            drawChartsRade();
        }

        private void BernoulliRandomWalk_Click(object sender, EventArgs e)
        {
            initGraphics(this.bernoulliRWPictureBox);


            // get input values
            n = (int)this.numericNBernRW.Value;
            j = (int)this.numericJBernRW.Value;
            m = (int)this.numericMBernRW.Value;
            double lambda = (double)this.numericLambda.Value;
            p = (double)(lambda / n);
            Bernoullis = new List<Bernoulli>();


            // check on lambda
            if (lambda > n)
            {
                this.numericLambda.Value = (int)(n*0.65);
                lambda = n * 0.65;
                p = (double)(lambda / n);
                //MessageBox.Show("Lambda cannot be bigger than n!");

            }

            // check on j
            if (j > n)
            {
                this.numericJBernRW.Value = (int)n / 2;
                j = n / 2;
                //MessageBox.Show("J cannot be bigger than n!");
            }

            // creation of distributions
            MaxY_Win = 0;
            Bernoullis.Clear();
            for (int i = 0; i < m; i++)
            {
                Bernoulli B = new Bernoulli(p, n, r.Next());
                if (B.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = B.getMaxRandomWalk();
                Bernoullis.Add(B);
            }

            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = n;
            //MaxY_Win = nBernRW*p*1.3;
            ViewPort = new ResizableRectangle(this.bernoulliRWPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new RectangleF(30, 30, 430, 350));
            ViewPort.ModifiedRect += drawChartsBernRW;

            drawChartsBernRW();



            initGraphics(this.bernoulliJumpPictureBox1);
            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = 300;
            MaxY_Win = 300;
            ViewPort = new ResizableRectangle(this.bernoulliJumpPictureBox1,
                b, g,
                MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(20, 10, (float)(bernoulliJumpPictureBox1.Width*0.8), (float)(bernoulliJumpPictureBox1.Height * 0.8)));
            ViewPort.ModifiedRect += drawJumpDistributions1;

            drawJumpDistributions1();

            initGraphics(this.bernoulliJumpPictureBox2);
            // set values for graphics
            MinX_Win = 0;
            MinY_Win = 0;
            MaxX_Win = 300;
            MaxY_Win = 300;
            ViewPort = new ResizableRectangle(this.bernoulliJumpPictureBox2,
                b, g,
                MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(20, 10, (float)(bernoulliJumpPictureBox2.Width * 0.8), (float)(bernoulliJumpPictureBox2.Height * 0.8)));
            ViewPort.ModifiedRect += drawJumpDistributions2;

            drawJumpDistributions2();


        }

        private void GaussianRandomWalk_Click(object sender, EventArgs e)
        {
            initGraphics(this.gaussianPictureBox);

            n = (int)this.numericNGaussian.Value;
            j = (int)this.numericJGaussian.Value;
            m = (int)this.numericMGaussian.Value;
            sigma = (int)this.numericSigmaGaussian.Value;
            int drift = (int)this.numericDriftGaussian.Value;
            Gaussians = new List<Gaussian>();


            // check on j
            if (j > n)
            {
                this.numericJGaussian.Value = (int)n / 2;
                j = n / 2;
                //MessageBox.Show("J cannot be bigger than n!");
            }

            // creation of distributions
            MinY_Win = MaxY_Win = 0;
            for (int i = 0; i < m; i++)
            {
                Gaussian G = new Gaussian(n, r.Next(), sigma, drift);
                if (G.getMinRandomWalk() <= MinY_Win) MinY_Win = G.getMinRandomWalk();
                if (G.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = G.getMaxRandomWalk();
                Gaussians.Add(G);
            }

            // set values for graphics
            MinX_Win = 0;
            //MinY_Win = -sigmaGauss * Math.Sqrt(1/ (double)nGauss) * sigmaGauss ;
            MaxX_Win = n;
            //MaxY_Win = sigmaGauss * Math.Sqrt(1 / (double)nGauss)  * sigmaGauss;
            ViewPort = new ResizableRectangle(this.gaussianPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(50, 45, (float)(0.8 * this.gaussianPictureBox.Width), (float)(0.8 * this.gaussianPictureBox.Height)));
            ViewPort.ModifiedRect += drawChartsGauss;



            drawChartsGauss();

        }

        private void GBMRandomWalk_Click(object sender, EventArgs e)
        {
            initGraphics(this.GBMPictureBox);


            n = (int)this.numericNGBM.Value;
            j = (int)this.numericJGBM.Value;
            m = (int)this.numericMGBM.Value;
            sigma = (double)this.numericSigmaGBM.Value;
            drift = (int)this.numericDriftGBM.Value;
            GBMs = new List<GBM>();


            // check on j
            if (j > n)
            {
                this.numericJGaussian.Value = (int)n / 2;
                j = n / 2;
                //MessageBox.Show("J cannot be bigger than n!");
            }

            // creation of distributions
            MinY_Win = MaxY_Win = 0;
            for (int i = 0; i < m; i++)
            {
                GBM G = new GBM(n, sigma, drift);
                if (G.getMinRandomWalk() <= MinY_Win) MinY_Win = G.getMinRandomWalk();
                if (G.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = G.getMaxRandomWalk();
                GBMs.Add(G);
            }

            // set values for graphics
            MinX_Win = 0;
            //MinY_Win = -sigmaGauss * Math.Sqrt(1/ (double)nGauss) * sigmaGauss ;
            MaxX_Win = n;
            //MaxY_Win = sigmaGauss * Math.Sqrt(1 / (double)nGauss)  * sigmaGauss;
            ViewPort = new ResizableRectangle(this.GBMPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(50, 45, (float)(0.8 * this.GBMPictureBox.Width), (float)(0.8 * this.GBMPictureBox.Height)));
            ViewPort.ModifiedRect += drawChartsGBM;



            drawChartsGBM();
        }

        private void Vasicek_Click(object sender, EventArgs e)
        {
            initGraphics(this.VasicekPictureBox);


            n = (int)this.numericNVasicek.Value;
            j = (int)this.numericJVasicek.Value;
            m = (int)this.numericMVasicek.Value;
            sigma = (double)this.numericSigmaVasicek.Value;
            eq = (int)this.numericPVasicek.Value;
            k = (int)this.numericKVasicek.Value;
            Vasiceks = new List<Vasicek>();


            // check on j
            if (j > n)
            {
                this.numericJVasicek.Value = (int)n / 2;
                j = n / 2;
                //MessageBox.Show("J cannot be bigger than n!");
            }

            // creation of distributions
            MinY_Win = MaxY_Win = 0;
            for (int i = 0; i < m; i++)
            {
                Vasicek V = new Vasicek(n, sigma, eq, k);
                if (V.getMinRandomWalk() <= MinY_Win) MinY_Win = V.getMinRandomWalk();
                if (V.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = V.getMaxRandomWalk();
                Vasiceks.Add(V);
            }

            // set values for graphics
            MinX_Win = 0;
            //MinY_Win = -sigmaGauss * Math.Sqrt(1/ (double)nGauss) * sigmaGauss ;
            MaxX_Win = n;
            //MaxY_Win = sigmaGauss * Math.Sqrt(1 / (double)nGauss)  * sigmaGauss;
            ViewPort = new ResizableRectangle(this.VasicekPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(50, 45, (float)(0.8 * this.VasicekPictureBox.Width), (float)(0.8 * this.VasicekPictureBox.Height)));
            ViewPort.ModifiedRect += drawChartsVasicek;



            drawChartsVasicek();
        }

        private void Heston_click(object sender, EventArgs e)
        {
            initGraphics(this.HestonPictureBox);


            n = (int)this.numericNHeston.Value;
            j = (int)this.numericJHeston.Value;
            m = (int)this.numericMHeston.Value;
            mu = (double)this.numericMuHeston.Value;
            double k = (double)this.numericKHeston.Value;
            double eq = (double)this.numericEqHeston.Value;
            psi = (double)this.numericPsiHeston.Value;
            Hestons = new List<Heston>();


            // check on j
            if (j > n)
            {
                this.numericJHeston.Value = (int)n / 2;
                j = n / 2;
                //MessageBox.Show("J cannot be bigger than n!");
            }

            // creation of distributions
            MinY_Win = MaxY_Win = 0;
            for (int i = 0; i < m; i++)
            {
                Heston H = new Heston(n, mu, k, eq, psi);
                if (H.getMinRandomWalk() <= MinY_Win) MinY_Win = H.getMinRandomWalk();
                if (H.getMaxRandomWalk() >= MaxY_Win) MaxY_Win = H.getMaxRandomWalk();
                Hestons.Add(H);
            }

            // set values for graphics
            MinX_Win = 0;
            //MinY_Win = -sigmaGauss * Math.Sqrt(1/ (double)nGauss) * sigmaGauss ;
            MaxX_Win = n;
            //MaxY_Win = sigmaGauss * Math.Sqrt(1 / (double)nGauss)  * sigmaGauss;
            ViewPort = new ResizableRectangle(this.HestonPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win,
                new RectangleF(50, 45, (float)(0.8 * this.HestonPictureBox.Width), (float)(0.8 * this.HestonPictureBox.Height)));
            ViewPort.ModifiedRect += drawChartsHeston;

            drawChartsHeston();
        }




        // -------------GRAPHICS FUNCTIONS----------------

        private void initGraphics(PictureBox P)
        {
            b = new Bitmap(P.Width, P.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.HighQuality;
        }

        private void drawChartsBern()
        {
            g.Clear(Color.Gainsboro);

            //drawAxis(ViewPort1, g1, "trials", "");
            ViewPort.drawAxis("trials", "");

            drawBernoulliPaths();

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;
            ViewPort.drawHorizontalLine("p", p, pen);
            ViewPort.drawHorizontalLine("p+eps", p + eps, Pens.Brown);
            ViewPort.drawHorizontalLine("p-eps", p - eps, Pens.Brown);

            double Step = 0.05;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> BernAtStepN = new List<double>();
            List<Interval> FrequencyDistribution = new List<Interval>();
            // -------- histogram at time j ---------
            BernAtStepN = Bernoullis.Select(B => B.MeanDistribution[j -1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernAtStepN, StartingPoint, Step);
            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, 1);

            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            // count how many means are inside the strip at step n
            int PathsInsideStrip = BernAtStepN.Count(M => (M < p + eps) && (M > p - eps));
            ViewPort.drawVerticalLine($"n = {j }\npaths in strip = {PathsInsideStrip}", j-1, Pens.Purple);

            drawVerticalHistogram(j-1, ViewPort, ReversedFrequencyDistribution);


            // -------- histogram at time n ---------
            BernAtStepN = Bernoullis.Select(B => B.MeanDistribution[n -1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernAtStepN, StartingPoint, Step);
            // add intervals to cover all the range [0,1]
            addPaddingIntervals(FrequencyDistribution, 1);

            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            // count how many means are inside the strip at step n
            PathsInsideStrip = BernAtStepN.Count(M => (M < p + eps) && (M > p - eps));
            ViewPort.drawVerticalLine($"n = {n}\npaths in strip = {PathsInsideStrip}", n - 1, Pens.Purple);

            drawVerticalHistogram(n-1, ViewPort, ReversedFrequencyDistribution);
        }

        private void drawChartsRade()
        {
            g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", "Random Walk");

            drawRademacherPaths();

            double Step = 5;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> RadesAtStepN = Rademachers.Select(R => R.RandomWalk[j-1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            RadesAtStepN = Rademachers.Select(R => R.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(RadesAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.DashDotDot;

            ViewPort.drawHorizontalLine("0", 0, pen);

        }

        private void drawChartsBernRW()
        {
            ViewPort.g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", "Random Walk");

            drawBernoulliRWPaths();

            double Step = 3;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> BernsRWAtStepN = Bernoullis.Select(B => B.RandomWalk[j - 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernsRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            BernsRWAtStepN = Bernoullis.Select(B => B.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(BernsRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

        }

        private void drawChartsGauss()
        {
            ViewPort.g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", "Random Walk");

            drawGaussianRWPaths();

            double Step = ViewPort.MaxY_Win/30;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> GaussianRWAtStepN = Gaussians.Select(G => G.RandomWalk[j- 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(GaussianRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            GaussianRWAtStepN = Gaussians.Select(G => G.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(GaussianRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

            ViewPort.drawHorizontalLine("0", 0, Pens.Red);
            ViewPort.drawHorizontalLine($"{ViewPort.MaxY_Win.ToString("#.##")}", ViewPort.MaxY_Win, Pens.Tan);
            ViewPort.drawHorizontalLine($"{ViewPort.MinY_Win.ToString("#.##")}", ViewPort.MinY_Win, Pens.Tan);
        }

        private void drawChartsGBM()
        {
            ViewPort.g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", "");

            drawGBMPaths();

            double Step = ViewPort.MaxY_Win / 30;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> GaussianRWAtStepN = GBMs.Select(G => G.RandomWalk[j - 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(GaussianRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            GaussianRWAtStepN = GBMs.Select(G => G.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(GaussianRWAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

            ViewPort.drawHorizontalLine("0", 0, Pens.Red);
            ViewPort.drawHorizontalLine($"{ViewPort.MaxY_Win.ToString("#.##")}", ViewPort.MaxY_Win, Pens.Tan);
            ViewPort.drawHorizontalLine($"{ViewPort.MinY_Win.ToString("#.##")}", ViewPort.MinY_Win, Pens.Tan);
        }

        private void drawChartsVasicek()
        {
            ViewPort.g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", " ");

            drawVasicekPaths();

            double Step = ViewPort.MaxY_Win / 30;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> VasiceksAtStepN = Vasiceks.Select(V => V.RandomWalk[j - 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(VasiceksAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            VasiceksAtStepN = Vasiceks.Select(V => V.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(VasiceksAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

            ViewPort.drawHorizontalLine($"eq", eq, Pens.Red);

            //Vasiceks[0].drawBoundaries(ViewPort);

            ViewPort.drawHorizontalLine($"{ViewPort.MaxY_Win.ToString("#.##")}", ViewPort.MaxY_Win, Pens.Tan);
            ViewPort.drawHorizontalLine($"{ViewPort.MinY_Win.ToString("#.##")}", ViewPort.MinY_Win, Pens.Tan);
        }

        private void drawChartsHeston()
        {
            ViewPort.g.Clear(Color.Gainsboro);

            ViewPort.drawAxis("num of steps", " ");

            drawHestonPaths();

            double Step = ViewPort.MaxY_Win / 30;
            double StartingPoint = ViewPort.MinY_Win;

            List<double> HestonsAtStepN = Hestons.Select(V => V.RandomWalk[j - 1].Y).ToList();
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(HestonsAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            List<Interval> ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(j - 1, ViewPort, ReversedFrequencyDistribution);



            HestonsAtStepN = Hestons.Select(V => V.RandomWalk[n - 1].Y).ToList();
            FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(HestonsAtStepN, StartingPoint, Step);
            // add intervals to cover all the range
            addPaddingIntervals(FrequencyDistribution, ViewPort.MaxY_Win);
            // invert list so the biggest comes before the smallest
            ReversedFrequencyDistribution = Enumerable.Reverse(FrequencyDistribution).ToList();

            drawVerticalHistogram(n - 1, ViewPort, ReversedFrequencyDistribution);

            ViewPort.drawHorizontalLine($"eq", eq, Pens.Red);

            //Vasiceks[0].drawBoundaries(ViewPort);

            ViewPort.drawHorizontalLine($"{ViewPort.MaxY_Win.ToString("#.##")}", ViewPort.MaxY_Win, Pens.Tan);
            ViewPort.drawHorizontalLine($"{ViewPort.MinY_Win.ToString("#.##")}", ViewPort.MinY_Win, Pens.Tan);
        }

        private void drawJumpDistributions1()
        {
            // this section will print the distribuion of distance betwee consesutive jumps (exponential)
            ViewPort.g.Clear(Color.Gainsboro);
            ViewPort.drawAxis("distance","f");
       
            List<double> L = new List<double>();
            L.Clear();

            foreach (Bernoulli B in Bernoullis)
                L.AddRange(B.distancesBetweenConsecutiveJumps());

            List<Interval> ConsecutiveJumpDistr = UnivariateDistribution_CountinuousVariable(L, 1, 1);
            drawHorizontalHistogram(0, ViewPort, ConsecutiveJumpDistr);
        }

        private void drawJumpDistributions2()
        {
            // this section will print the distribution of the distance from each jump from the origin (uniform)
            ViewPort.g.Clear(Color.Gainsboro);
            ViewPort.drawAxis("distance", "f");

            List<double> L2 = new List<double>();
            L2.Clear();

            foreach (Bernoulli B in Bernoullis)
                L2.AddRange(B.distancesJumpsFromOrigin());

            List<Interval> FromOriginJumpDistr = UnivariateDistribution_CountinuousVariable(L2, 1, 10);
            drawHorizontalHistogram(0, ViewPort, FromOriginJumpDistr);
        }

        private void drawBernoulliPaths()
        {
            //draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                B.drawSampleMeanPath(ViewPort);
            }
        }


        private void drawBernoulliRWPaths()
        {
            //draw the path for each mean distribution
            foreach (Bernoulli B in Bernoullis)
            {
                B.drawRandomWalkPath(ViewPort);
            }
        }


        private void drawRademacherPaths()
        {
            foreach (Rademacher R in Rademachers)
            {
                R.drawRandomWalkPath(ViewPort);
            }
        }

        private void drawGaussianRWPaths()
        {
            foreach (Gaussian G in Gaussians)
            {
                G.drawRandomWalkPath(ViewPort);
            }
        }

        private void drawGBMPaths()
        {
            foreach (GBM G in GBMs)
            {
                G.drawRandomWalkPath(ViewPort);
            }
        }

        private void drawVasicekPaths()
        {
            foreach (Vasicek V in Vasiceks)
            {
                V.drawRandomWalkPath(ViewPort);
            }
        }

        private void drawHestonPaths()
        {
            foreach(Heston H in Hestons)
            {
                H.drawRandomWalkPath(ViewPort);
            }
        }

        private void drawVerticalHistogram(int n, ResizableRectangle V, List<Interval> FreqDistr)
        {
            // draw proportionate rectangles 
            Graphics g = V.g;
            double BarWidth = (double)V.R.Height /FreqDistr.Count;
            double max = FreqDistr.Max(I => I.RelativeFrequency);
            double BarMaxHeight = (V.R.Width / FreqDistr.Max(I => I.RelativeFrequency))*0.23;
            //double BarMaxHeight = V.R.Width * 0.4;
            int BarNum = 0;
            foreach (Interval I in FreqDistr)
            {
                float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                RectangleF R = new RectangleF(
                    V.viewport_X(MinX_Win + n),
                    (float)(V.R.Y + (BarNum * BarWidth)),
                    (float)BarHeight,
                    (float)BarWidth
                    );

                SolidBrush B = new SolidBrush(Color.FromArgb(200, 253, 185, 39));
                Pen pen = new Pen(Color.FromArgb(200, 85, 37, 130));
                g.DrawRectangles(pen, new[] { R });
                g.FillRectangle(B, R);                
                BarNum++;
            }

            V.PictureBox.Image = V.b;
        }

        private void drawHorizontalHistogram(int n, ResizableRectangle V, List<Interval> FreqDistr)
        {
            // draw proportionate rectangles 
            Graphics g = V.g;
            double BarWidth = (double)V.R.Width / FreqDistr.Count;
            double max = FreqDistr.Max(I => I.RelativeFrequency);
            double BarMaxHeight = V.R.Height/FreqDistr.Max(I => I.RelativeFrequency);
            int BarNum = 0;
            foreach (Interval I in FreqDistr)
            {
                float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                RectangleF R = new RectangleF(
                    (float)(V.R.X + (BarNum * BarWidth)),
                    (float)(V.R.Y + V.R.Height - (BarMaxHeight * I.RelativeFrequency)),
                    (float)BarWidth + n,
                    BarHeight
                    );

                SolidBrush B = new SolidBrush(Color.FromArgb(200, 253, 185, 39));
                Pen pen = new Pen(Color.FromArgb(200, 85, 37, 130));
                g.DrawRectangles(pen, new[] { R });
                g.FillRectangle(B, R);                
                BarNum++;
            }

            V.PictureBox.Image = V.b;
        }

        private void addPaddingIntervals(List<Interval> L, double MaxValue)
        {
            Interval LastInterval = L[L.Count - 1];
            while ((LastInterval.LowerInclusiveBound + LastInterval.Step) < MaxValue)
            {
                Interval I = new Interval
                {
                    LowerInclusiveBound = L[L.Count - 1].LowerInclusiveBound + LastInterval.Step,
                    Step = LastInterval.Step
                };

                L.Add(I);
                LastInterval = I;
            }
        }


        // ------------- STATS FUNCTIONS ----------------
        private List<Interval> UnivariateDistribution_CountinuousVariable(List<double> L, double StartingPoint, double Step)
        {

            List<Interval> ListOfIntervals = new List<Interval>();

            // Crate and insert first interval
            Interval Interval0 = new Interval();
            Interval0.LowerInclusiveBound = StartingPoint;
            Interval0.Step = Step;
            ListOfIntervals.Add(Interval0);

            // insertion of values
            foreach (var d in L)
            {
                if (double.IsNaN(d))
                    continue;

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
                    else if (d >= (ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step))
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval
                            {
                                LowerInclusiveBound = ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step,
                                Step = Step
                            };

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

        // ------------- USEFUL STUFF ------------------




    }
}
