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
            // Clear richTextBox
            this.textBox.Text = "";
        }

        private void putTextButton_Click(object sender, EventArgs e)
        {
            // Insert text in the box
            this.textBox.Text = "A simple string :)";
        }

        private void textBox_MouseEnter(object sender, EventArgs e)
        {
            // Choose a random color each time the mouse enters the box
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            this.textBox.BackColor = randomColor;
        }

        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            // Reset original color when mouse leaves
            this.textBox.BackColor = Color.White;
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                listBox1.Items.Add(s[i]);
            
        }
    }
}
