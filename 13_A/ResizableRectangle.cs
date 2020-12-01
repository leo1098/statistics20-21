using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


public partial class ResizableRectangle
{

    public PictureBox PictureBox1
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return PictureBox;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            if (PictureBox != null)
            {
                PictureBox.MouseDown -= PictureBox1_MouseDown;
                PictureBox.MouseEnter -= PictureBox1_MouseEnter;
                PictureBox.MouseMove -= PictureBox1_MouseMove;
                PictureBox.MouseUp -= PictureBox1_MouseUp;
                PictureBox.MouseWheel -= PictureBox1_MouseWheel;
            }

            PictureBox = value;
            if (PictureBox != null)
            {
                PictureBox.MouseDown += PictureBox1_MouseDown;
                PictureBox.MouseEnter += PictureBox1_MouseEnter;
                PictureBox.MouseMove += PictureBox1_MouseMove;
                PictureBox.MouseUp += PictureBox1_MouseUp;
                PictureBox.MouseWheel += PictureBox1_MouseWheel;
            }
        }
    }

    public PictureBox PictureBox;
    public Bitmap b;
    public Graphics g;
    public double MinX_Win;
    public double MinY_Win;
    public double MaxX_Win;
    public double MaxY_Win;
    public double Range_X;
    public double Range_Y;
    public RectangleF R;

    public ResizableRectangle(PictureBox pictureBox1, Bitmap b, Graphics g, double minX_Win, double minY_Win, double maxX_Win, double maxY_Win, RectangleF R)
    {
        PictureBox1 = pictureBox1;
        this.b = b;
        this.g = g;
        MinX_Win = minX_Win;
        MinY_Win = minY_Win;
        MaxX_Win = maxX_Win;
        MaxY_Win = maxY_Win;
        Range_X = maxX_Win - minX_Win;
        Range_Y = maxY_Win - minY_Win;
        this.R = R;
    }

    public event ModifiedRectEventHandler ModifiedRect;
    public delegate void ModifiedRectEventHandler();

    public PointF Location_MouseDown;
    public PointF Location_Rect_MouseDown;
    public SizeF Size_Rect_MouseDown;
    public bool Dragging;
    public bool Resizing;

    private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        if (!R.Contains(e.Location))
            return;
        Location_MouseDown = e.Location;                 // oppure New Point(e.X, e.Y)
        if (e.Button == MouseButtons.Left)
        {
            // salviamo la posizione iniziale del rettangolo
            Location_Rect_MouseDown = R.Location;
            Dragging = true;
        }
        else if (e.Button == MouseButtons.Right)
        {
            Size_Rect_MouseDown = R.Size;
            Resizing = true;
        }
    }

    private void PictureBox1_MouseEnter(object sender, EventArgs e)
    {
        PictureBox1.Focus();
    }

    private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (Dragging)
        {
            float DeltaX = e.X - Location_MouseDown.X;
            float DeltaY = e.Y - Location_MouseDown.Y;
            R.Location = new PointF(Location_Rect_MouseDown.X + DeltaX, Location_Rect_MouseDown.Y + DeltaY);
            if (R.Right < 50)
            {
                R.X = -R.Width + 50;
            }

            if (R.X > b.Width - 50)
            {
                R.X = b.Width - 50;
            }

            if (R.Bottom < 50)
            {
                R.Y = -R.Height + 50;
            }

            if (R.Y > b.Height - 50)
            {
                R.Y = b.Height - 50;
            }

            ModifiedRect?.Invoke();
        }
        else if (Resizing)
        {
            float DeltaX = e.X - Location_MouseDown.X;
            float DeltaY = e.Y - Location_MouseDown.Y;
            R.Size = new SizeF(Size_Rect_MouseDown.Width + DeltaX, Size_Rect_MouseDown.Height + DeltaY);
            if (R.Width < 20)
                R.Width = 20;
            if (R.Height < 20)
                R.Height = 20;
            ModifiedRect?.Invoke();
        }
    }

    private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        Dragging = false;
        Resizing = false;
    }

    private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        if (!R.Contains(e.Location))
            return;
        int DeltaX;
        int DeltaY;
        if (e.Delta > 0)
        {
            DeltaX = 10;
            DeltaY = 10;
        }
        else
        {
            DeltaX = -10;
            DeltaY = -10;
        }

        R.Location = new PointF(R.X - DeltaX, R.Y - DeltaY);
        R.Size = new SizeF(R.Width + 2 * DeltaX, R.Height + 2 * DeltaY);
        if (R.Width < 20)
            R.Width = 20;
        if (R.Height < 20)
            R.Height = 20;
        ModifiedRect?.Invoke();
    }

    public float viewport_X(double World_X)
    {
        return (float)(R.Left + (World_X - MinX_Win) * (R.Width / Range_X));
    }

    public float viewport_Y(double World_Y)
    {
        return (float)(R.Top + R.Height - (World_Y - MinY_Win) * (R.Height / Range_Y));
    }

    public  void drawAxis(string Name_X, string Name_Y)
    {
        Pen p = new Pen(Color.Black);
        p.EndCap = LineCap.ArrowAnchor;


        // X axis
        g.DrawLine(
            p,
            viewport_X(MinX_Win),
            viewport_Y(MinY_Win),
            viewport_X(MaxX_Win),
            viewport_Y(MinY_Win));

        g.DrawString(Name_X, SystemFonts.DefaultFont, Brushes.Black,
            viewport_X(MaxX_Win),
            (float)(viewport_Y(MinY_Win) + 0.05 * MaxY_Win));

        // Y axis
        g.DrawLine(
            p,
            viewport_X(MinX_Win),
            viewport_Y(MinY_Win),
            viewport_X(MinX_Win),
            viewport_Y(MaxY_Win + 0.1)
            );

        g.DrawString(
            Name_Y,
            SystemFonts.DefaultFont,
            Brushes.Black,
            viewport_X(MinX_Win) - g.MeasureString(Name_Y, SystemFonts.DefaultFont).Width,
            (float)(viewport_Y(MaxY_Win) + 0.05 * MaxX_Win));
    }

    public void drawHorizontalLine(string label, double y, Pen pen)
    {
        g.DrawLine(
            pen,
            viewport_X(MinX_Win),
            viewport_Y(y),
            viewport_X(MaxX_Win),
            viewport_Y(y));

        g.DrawString(
            label,
            SystemFonts.DefaultFont,
            Brushes.Black,
            viewport_X(MinX_Win) - g.MeasureString(label, SystemFonts.DefaultFont).Width,
            viewport_Y(y)
            );
    }

    public void drawVerticalLine(string label, double x, Pen pen)
    {
        // Y axis
        g.DrawLine(
            pen,
            viewport_X(MinX_Win + x),
            viewport_Y(MinY_Win),
            viewport_X(MinX_Win + x),
            viewport_Y(MaxY_Win)
            );

        g.DrawString(
            label,
            SystemFonts.DefaultFont,
            Brushes.Black,
            viewport_X(MinX_Win + x),
            viewport_Y(MinY_Win) + g.MeasureString(label, SystemFonts.DefaultFont).Height
            );
    }

}