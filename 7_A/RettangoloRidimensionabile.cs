using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public partial class RettangoloRidimensionabile
{
    private PictureBox _PictureBox1;

    public PictureBox PictureBox1
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _PictureBox1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            if (_PictureBox1 != null)
            {
                _PictureBox1.MouseDown -= PictureBox1_MouseDown;
                _PictureBox1.MouseEnter -= PictureBox1_MouseEnter;
                _PictureBox1.MouseMove -= PictureBox1_MouseMove;
                _PictureBox1.MouseUp -= PictureBox1_MouseUp;
                _PictureBox1.MouseWheel -= PictureBox1_MouseWheel;
            }

            _PictureBox1 = value;
            if (_PictureBox1 != null)
            {
                _PictureBox1.MouseDown += PictureBox1_MouseDown;
                _PictureBox1.MouseEnter += PictureBox1_MouseEnter;
                _PictureBox1.MouseMove += PictureBox1_MouseMove;
                _PictureBox1.MouseUp += PictureBox1_MouseUp;
                _PictureBox1.MouseWheel += PictureBox1_MouseWheel;
            }
        }
    }

    public Bitmap b;

    public RettangoloRidimensionabile(PictureBox PictureBox1, Bitmap b, Rectangle RettangoloIniziale)
    {
        this.PictureBox1 = PictureBox1;
        this.b = b;
        Rettangolo = RettangoloIniziale;
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
        RettangoloModificato?.Invoke();
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
}