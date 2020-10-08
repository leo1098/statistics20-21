using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_A_CSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*1_A.Create, in both languages C# and VB.NET, a program which does the following simple tasks:

            when a button is pressed some text appears in a richtexbox on the startup form
            when another button is pressed the richtextbox is cleared
            when the mouse enters the richtextbox, the richtext backcolor is switched to another color
            when the mouse leaves the richtextbox, the richtext backcolor is reset to its original state*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
