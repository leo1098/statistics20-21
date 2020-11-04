using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public partial class RettangoloRidimensionabile
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

    public RettangoloRidimensionabile(PictureBox pictureBox1, Bitmap b, Graphics g, double minX_Win, double minY_Win, double maxX_Win, double maxY_Win, Rectangle rettangolo)
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
        Rettangolo = rettangolo;
    }

    public event RettangoloModificatoEventHandler RettangoloModificato;
    public delegate void RettangoloModificatoEventHandler();

    public Rectangle Rettangolo;
    public Point Location_MouseDown;
    public Point Location_Rettangolo_MouseDown;
    public Size Size_Rettangolo_MouseDown;
    public bool DraggingIncorso;
    public bool ResizingIncorso;

    private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        if (!Rettangolo.Contains(e.Location))
            return;


        // salviamo la posizione iniziale del mouse
        Location_MouseDown = e.Location;                 // oppure New Point(e.X, e.Y)
        if (e.Button == MouseButtons.Left)
        {

            // salviamo la posizione iniziale del rettangolo
            Location_Rettangolo_MouseDown = Rettangolo.Location;
            DraggingIncorso = true;
        }
        else if (e.Button == MouseButtons.Right)
        {
            Size_Rettangolo_MouseDown = Rettangolo.Size;
            ResizingIncorso = true;
        }
    }

    private void PictureBox1_MouseEnter(object sender, EventArgs e)
    {
        PictureBox1.Focus();
    }

    private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (DraggingIncorso)
        {
            int DeltaX = e.X - Location_MouseDown.X;
            int DeltaY = e.Y - Location_MouseDown.Y;
            Rettangolo.Location = new Point(Location_Rettangolo_MouseDown.X + DeltaX, Location_Rettangolo_MouseDown.Y + DeltaY);
            if (Rettangolo.Right < 50)
            {
                Rettangolo.X = -Rettangolo.Width + 50;
            }

            if (Rettangolo.X > b.Width - 50)
            {
                Rettangolo.X = b.Width - 50;
            }

            if (Rettangolo.Bottom < 50)
            {
                Rettangolo.Y = -Rettangolo.Height + 50;
            }

            if (Rettangolo.Y > b.Height - 50)
            {
                Rettangolo.Y = b.Height - 50;
            }

            RettangoloModificato?.Invoke();
        }
        else if (ResizingIncorso)
        {
            int DeltaX = e.X - Location_MouseDown.X;
            int DeltaY = e.Y - Location_MouseDown.Y;
            Rettangolo.Size = new Size(Size_Rettangolo_MouseDown.Width + DeltaX, Size_Rettangolo_MouseDown.Height + DeltaY);
            if (Rettangolo.Width < 20)
                Rettangolo.Width = 20;
            if (Rettangolo.Height < 20)
                Rettangolo.Height = 20;
            RettangoloModificato?.Invoke();
        }
    }

    private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        //drawViewport();
        DraggingIncorso = false;
        ResizingIncorso = false;
    }

    private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        if (!Rettangolo.Contains(e.Location))
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

        Rettangolo.Location = new Point(Rettangolo.X - DeltaX, Rettangolo.Y - DeltaY);
        Rettangolo.Size = new Size(Rettangolo.Width + 2 * DeltaX, Rettangolo.Height + 2 * DeltaY);
        if (Rettangolo.Width < 20)
            Rettangolo.Width = 20;
        if (Rettangolo.Height < 20)
            Rettangolo.Height = 20;
        RettangoloModificato?.Invoke();
    }

    public int viewport_X(double World_X)
    {
        return (int)(Rettangolo.Left + (World_X - MinX_Win) * (Rettangolo.Width / Range_X));
    }

    public int viewport_Y(double World_Y)
    {
        return (int)(Rettangolo.Top + Rettangolo.Height - (World_Y - MinY_Win) * (Rettangolo.Height / Range_Y));
    }
}