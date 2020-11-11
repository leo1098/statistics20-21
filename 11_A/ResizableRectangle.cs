using System;
using System.Drawing;
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
    public Rectangle R;

    public ResizableRectangle(PictureBox pictureBox1, Bitmap b, Graphics g, double minX_Win, double minY_Win, double maxX_Win, double maxY_Win, Rectangle R)
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

    public Point Location_MouseDown;
    public Point Location_Rect_MouseDown;
    public Size Size_Rect_MouseDown;
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
            int DeltaX = e.X - Location_MouseDown.X;
            int DeltaY = e.Y - Location_MouseDown.Y;
            R.Location = new Point(Location_Rect_MouseDown.X + DeltaX, Location_Rect_MouseDown.Y + DeltaY);
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
            int DeltaX = e.X - Location_MouseDown.X;
            int DeltaY = e.Y - Location_MouseDown.Y;
            R.Size = new Size(Size_Rect_MouseDown.Width + DeltaX, Size_Rect_MouseDown.Height + DeltaY);
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

        R.Location = new Point(R.X - DeltaX, R.Y - DeltaY);
        R.Size = new Size(R.Width + 2 * DeltaX, R.Height + 2 * DeltaY);
        if (R.Width < 20)
            R.Width = 20;
        if (R.Height < 20)
            R.Height = 20;
        ModifiedRect?.Invoke();
    }

    public int viewport_X(double World_X)
    {
        return (int)(R.Left + (World_X - MinX_Win) * (R.Width / Range_X));
    }

    public int viewport_Y(double World_Y)
    {
        return (int)(R.Top + R.Height - (World_Y - MinY_Win) * (R.Height / Range_Y));
    }
}